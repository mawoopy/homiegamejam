using Godot;
using System;

public enum PlayerStates
{
    Phase1,
	Phase2,
	Phase3,
	Phase4,
}

public class PlayerStateChanger : Node
{

    public int PlayerState = 0;

    [Export]
    public  AnimatedSprite[] characterSprites = new AnimatedSprite[4];

    [Export]
    public  AudioStream[] Music = new AudioStream[5];

    
    

    public override void _Ready()
    {
        
    }


    public override void _Process(float delta)
    {
     
    }
}
