using UnityEngine;
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
