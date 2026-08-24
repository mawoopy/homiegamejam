using Godot;
using System;

public class Player : KinematicBody2D
{
	public Vector2 ScreenSize; // Size of the game window.

	[Export]
	public int Speed {get; set;} = 100; // How fast the player will move (pixels/sec).
	[Export]
	public float JumpSpeed {get; set;} = 500; 
	[Export]
	public int Gravity; 

	private Vector2 _velocity = new Vector2(); // The player's movement vector.

	private AnimatedSprite _animatedSprite;

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;

		_animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		
		_animatedSprite.Play("idle");

	}


	public override void _Process(float delta)
	{
		GD.Print(IsOnFloor());

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

	public override void _PhysicsProcess(float delta)
	{
		

		if(!IsOnFloor())
		{
			//_velocity.y += Gravity * (float)delta;
			_velocity.x = 0;
		}
		else
		{
			GD.Print("On Floor");
			_velocity.y = 0;
			if (Input.IsActionPressed("move_right"))
			{
			_velocity.x += 1;
			}
			else if (Input.IsActionPressed("move_left"))
			{
			_velocity.x -= 1;
			}
			else
			{
				_velocity.x = 0;
			}


			if (Input.IsActionJustPressed("jump"))
			{
				//_velocity.y = -JumpSpeed;
				
				MoveAndCollide(new Vector2(0, -JumpSpeed));
				
			}
			
			
		}







		/* Position += _velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.x, 0, ScreenSize.x),
			y: Mathf.Clamp(Position.y, 0, ScreenSize.y)
		); */

		// if (_velocity.x != 0)
		// {
		// 	_animatedSprite.Animation = "walk";
		// 	_animatedSprite.FlipV = false;
		// 	_animatedSprite.FlipH = _velocity.x < 0;
		// }
		// else if (_velocity.y != 0)
		// {
		// 	_animatedSprite.Animation = "fall";
		// }
		// else
		// {
		// 	_animatedSprite.Play("idle");
		// }
		if (IsOnFloor())
		{
			_velocity  = _velocity.Normalized() * Speed;			
		}
		else
		{
			_velocity.x = 0;
			_velocity.y += Gravity * (float)delta;
		}
		//MoveAndCollide(_velocity * (float)delta);
		MoveAndSlide(_velocity, Vector2.Up,infiniteInertia:false);
		GD.Print(_velocity);
		

	}

	

	

}
