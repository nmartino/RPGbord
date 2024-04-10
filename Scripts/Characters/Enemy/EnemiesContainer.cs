using Godot;
using System;

public partial class EnemiesContainer : Node3D
{
    public override void _Ready()
    {
        int totalEnemies = GetChildCount();
        GameEvents.RaiseNewEnemyCount(totalEnemies);

        ChildExitingTree += HandleExitingTree;
    }

    private void HandleExitingTree(Node node)
    {
        int totalEnemies = GetChildCount()-1 ;
        GameEvents.RaiseNewEnemyCount(totalEnemies);

        if(totalEnemies == 0)
        {
            GameEvents.RaiseVictory();
        }
    }

}
