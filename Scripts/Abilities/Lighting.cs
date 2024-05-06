using Godot;
using System;

public partial class Lighting : Ability
{
    public override void _Ready()
    {
        playerNode.AnimationFinished += (animName) => QueueFree();
    }
}
