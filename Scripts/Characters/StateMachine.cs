using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
    [Export] private Node currenState;
    [Export] private CharacterState[] states;

    public override void _Ready()
    {
        currenState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>()
    {
        CharacterState newState = states.Where((state) => state is T)
            .FirstOrDefault();

        if(newState == null){return;}

        if(currenState is T){return;}

        if(!newState.canTransition()){return;}

        currenState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        currenState = newState;
        currenState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);        
        
    }
}



