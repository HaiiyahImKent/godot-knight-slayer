using Godot;
using System;

public partial class TutorialLevel : Node2D
{
    // 1. This variable holds the connection to your HUD
    [Export]
    public Hud HudReference; 

    private int _score = 0;
    private int _health = 5;

    public override void _Ready()
    {
        GD.Print("Level initialized!");
    }

    // 2. This function is called by the Coin
    public void AddCoin()
    {
        _score += 1;
        UpdateHudScore(_score);
    }

    public void TakeDamage(int amount)
    {
        _health -= amount;
        if (_health < 0) _health = 0;

        UpdateHudHealth(_health);

        GD.Print($"Took damage. Health now: {_health}");
        

        if (_health == 0)
        {
            // Simple death handling: you can expand this to reload scene or respawn player
            GD.Print("Player died - implement respawn or game over logic here.");
        }
    }

    private void UpdateHudScore(int score)
    {
        // Prefer explicit HudReference if assigned
        if (HudReference != null)
        {
            HudReference.UpdateScore(score);
            return;
        }

        // Otherwise, find the HUD node by name and update the Label directly
        var hud = FindHudNode();
        if (hud != null)
        {
            // Label path used in `hud.tscn`: "Score Container/HBoxContainer/Label"
            var label = hud.GetNodeOrNull<Label>("Score Container/HBoxContainer/Label");
            if (label != null)
                label.Text = score.ToString();
        }
    }

    private void UpdateHudHealth(int health)
    {
        if (HudReference != null)
        {
            HudReference.UpdateHealth(health);
            return;
        }

        var hud = FindHudNode();
        if (hud != null)
        {
            var healthContainer = hud.GetNodeOrNull<Node>("Health Container");
            if (healthContainer != null)
            {
                var children = healthContainer.GetChildren();
                for (int i = 0; i < children.Count; i++)
                {
                    if (children[i] is Node2D child)
                        child.Visible = (i < health);
                }
            }
        }
    }

	private Node FindHudNode()
	{
		// Attempt to find the HUD node in the scene tree
		return GetTree().Root.GetNodeOrNull<Node>("Root/Hud");
	}
}