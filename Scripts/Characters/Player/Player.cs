using Godot;
using System;

public partial class Player : Character
{
    public override void _Ready()
    {
        base._Ready();
        GameEvents.OnReward += HandleReward;
        GameEvents.PlayerInitialized(this);
    }




    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FOWARD, GameConstants.INPUT_MOVE_BACKWARD
        );
    }

    private void HandleReward(RewardResource resource)
    {
        StatResource targetStat = GetStatResource(resource.TargetStat);

        targetStat.StatValue += resource.Amount;
    }

    public void ClampToCube(Vector3 cubePosition, Vector3 cubeSize)
    {
        Vector3 newPosition = GlobalPosition;
        newPosition.X = Mathf.Clamp(GlobalPosition.X, cubePosition.X - cubeSize.X / 2, cubePosition.X + cubeSize.X / 2);
        newPosition.Y = Mathf.Clamp(GlobalPosition.Y, cubePosition.Y - cubeSize.Y / 2, cubePosition.Y + cubeSize.Y / 2);
        newPosition.Z = Mathf.Clamp(GlobalPosition.Z, cubePosition.Z - cubeSize.Z / 2, cubePosition.Z + cubeSize.Z / 2);
        GlobalPosition = newPosition;
    }

}
