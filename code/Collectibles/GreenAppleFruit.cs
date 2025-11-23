using Godot;
using System;

public partial class GreenAppleFruit : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        GD.Print("Green Apple is ready and connected!");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_body_entered(Node2D body)
	{
		GD.Print("+1 Green Apple!");
		QueueFree();
	}
}
