using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool Walkable;
    public Vector3 worldPos;

    public Node(bool _Walkable, Vector3 pos){
        Walkable = _Walkable;
        worldPos = pos;
    }
}
