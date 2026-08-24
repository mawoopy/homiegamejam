using Godot;
using System;

public class PlayerRigid : RigidBody2D
{
    
	public Vector2 ScreenSize; // Size of the game window.


	[Export]
	public int Speed {get; set;} = 400; // How fast the player will move (pixels/sec).
	[Export]
	public float JumpSpeed {get; set;} = -400; 

	public const float Gravity = 9000; 

	private Vector2 _velocity = new Vector2(); // The player's movement vector.

	private AnimatedSprite _animatedSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;

		_animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		
		_animatedSprite.Play("idle");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{

		

		
		
	  
	}

	public override void _PhysicsProcess(float delta)
	{
		_velocity = Vector2.Zero;


        

	/* 	if(!IsOnFloor())
		{
			_velocity.y += Gravity * (float)delta;
		}

		if (Input.IsActionPressed("jump") && IsOnFloor())
		{
			_velocity.y = JumpSpeed;
		} */
		

		if (Input.IsActionPressed("move_right"))
		{
			_velocity.x += Speed;
		}
		else if (Input.IsActionPressed("move_left"))
		{
			_velocity.x -= Speed;
		}


		Position += _velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.x, 0, ScreenSize.x),
			y: Mathf.Clamp(Position.y, 0, ScreenSize.y)
		);

		if (_velocity.x != 0)
		{
			_animatedSprite.Animation = "walk";
			_animatedSprite.FlipV = false;
			_animatedSprite.FlipH = _velocity.x < 0;
		}
		else if (_velocity.y != 0)
		{
			_animatedSprite.Animation = "fall";
		}
		else
		{
			_animatedSprite.Play("idle");
		}

	}


    private void OnBodyEntered(Node body)
    {
       
    }

}
