using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
    [Export] private Timer chaseTimerNode;
    private CharacterBody3D target;
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);

        chaseTimerNode.Timeout += handleChaseTimeOut;
        target = characterNode.ChaseAreaNode
            .GetOverlappingBodies()
                .First() as CharacterBody3D;
        characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
        
    }
    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    protected override void ExitState()
    {
        chaseTimerNode.Timeout -= handleChaseTimeOut;
        characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
        characterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
    }
    private void handleChaseTimeOut()
    {
        destination = target.GlobalPosition;
        characterNode.AgentNode.TargetPosition = destination;       
    }

    private void HandleAttackAreaBodyEntered(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
    }

    private void HandleChaseAreaBodyExited(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();
    }

}
