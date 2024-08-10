using System;
using Godot;
using System.Linq;

public abstract partial class Character : CharacterBody3D
{
    [Export] private StatResource[] stats;

    [ExportGroup("Required Nodes")]
    [Export] public AnimationPlayer AnimPlayerNode{get; private set;}
    [Export] public Sprite3D SpriteNode{get; private set;}
    [Export] public ProgressBar healthBar{get; private set;}
    [Export] public StateMachine StateMachineNode{get; private set;}
    [Export] public Area3D HurtBoxNode {get; private set;}
    [Export] public Area3D HitBoxNode {get; private set;}
    [Export] public CollisionShape3D HitBoxShapeNode {get; private set;}
    [Export] public Timer ShaderTimerNode {get; private set;}
    

    [ExportGroup("AI Nodes")]
    [Export] public Path3D PathNode {get; private set;}
    [Export] public NavigationAgent3D AgentNode {get; private set;}
    [Export] public Area3D ChaseAreaNode {get; private set;}
    [Export] public Area3D AttackAreaNode {get; private set;}


    public Vector2 direction = new();
    private ShaderMaterial shader;
    public bool isFlip = false;

    public override void _Ready()
    {
        shader = (ShaderMaterial)SpriteNode.MaterialOverlay;
        healthBar.MaxValue = GetStatResource(Stat.Health).StatValue;
        HurtBoxNode.AreaEntered += HandleHurtboxEntered;
        SpriteNode.TextureChanged += HandleTextureChange;
        ShaderTimerNode.Timeout += HandleShaderTimeOut;
    }

    private void HandleShaderTimeOut()
    {
        shader.SetShaderParameter("active",false);
    }

    public void Flip()
    {
        bool isNotMovingHorizontally = Velocity.X == 0;

        if (isNotMovingHorizontally) { return; }

        bool isMovingLeft = Velocity.X < 0;

        isFlip = isMovingLeft;
        var targetYRotation = isFlip ? 180 : 0;
        SpriteNode.RotationDegrees = new Vector3(SpriteNode.RotationDegrees.X,
         Mathf.MoveToward(SpriteNode.RotationDegrees.Y, targetYRotation, 50),
         SpriteNode.RotationDegrees.Z);

    }

    private void HandleHurtboxEntered(Area3D area)
    {
        if(area is not IHitBox hitBox){return;}

        StatResource health = GetStatResource(Stat.Health);
        float damage = hitBox.GetDamage();
        health.StatValue -= damage;
        healthBar.Value = health.StatValue;
        shader.SetShaderParameter("active", true);
        ShaderTimerNode.Start();
    }

        private void HandleTextureChange()
    {
        shader.SetShaderParameter(
            "tex", SpriteNode.Texture
        );
    }

    public StatResource GetStatResource(Stat stat)
    {
       return stats.Where((element) => element.StatType == stat)
            .FirstOrDefault();
    }

    public void ToggleHitBox(bool flag)
    {
        HitBoxShapeNode.Disabled = flag;
    }
}