using Godot;
using System;

public partial class Main : Node3D
{
    [Export] AudioStreamPlayer audioStreamPlayer;
    public override void _Ready()
    {
        GetTree().Paused = true;
    }

}
