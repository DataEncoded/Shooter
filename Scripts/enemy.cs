using Godot;
using Godot.Collections;
using System;

public partial class enemy : Area2D
{
	[Export]
	public float Speed {get; set;} = 100f;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Array<Node> playerGroup = GetTree().GetNodesInGroup("Player");

		if (playerGroup.Count == 1)
		{
			Node player = playerGroup[0];
			if (player is Node2D)
			{
				Node2D player2D = player as Node2D;
				LookAt(player2D.Position);
				Rotate(-Mathf.Pi/2);

				GlobalPosition += new Vector2(Speed * (float)delta, 0).Rotated(Rotation + (Mathf.Pi/2));

			}
		}
	}

    private void OnAreaEntered(Area2D area)
	{
		if (area.GetParent().IsClass("CharacterBody2D"))
		{
			QueueFree();
			area.GetParent().QueueFree();
		}
	}
}
