using Godot;
using System;

public partial class EnemyStunState : EnemyState
{
    [Export] private Timer cooldownTimerNode;
    public override void _Ready()
    {
        base._Ready();
        canTransition = () => cooldownTimerNode.IsStopped();
    }
    protected override void EnterState()
    {
        base.EnterState();
        canTransition = () => cooldownTimerNode.IsStopped();
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_STUN);

        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimFinished;
          
    }

    protected override void ExitState(){
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimFinished;
    }

    private void HandleAnimFinished(StringName animName)
    {
        if (canTransition()){
            cooldownTimerNode.Start();
            GD.Print("Start");
        }       
        GD.Print("Stopped"); 
        if(characterNode.AttackAreaNode.HasOverlappingBodies()){
            characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
        }else if(characterNode.ChaseAreaNode.HasOverlappingBodies()){
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        }else
        {
            characterNode.StateMachineNode.SwitchState<EnemyIdleState>();
        }
    }

}
