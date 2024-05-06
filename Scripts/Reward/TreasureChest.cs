using Godot;
using System;

public partial class TreasureChest : StaticBody3D
{
    [Export] private Area3D areaNode;
    [Export] private Sprite3D spriteNode;
    [Export] private RewardResource reward;
    [Export]private AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        areaNode.BodyEntered += (body) => spriteNode.Visible = true;
        areaNode.BodyExited += (body) => spriteNode.Visible = false;
        animationPlayer.AnimationFinished += HandleAnimationFinished;
    }

    private void HandleAnimationFinished(StringName animName)
    {
        animationPlayer.AnimationFinished -= HandleAnimationFinished;
        areaNode.Monitoring = false;
        GameEvents.RaiseReward(reward);
    }


    public override void _Input(InputEvent @event)
    {
        if(
            !areaNode.Monitoring ||
            !areaNode.HasOverlappingBodies() ||
            !Input.IsActionJustPressed(GameConstants.INPUT_INTERACT))
        {
            return;
        }
        animationPlayer.Play("open");
        animationPlayer.SpeedScale = 5;

    }

}
