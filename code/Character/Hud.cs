using Godot;
using System;

public partial class Hud : CanvasLayer
{
    // --- VARIABLES ---
    
    // 1. Drag your "Label" node here in the Inspector
    [Export] 
    public Label ScoreLabel; 

    // 2. Drag your "HealthContainer" (HBoxContainer) here
    //    (The one holding the 5 heart sprites)
    [Export] 
    public HBoxContainer HealthContainer;


    // --- FUNCTIONS ---

    public override void _Ready()
    {
        // Optional: Test code to see if it works when the game starts
        // UpdateScore(0);
        // UpdateHealth(5);
    }

    // Call this from your Level/GameManager script
    public void UpdateScore(int score)
    {
        // Updates the text to match the score
        ScoreLabel.Text = score.ToString();
    }

    // Call this from your Player or Level script when taking damage
    public void UpdateHealth(int currentHealth)
    {
        // Get the list of all heart sprites
        var hearts = HealthContainer.GetChildren();

        // Loop through every heart slot (0 to 4)
        for (int i = 0; i < hearts.Count; i++)
        {
            // We treat the child as a Node2D (AnimatedSprite2D inherits from this)
            Node2D heart = (Node2D)hearts[i];

            // If the index is less than health, show the heart.
            // Example: Health is 3. Indices 0, 1, 2 are Visible. Indices 3, 4 are Hidden.
            if (i < currentHealth)
            {
                heart.Visible = true;
            }
            else
            {
                heart.Visible = false; 
                // Hint: If you have an "Empty Heart" animation, do this instead:
                // ((AnimatedSprite2D)heart).Play("empty");
            }
        }
    }
}