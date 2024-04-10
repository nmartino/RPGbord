using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> containers;

    public override void _Ready()
    {
        containers = GetChildren().Where(
            (element) => element is UIContainer
            ).Cast<UIContainer>().ToDictionary(
                (element) => element.container 
                 );

        containers[ContainerType.Start].Visible = true;

        containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPRessed;
        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnVictory += HandleVictory;
    }

    private void HandleVictory()
    {
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Victory].Visible = true;
        GetTree().Paused = true;
    }


    private void HandleEndGame()
    {
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Defeat].Visible = true;
    }


    private void HandleStartPRessed()
    {
        GetTree().Paused = false;
        
        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;

        GameEvents.RaiseStartGame();
    }

}
