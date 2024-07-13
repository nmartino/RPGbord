using Godot;
using System;

public partial class StatLabel : Label
{
	[Export] private Stat statType;
	private StatResource statResource;
	public override void _Ready()
	{
		GameEvents.OnPlayerInitialized += HandlePlayerInitialized;
	}

	private void HandlePlayerInitialized(Player player)
	{
		statResource = player.GetStatResource(statType);
		statResource.OnUpdate += HandleUpdate;
		Text = statResource.StatValue.ToString();
	}

	private void HandleUpdate()
	{
		Text = statResource.StatValue.ToString();
	}
}
