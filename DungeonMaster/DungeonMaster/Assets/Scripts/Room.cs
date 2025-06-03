using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public static int SIZE = 10;

    private Transform roomTransform;
    private Transform type;

    //Referencias dels veins
    private Room[] neighboors = new Room[4];

    private Transform prefabRoom;
    private int x;
    private int y;

    public int X { get { return x; } }
    public int Y { get { return y; } }

    public Room(Transform prefabRoom, int x, int y)
    {
        this.prefabRoom = prefabRoom;
        this.x = x;
        this.y = y;

        roomTransform = Object.Instantiate(prefabRoom, new Vector3(x*SIZE, 0, y*SIZE), Quaternion.identity);
        
    }


    public Vector3 getMiddlePosition()
    {
        //return new Vector3((x * SIZE)/2, 0, (y * SIZE)/2);
        return new Vector3((x+0.5f) * SIZE, 0, (y+0.5f) * SIZE);
    }

    private List<Vector2> direccionsPossibles = new();
    public List<Vector2> GetDireccionsPossibles()
    {
        return direccionsPossibles;
    }

    public void openGate(Vector2 dir, bool enter, bool open)
    {
        if (open)
        {
            direccionsPossibles.Add(enter ? -dir : dir);
        }

        BaseRoomScript rbs = roomTransform.GetComponent<BaseRoomScript>();
        rbs.openGate(dir, enter, open);
    }

}
