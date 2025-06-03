using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
public abstract class BaseRoomScript : MonoBehaviour
{
    public abstract void openGate(Vector2 dir, bool enter, bool open);
}
