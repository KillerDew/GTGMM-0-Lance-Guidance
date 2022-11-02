using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform Player;
    Node[,] grid;
    public Vector2 gridworldsize;
    public float noderadius;
    public LayerMask UnwalkableMask;

    float nodediameter;
    int Gridsize_x;
    int Gridsize_Y;
    
    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridworldsize.x, 1, gridworldsize.y));
        if (grid != null){
            Node PlayerNode = NodefromWorldPos(Player.position);
            foreach (Node n in grid){
                Gizmos.color = (n.Walkable) ? Color.white : Color.red;
                if (PlayerNode == n){
                    Gizmos.color = Color.cyan;
                }
                Gizmos.DrawCube(n.worldPos, Vector3.one*(nodediameter - .1f));
            }
        }
    }

    void Start() {
        nodediameter = noderadius*2;
        Gridsize_x = Mathf.RoundToInt(gridworldsize.x/nodediameter);
        Gridsize_Y = Mathf.RoundToInt(gridworldsize.y/nodediameter);
        Creategrid();
    }
    void Creategrid(){
        grid = new Node[Gridsize_x, Gridsize_Y];
        Vector3 WorldBotomLeft = transform.position - Vector3.right * gridworldsize.x/2 - Vector3.forward * gridworldsize.y/2;

        for (int x=0; x<Gridsize_x;x++){
            for (int y=0; y<Gridsize_Y;y++){
                Vector3 Worldpoint = WorldBotomLeft + Vector3.right * (x * nodediameter + noderadius) +Vector3.forward * (y*nodediameter+noderadius);
                bool walkable = !(Physics.CheckSphere(Worldpoint, noderadius, UnwalkableMask));
                grid[x,y] = new Node(walkable,Worldpoint);
            }
        }
    }
    public Node NodefromWorldPos(Vector3 WorldPos){
        float percentX = (WorldPos.x + gridworldsize.x/2)/ gridworldsize.x;
        float percentY = (WorldPos.z + gridworldsize.y/2)/ gridworldsize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((Gridsize_x-1) * percentX);
        int y = Mathf.RoundToInt((Gridsize_Y-1) * percentY);
        
        
        return grid[x,y];
    }

    
}

