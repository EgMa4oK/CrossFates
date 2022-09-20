using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 coords;
    [SerializeField] private bool useTarget = true;
    private Animator anim;
    private NavMeshAgent agent;
    private bool CheckXY;
    public float Distance { get; private set; }

    void Start()
    {
        anim = GetComponent<Animator>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        Walk();
        if (useTarget)
        {
            Distance = agent.remainingDistance;
            agent.SetDestination(Target.position);
        }
        else
        {
            agent.SetDestination(coords);
        }
    }


    private void Walk()
    {
        if (Math.Abs(agent.velocity.y) > Math.Abs(agent.velocity.x))
        {
            CheckXY = true;
        }
        else if (Math.Abs(agent.velocity.y) < Math.Abs(agent.velocity.x))
        {
            CheckXY = false;
        }
        anim.SetBool("CheckXY", CheckXY);
        anim.SetFloat("EnemymoveX", agent.velocity.x);
        anim.SetFloat("EnemymoveY", agent.velocity.y);
    }

    public IEnumerator Stun(float sec)
    {
        agent.isStopped = true;
        anim.SetBool("Stun", true);
        yield return new WaitForSeconds(sec);
        agent.isStopped = false;
        anim.SetBool("Stun", false);
    }
}
