using Godot;

public partial class Ship : CharacterBody2D
{
	[Export]
	public float RotationSpeed { get;set; } = 3f;
	[Export]
	public float MaxSpeed {get;set;} = 90f;
	[Export]
	public float ForwardAcceleration {get;set;} = 1f;
	[Export]
	public float StoppingForce {get;set;} = 3f;

	[Export]
	public AnimatedSprite2D Sprite {get;set;} = null;
	[Export]
	public float ShotCooldown {get;set;} = 0.3f;
	[Export]
	public PackedScene ProjectileScene {get;set;} = null;

	private Timer ShootTimer = new Timer();

	float forwardSpeed;

    public override void _Ready()
    {
        AddChild(ShootTimer);
		ShootTimer.OneShot = true;
    }

    Vector2 getInput(float delta)
	{
		if (Input.IsActionPressed("move_backward"))
		{
			forwardSpeed = Mathf.Lerp(forwardSpeed, 0, StoppingForce * delta);
			if (forwardSpeed < 3)
			{
				forwardSpeed = 0;
			}
		}
		if (Input.IsActionPressed("move_forward"))
		{
			forwardSpeed = Mathf.Lerp(forwardSpeed, MaxSpeed, ForwardAcceleration * delta);
		}

		float rotationDirection = Input.GetAxis("rotate_left", "rotate_right");

		return new Vector2(forwardSpeed, rotationDirection);
	}

    public override void _PhysicsProcess(double delta)
    {
		Vector2 input = getInput((float)delta);

		Velocity = -Transform.Y * input.X;

		Rotation += input.Y * RotationSpeed * (float)delta;

		if (Input.GetAxis("move_forward", "move_backward") == 0 && input.Y == 0)
		{
			Sprite.Animation = "idle";
		} else {
			Sprite.Animation = "thrust";
		}

		if (Input.IsActionPressed("shoot") && ShootTimer.IsStopped())
		{
			ShootTimer.Start(ShotCooldown);

			Projectile projectile = Projectile.Create(ProjectileScene, forwardSpeed/30);
			projectile.Position = Position - (Transform.Y * 3);
			projectile.Rotation = Rotation;

			GetWindow().AddChild(projectile);
		}
		
		MoveAndSlide();

    }


}
