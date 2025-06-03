using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilderScript : BaseRoomScript
{
    public Transform wallCube;
    public Transform floor;

    public enum ROOM_ITEM
    {
        EMPTY = 0,
        WALL = 1,
        NORTH_GATE = 2,
        EAST_GATE = 3,
        SOUTH_GATE = 4,
        WEST_GATE = 5,
        FLOOR = 8,
        
    }

    int[,] roomsSchema =
    {
        {0,0,1,1,2,2,1,1,0,0},
        {0,1,1,8,8,8,8,1,1,0},
        {1,1,8,8,8,8,8,8,1,1},
        {1,8,8,8,8,8,8,8,8,1},
        {5,8,8,8,8,8,8,8,8,3},
        {5,8,8,8,8,8,8,8,8,3},
        {1,8,8,8,8,8,8,8,8,1},
        {1,1,8,8,8,8,8,8,1,1},
        {0,1,1,8,8,8,8,1,1,0},
        {0,0,1,1,4,4,1,1,0,0},
    };

    //numero de port y transform per el element

    Dictionary<int, List<Transform>> gateTransforms = new();

    Dictionary<Vector2, int> mapper = new()
    {
        {Vector2.up, (int)ROOM_ITEM.NORTH_GATE},
        {Vector2.right, (int)ROOM_ITEM.EAST_GATE},
        {Vector2.down, (int)ROOM_ITEM.SOUTH_GATE},
        {Vector2.left, (int)ROOM_ITEM.WEST_GATE}
    };

    override public void openGate(Vector2 dir, bool enter, bool open)
    {
        //Invertir les direccions per saber la direccio de la nova habitacio que es la contraria de la que ja esta creada!
        if (enter) dir =- dir; 
        //Saber quina es la direcció de la porta que s'ha d'eliminar
        int d = (int)mapper[dir];

        foreach(Transform t in gateTransforms[d])
        {
            t.gameObject.SetActive(!open);
        }

    }


   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() //Before the start
    {

        for(int i = (int)ROOM_ITEM.NORTH_GATE; i <= (int)ROOM_ITEM.WEST_GATE; i++)
        {
            gateTransforms[i] = new();
        }

        Vector3 p = this.transform.position;
        for (int x = 0; x < roomsSchema.GetLength(0); x++)
        {
            for (int z = 0; z < roomsSchema.GetLength(1); z++)
            {
                int current = roomsSchema[z, roomsSchema.GetLength(0)-1-x];
                if (current >= (int)ROOM_ITEM.WALL && current <= (int)ROOM_ITEM.WEST_GATE)
                {
                    Vector3 np = new Vector3(p.x + x ,p.y, p.z + z);
                    Transform t = Instantiate(wallCube, np, Quaternion.identity);

                    if (current != (int)ROOM_ITEM.WALL)
                    {
                        gateTransforms[current].Add(t);
                    }

                }

                if (current == (int)ROOM_ITEM.FLOOR || current > 0)
                {
                    Vector3 np = new Vector3(p.x + x, p.y, p.z + z);
                    Quaternion.AngleAxis(90, np);
                    Instantiate(floor, np, Quaternion.identity);
                }
            }
        }




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
