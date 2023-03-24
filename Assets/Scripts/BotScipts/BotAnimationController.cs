using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotAnimationController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private bool isRunning;

    public bool IsRunning { get => isRunning; set => isRunning = value; }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (agent.velocity.sqrMagnitude > 0f)
        {
            animator.SetFloat("Run", 1f);
            isRunning = true;
        }else if(isRunning && agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            animator.SetFloat("Run", 0f);
            isRunning = false;
        }
    }


}
