using Godot;
using System;

public partial class Score : Node
{
	public static Score Instance {get; private set;}

	public int score = 0;

	[Signal]
	public delegate void UpdateScoreEventHandler(int numberOfPoints);

	public string UpdateScoreSignal = SignalName.UpdateScore;

	private RichTextLabel textLabel; 

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;

		textLabel = GetTree().CurrentScene.GetNode<RichTextLabel>("Score");

		textLabel.Text = score.ToString();

		UpdateScore += updateScore;
	}

	private void updateScore(int numberOfPoints)
	{
		score += numberOfPoints;

		textLabel.Text = score.ToString();
	}
}
