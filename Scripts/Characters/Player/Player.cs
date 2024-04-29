using Godot;
using System;

public partial class Player : Character
{
    public override void _Ready()
    {
        base._Ready();
        GameEvents.OnReward += HandeReward;
    }




    public override void _Input(InputEvent @event)
        {
            direction = Input.GetVector(
                GameConstants.INPUT_MOVE_LEFT, GameConstants.INPUT_MOVE_RIGHT, GameConstants.INPUT_MOVE_FOWARD, GameConstants.INPUT_MOVE_BACKWARD
            );
        }

    private void HandeReward(RewardResource resource)
    {
        StatResource targetStat = GetStatResource(resource.TargetStat);

        targetStat.StatValue += resource.Amount;
    }

    
}

