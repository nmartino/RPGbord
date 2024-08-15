using Godot;
using System;

public partial class EnemiesContainer : Node3D
{
    public override void _Ready()
    {
        RaiseEnemyCount();
    }

    private void HandleExitingTree(Node node)
    {
        int totalEnemies = GetChildCount() - 1;
        GameEvents.RaiseNewEnemyCount(totalEnemies);
    }

    public void RaiseEnemyCount()
    {
        int totalEnemies = GetChildCount();
        Console.WriteLine("Total Enemies: " + totalEnemies);
        GameEvents.RaiseNewEnemyCount(totalEnemies);

        ChildExitingTree += HandleExitingTree;
    }
}
