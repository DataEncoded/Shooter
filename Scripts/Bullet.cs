using System;
using System.Runtime.CompilerServices;
using Godot;

public partial class Bullet : Projectile
{
	[Export]
	public float InitialSpeed {get;set;} = 6f;

	static PackedScene bulletScene = ResourceLoader.Load<PackedScene>("/Scenes/Bullet.tscn");

	Score gameScore;

	private float rotation = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		rotation = GlobalRotation;

		Speed = Mathf.Clamp(InitialSpeed + Speed, InitialSpeed, InitialSpeed*2);

		gameScore = GetTree().CurrentScene.GetNode<Score>("Score");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position -= Transform.Y * Speed;
		GlobalRotation = rotation;
	}

	public void OnAreaEntered(Area2D area)
	{
		if (!area.GetParent().IsClass("CharacterBody2D"))
		{
			// Enemy Hit
			area.QueueFree();
			QueueFree();

			if (area.IsInGroup("Enemy"))
			{
				// Add score
				if (gameScore != null)
				{
					gameScore.EmitSignal(gameScore.UpdateScoreSignal, 1);
				}
			}

			// Play explosion
		}
	}
}
