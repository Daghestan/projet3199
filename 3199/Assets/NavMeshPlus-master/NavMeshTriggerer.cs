using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTriggerer : MonoBehaviour
{
    // Start is called before the first frame updateMesh
    public NavMeshSurface NavSurface;
    
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
         NavSurface.BuildNavMesh();
    }
}
