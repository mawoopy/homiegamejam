using Godot;
using System;



public enum DeathState
{
	Phase1,
	Phase2,
	Phase3,
	Phase4,
}
public class Player : KinematicBody2D
{
	public static Player Instance; // Singleton instance of the player.
	public static DeathState CurrentDeathState;
	public Vector2 ScreenSize; // Size of the game window.
	
	[Export]
	public int Speed {get; set;} = 70; // How fast the player will move (pixels/sec).
	[Export]
	public float JumpSpeed {get; set;} = 30; 
	[Export]
	public int Gravity {get; set;} = 1750; 

	[Signal]
	public delegate void EnterRoomEventHandler(); // Signal for entering a room.

	private Vector2 _velocity = new Vector2(); // The player's movement vector.

	private AnimatedSprite _animatedSprite;

	private float _jumpHeight = 100f;

	private Vector2 _jumpTargetPosition;

	public bool CanInteract = false; 

	public override void _Ready()
	{
		if(Instance != null)
        {
        	QueueFree();

        }
        else
        {
            Instance = this; 
        }
		ScreenSize = GetViewportRect().Size;
	
		_animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		
		_animatedSprite.Play("idle");
		//GD.Print(Position.ToString());
	}
	

	public override void _Process(float delta)
	{
		//GD.Print(IsOnFloor());

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
		//GD.Print("Player Position: " + Position.ToString());
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


			if(GetNode<Area2D>("InteractCheck").GetOverlappingAreas().Count > 0)
			{

				CanInteract = true;
				//GD.Print("Can Interact");
				
			}
			else
			{
				CanInteract = false;
			}
			

			if(Input.IsActionJustPressed("jump") && CanInteract || Input.IsActionJustPressed("move_up") && CanInteract)
			{
				EmitSignal("EnterRoomEventHandler");


			}
			else if(Input.IsActionJustPressed("jump") && Input.IsActionPressed("move_down"))
			{
				if (GetNode<Area2D>("FallableCheck").GetOverlappingAreas().Count > 0)
				{
					GetNode<CollisionShape2D>("Hitbox").Disabled = true;
					_jumpTargetPosition = new Vector2(Position.x, Position.y +_jumpHeight/2);
					Position = Position.LinearInterpolate(_jumpTargetPosition, 0.5f);
				}
				
				
			}
			else if (Input.IsActionJustPressed("jump"))
			{
				//_velocity.y = -JumpSpeed;
				//MoveAndCollide(new Vector2(0, -JumpSpeed));

				GetNode<CollisionShape2D>("Hitbox").Disabled = true;
				Vector2 jumpTargetPosition = new Vector2(Position.x, Position.y - _jumpHeight);
				Position = Position.LinearInterpolate(jumpTargetPosition, 0.5f);

				if (Position.y >= jumpTargetPosition.y)
				{
					GetNode<CollisionShape2D>("Hitbox").Disabled = false;
				}
				
			}
			
			
		}
		


			
		if (Position.y <= _jumpTargetPosition.y)
				{
					GetNode<CollisionShape2D>("Hitbox").Disabled = false;
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
		MoveAndSlide(_velocity, Vector2.Up,infiniteInertia:false);}

		


		 

	}

	

	


