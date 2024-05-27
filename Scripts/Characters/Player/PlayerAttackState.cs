using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer comboTimerNode;
    [Export] private PackedScene lightingScene;
    private int comboCounter = 1;
    private int maxComboCount = 2;

    public override void _Ready()
    {
        base._Ready();
        comboTimerNode.Timeout += () => comboCounter = 1;
    }
    protected override void EnterState()
    {
        characterNode.AnimPlayerNode.Play(
            GameConstants.ANIM_ATTACK + comboCounter,-1,2
        );

        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
        characterNode.HitBoxNode.BodyEntered += HandleBodyEntered;

        }


    protected override void ExitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        characterNode.HitBoxNode.BodyEntered -= HandleBodyEntered;
        comboTimerNode.Start();        
    }

    private void HandleAnimationFinished(StringName animName)
    {
        if(comboCounter==maxComboCount)
        {
            comboCounter = 1;
        }else{
            comboCounter++;
        }
        characterNode.ToggleHitBox(true);
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }
    
    private void PerformHit()
    {
        Vector3 newPosition = characterNode.isFlip? 
        Vector3.Left :
        Vector3.Right;
        float distanceMultiplier = 0.85f;
        newPosition *= distanceMultiplier;
        characterNode.HitBoxNode.Position = newPosition;
        characterNode.ToggleHitBox(false);
    }

        private void HandleBodyEntered(Node3D body)
    {
        if (comboCounter != maxComboCount){return;}

        Node3D lighting = lightingScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(lighting);
        lighting.GlobalPosition = body.GlobalPosition;

    }
}

