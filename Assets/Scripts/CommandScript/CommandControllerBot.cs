using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class CommandControllerBot : MonoBehaviour
{
    [SerializeField] private float commandFinishDistance;
    [SerializeField] private LayerMask platformMask;

    private NavMeshAgent _agent;

    private Queue<Command> _commnads = new Queue<Command>();
    private Command _currentCommand;


    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ListenForCommands();
        ProcessCommands();
    }

    private void ListenForCommands()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, platformMask))
            {
                _commnads.Enqueue(new MoveCommand(hitInfo.point, _agent, commandFinishDistance));
            }
        }
    }
    private void ProcessCommands()
    {
        if (_currentCommand != null && _currentCommand.IsFinished == false)
            return;

        if(_commnads.Any() == false)
            return;

        _currentCommand = _commnads.Dequeue();
        _currentCommand.Execute();
    }

}
