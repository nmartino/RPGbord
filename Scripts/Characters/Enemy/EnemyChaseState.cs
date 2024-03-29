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
        
    }

    public override void _PhysicsProcess(double delta)
    {
        Move();
    }

    protected override void ExitState()
    {
       chaseTimerNode.Timeout -= handleChaseTimeOut;
    }
    private void handleChaseTimeOut()
    {
        destination = target.GlobalPosition;
        characterNode.AgentNode.TargetPosition = destination;       
    }



}
