using Godot;
using System;

public partial class Killzone : Area2D
{
    private Timer _timer;

    private const float SlowmoScale = 0.25f;
    private const float NormalScale = 1.0f;

    public override void _Ready()
    {
        if (HasNode("Timer"))
        {
            _timer = GetNode<Timer>("Timer");
            _timer.Timeout += _OnTimerTimeout;
        }

        BodyEntered += _OnBodyEntered;
    }

    public void _OnBodyEntered(Node2D body)
    {
        GD.Print("Hit a hazard!");

        // Get the level
        TutorialLevel level = GetTree().CurrentScene as TutorialLevel;

        if (level == null)
        {
            GD.PrintErr("Killzone Error: Could not find TutorialLevel.");
            return;
        }

        // Deal damage
        level.TakeDamage(1);

        // --------------------------------------
        // FIND PLAYER WITHOUT USING PlayerRef
        // --------------------------------------
        var player = level.GetNodeOrNull<Node2D>("Player");

        if (player == null)
        {
            GD.PrintErr("Killzone Error: Could not find Player node in scene!");
            return;
        }

        // --------------------------------------
        // FIND SPAWN POINT AUTOMATICALLY
        // --------------------------------------
        var spawn = level.GetNodeOrNull<Node2D>("SpawnPoint");

        if (spawn == null)
        {
            GD.PrintErr("Killzone Error: Could not find SpawnPoint node!");
            return;
        }

        // Teleport Player
        player.GlobalPosition = spawn.GlobalPosition;

        // Reset velocity if using CharacterBody2D
        if (player is CharacterBody2D cb)
            cb.Velocity = Vector2.Zero;

        // Enable slow motion
        Engine.TimeScale = SlowmoScale;

        if (_timer != null)
            _timer.Start();
    }

    public void _OnTimerTimeout()
    {
        Engine.TimeScale = NormalScale;
    }
}
