using Godot;
using System;

public partial class Score : RichTextLabel
{
	private int score = 0;

	[Signal]
	public delegate void UpdateScoreEventHandler(int numberOfPoints);

	public string UpdateScoreSignal = SignalName.UpdateScore;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Text = score.ToString();

		UpdateScore += updateScore;
	}

	private void updateScore(int numberOfPoints)
	{
		score += numberOfPoints;

		Text = score.ToString();
	}
}
