using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static RoomBuilderScript;

public class CorridorBuilderScript : BaseRoomScript
{
    
    public Transform[] elementsOpen = new Transform[4];
    public Transform[] elementsClose = new Transform[4];

    Dictionary<Vector2, int> mapper = new()
    {
        {Vector2.up, 2},
        {Vector2.right, 3},
        {Vector2.down, 0},
        {Vector2.left, 1}
    };

    public void Awake()
    {
        for(int i = 0; i < elementsClose.Length; i++) { 
            elementsOpen[i].gameObject.SetActive(false);
            elementsClose[i].gameObject.SetActive(true);
        }

    }

    public override void openGate(Vector2 dir, bool enter, bool open)
    {
        int index = mapper[dir * (enter ? -1 : 1)]; //si es entrada o sortida es canvia la direcció
        //int index = mapper[dir]; //si es entrada o sortida es canvia la direcció

        elementsOpen[index].gameObject.SetActive(open);
        elementsClose[index].gameObject.SetActive(!open);
        
    }




}
