using Godot;
using System;

public partial class chair : StaticBody3D
{
    [Export] private Area3D areaNode;
    [Export] private Sprite3D spriteNode;

    public override void _Ready()
    {
        areaNode.BodyEntered += (body) => spriteNode.Visible = true;
        areaNode.BodyExited += (body) => spriteNode.Visible = false;

    }

    private void HandleAnimationFinished(StringName animName)
    {
        areaNode.Monitoring = false;
    }
}
