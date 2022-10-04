using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

public class MoveNavMesh : MonoBehaviour
{
    void Start()
    {
        var agent = GetComponent<NavMeshAgent>(); 
        agent.updateRotation = false; 
        agent.updateUpAxis = false;
    }

    void Update()
    {
        
    }
}
