using Godot;
using System;
using System.Collections.Generic;

public partial class DialogueDisplay : PanelContainer
{
    private static Dictionary<string, Texture2D> _LOADED_ICONS
    = new Dictionary<string, Texture2D>();
    [Export] private TextureRect _icon;
    [Export] private Label _label;
    public override void _Ready()
    {
        Hide();
    }

    public void ShowDialogue(string npc, string text)
    {
        Show();
        Texture2D tex;
        if(!_LOADED_ICONS.TryGetValue(npc, out tex))
        {
            tex = GD.Load<Texture2D>($"res://Assets/Sprites/Characters/Icons/{npc}.png");
            _LOADED_ICONS[npc] = tex;
        }
        _icon.Texture = tex;
        _label.Text = text;
    }
}
