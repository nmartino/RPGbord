using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UIController : Control
{
    public bool canPause = false;
    private Dictionary<ContainerType, UIContainer> containers;

    public override void _Ready()
    {
        containers = GetChildren().Where(
            (element) => element is UIContainer
            ).Cast<UIContainer>().ToDictionary(
                (element) => element.container
        );

        containers[ContainerType.Start].Visible = true;


        containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;
        containers[ContainerType.Start].ButtonTeclas.Pressed += HandleTeclasPressed;
        containers[ContainerType.Pause].ButtonNode.Pressed += HandlePausePressed;
        containers[ContainerType.Pause].ButtonExit.Pressed += HandleExitPressed;
        containers[ContainerType.Teclas].ButtonNode.Pressed += HandleBackPressed;
        containers[ContainerType.Victory].ButtonExit.Pressed += HandleExitPressed;
        containers[ContainerType.Defeat].ButtonExit.Pressed += HandleExitPressed;
        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnVictory += HandleVictory;

        GameEvents.OnReward += HandleReward;
        containers[ContainerType.Reward].ButtonNode.Pressed += HandleRewardPressed;

    }

    private void HandleResetPressed()
    {
        GetTree().ReloadCurrentScene();
        GetTree().Paused = false;
    }


    private void HandleTeclasPressed()
    {
        containers[ContainerType.Start].AudioNode.Play();
        canPause = false;
        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Teclas].Visible = true;
    }


    private void HandleBackPressed()
    {
        containers[ContainerType.Teclas].AudioNode.Play();
        canPause = false;
        containers[ContainerType.Teclas].Visible = false;
        containers[ContainerType.Start].Visible = true;
    }


    private void HandleExitPressed()
    {
        canPause = false;
        GetTree().Quit();
        
    }


    public override void _Input(InputEvent @event)
    {
        if (!canPause) { return; }
        if (!Input.IsActionJustPressed(GameConstants.INPUT_PAUSE))
        {
            return;
        }
        containers[ContainerType.Pause].AudioNode.Play();
        containers[ContainerType.Stats].Visible = GetTree().Paused;
        GetTree().Paused = !GetTree().Paused;
        containers[ContainerType.Pause].Visible = GetTree().Paused;
    }

    private void HandleVictory()
    {
        canPause = false;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Victory].Visible = true;
        GetTree().Paused = true;
    }

    private void HandleEndGame()
    {
        canPause = false;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Defeat].Visible = true;
    }

    private void HandleReward(RewardResource resource)
    {
        canPause = false;
        GetTree().Paused = true;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Reward].Visible = true;
        containers[ContainerType.Reward].TextureNode.Texture = resource.SpriteTexture;
        containers[ContainerType.Reward].LabelNode.Text = resource.Description;

    }

    private void HandleStartPressed()
    {
        containers[ContainerType.Start].AudioNode.Play();
        canPause = true;
        GetTree().Paused = false;

        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;

        GameEvents.RaiseStartGame();
    }

    private void HandlePausePressed()
    {
        containers[ContainerType.Pause].AudioNode.Play();
        GetTree().Paused = false;
        containers[ContainerType.Pause].Visible = false;
        containers[ContainerType.Stats].Visible = true;
    }

    private void HandleRewardPressed()
    {
        containers[ContainerType.Reward].AudioNode.Play();
        canPause = true;
        GetTree().Paused = false;
        containers[ContainerType.Stats].Visible = true;
        containers[ContainerType.Reward].Visible = false;
    }

}
