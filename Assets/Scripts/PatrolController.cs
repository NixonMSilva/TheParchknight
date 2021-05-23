using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PatrolController : MonoBehaviour
{
    public List<Transform> nodes;
    public int currentNode = 0;
    public bool isMirrored = false;

    private void Awake ()
    {
        if (isMirrored)
        {
            for (int i = (nodes.Count - 2); i > 0; --i)
            {
                nodes.Add(nodes[i]);
            }
        }
        
        // Mirrors the nodes if true;
        /*for (int i = 0; < nodes.Length; ++i)
        {
            nodes.App
        }*/
    }

    public int PatrolLenght()
    {
        return nodes.Count;
    }

}
