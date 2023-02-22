using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    private readonly Vector3 _destibation;
    private readonly NavMeshAgent _agent;
    private readonly float _commandFinishDistance;

    public MoveCommand(Vector3 destibation, NavMeshAgent agent, float commandFinishDistance)
    {
        _destibation = destibation;
        _agent = agent;
        _commandFinishDistance = commandFinishDistance;
    }

    public override void Execute()
    {
        _agent.SetDestination(_destibation);
    }

    public override bool IsFinished => _agent.remainingDistance <= _commandFinishDistance;
}
