using System;
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

    private void OnEnable()
    {
        GlobalEvents.OnLevelEnd.AddListener(StartEndLevelDance);
    }

    private void StartEndLevelDance(int animIndex)
    {

        switch (animIndex)
        {
            case 0:
                animator.SetTrigger("Dance1");
                break;
            case 1:
                animator.SetTrigger("Dance2");
                break;
            case 2:
                animator.SetTrigger("Dance3");
                break;
            case 3:
                animator.SetTrigger("Dance4");
                break;
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (agent.velocity.sqrMagnitude > 0f)
        {
            isRunning = true;
        }
        else if (isRunning && agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            animator.SetBool("IsRunning", false);
            isRunning = false;
            GetComponent<Transform>().localEulerAngles = new Vector3(0f, 90f, 0f);
        }
    }


}
