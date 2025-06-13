using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MazeGeneratorScript : MonoBehaviour
{
    
    static Vector2[] dirs =
    {
        Vector2.left,
        Vector2.up,
        Vector2.right,
        Vector2.down,
    };

    static Vector2[] dirs8 =
    {
        Vector2.left,
        new Vector2(-1, -1),
        Vector2.up,
        new Vector2(-1, 1),
        Vector2.right,
        new Vector2(1, 1),
        Vector2.down,
        new Vector2(1, -1),
    };

    public static Vector2[] getDirs()
    {
        return dirs;
    }
    public static Vector2[] getDirs8()
    {
        return dirs8;
    }

    //Tipus d'habitacions per a que siguin autogenerades
    public Transform[] prefabsRooms;

    
    //public Transform prefabRoom;

    //Character
    public Transform character;

    //mainCamera
    public Transform mainCamera;

    public Transform AuxHeart;
    public Transform AuxKey;

    public Transform AuxPortal;
    public Transform AuxBoss;

    

    // {0.7, 0.2, 0.1}
    //public float[] probabilitiesRooms;

    //Number of rooms;
    public int N;

    //Boundings of map
    public int SIZE;
    
    Room[,] map;

    List<Room> rooms = new List<Room>();
    List<Room> normalRooms = new List<Room>();

    List<int> IndexesUsed = new();

    public int MaxHearts = 6;
    public int MaxKeys = 3;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        map = new Room[SIZE, SIZE];

        Room initialRoom = new Room(prefabsRooms[0], SIZE / 2, SIZE / 2);
        map[SIZE / 2, SIZE / 2] = initialRoom;
        rooms.Add(initialRoom);

        //Add character
        //Instantiate(prefabRoom, initialRoom.getMiddlePosition(), Quaternion.identity);

        character.transform.position = initialRoom.getMiddlePosition();
        mainCamera.transform.position = initialRoom.getMiddlePosition() + new Vector3(0, 10, 0);

        //AuxHeart.transform.position = initialRoom.getMiddlePosition() + new Vector3(3, 1, 0);
        //AuxKey.transform.position = initialRoom.getMiddlePosition() + new Vector3(3, 1, 0);

        while (rooms.Count() < N)
        {
            //Debug.Log("N:"+ rooms.Count());
            int idxSeedRoom = Random.Range(0, rooms.Count());
            Room seedRoom = rooms[idxSeedRoom];



            //Escollir una direcció
            int idx = Random.Range(0, dirs.Length);
            Vector2 dir = dirs[idx];

            int x = seedRoom.X + (int)dir.x;
            int y = seedRoom.Y + (int)dir.y;


            if (map[x, y] == null)
            {

                //Si te molts veins no deixa insertar la habitació
                int veins = comptarVeins(x, y);
                if (veins < 3)
                {
                    int index_aux = Random.Range(0, prefabsRooms.Length);
                    Room aux_room = new Room(prefabsRooms[index_aux], x, y);
                    map[x, y] = aux_room;
                    rooms.Add(aux_room);

                    if(index_aux == 0) //Normal room!
                    {
                        normalRooms.Add(aux_room);
                    }

                    aux_room.openGate(dir, false, true);
                    seedRoom.openGate(dir, true, true);
                }

            }

        }


        //Add keys and hearts at the maze
        AddMiddleRoomElements(MaxHearts, AuxHeart);

        AddMiddleRoomElements(MaxKeys, AuxKey);

        AddMiddleRoomElements(1, AuxBoss, 0);

        //Add portal!
        Transform PortalInstance = AddMiddleRoomElements(1, AuxPortal, 0);
        //Add the instantiate portal at the keys controller to can active the poral
        character.transform.GetComponent<KeysController>().setPortal(PortalInstance);

    }

    private Transform AddMiddleRoomElements(int max, Transform element, int y = 1)
    {
        Transform lastInstance = null;
        int n = 0;
        
        while (max > n)
        {
            int aux_index = Random.Range(0, normalRooms.Count());
            if (!IndexesUsed.Contains<int>(aux_index))
            {
                IndexesUsed.Add(aux_index);
                Room r = normalRooms[aux_index];

                Vector3 pos = r.getMiddlePosition();
                Transform aux = element;
                aux.position = pos + new Vector3(0, y, 0);
                lastInstance = Instantiate(aux);

                n += 1;

            }
        }

        return lastInstance;
    }

    private int comptarVeins(int x, int y)
    {
        Vector2 p = new Vector2(x, y);

        int veins = 0;

        foreach(Vector2 curr in dirs8)
        {
            Vector2 aux = p + curr;

            if(
                (int)aux.x >= 0 && (int)aux.x <SIZE &&
                (int)aux.y >= 0 && (int)aux.y <SIZE &&
                map[(int)aux.x, (int)aux.y] != null
            )
            {
                veins++;
            }
        }

        return veins;
    }


    public Room GetRoomPerPosition(Vector3 position)
    {
        position = position / Room.SIZE;
        //Debug.Log("POSITION: "+position);
        return map[(int)position.x, (int)position.z];
    }

    // Update is called once per frame
    void Update()
    {



    }
}
