using Godot;
using System;

public partial class Killzone : Area2D
{
    private Timer _timer; 

    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer"); 
        
        BodyEntered += _OnBodyEntered;
        _timer.Timeout += _OnTimerTimeout; 
    }

    public void _OnBodyEntered(Node2D body)
    {
        GD.Print("Player has entered the killzone. Respawning...");
        _timer.Start(); 
    }

    public void _OnTimerTimeout()
    {
        GetTree().ReloadCurrentScene();
    }
}