using Godot;
using System;

public partial class Coin : Area2D
{
    public override void _Ready()
    {
        // Automatically connect the signal via code
        BodyEntered += _OnBodyEntered;
    }

    public void _OnBodyEntered(Node2D body)
    {
        // 1. Find the Level Root
        // We use "tutorial_level" here because that is the name of your class
        TutorialLevel level = GetOwner<TutorialLevel>();

        // 2. Check if we found it, then add the coin
        if (level != null)
        {
            level.AddCoin();
        }

        // 3. Destroy the coin
        QueueFree();
    }
}