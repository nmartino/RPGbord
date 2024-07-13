using Godot;
using System;

public partial class UIContainer : Container
{
    [Export] public ContainerType container{get; private set;}
    [Export] public Button ButtonNode {get; private set;}
    [Export] public Button ButtonExit {get; private set;}
    [Export] public Button ButtonTeclas {get; private set;}
    [Export] public TextureRect TextureNode{get; private set;}
    [Export] public Label LabelNode {get; private set;}
    [Export] public AudioStreamPlayer2D AudioNode {get; private set;}
    
}
