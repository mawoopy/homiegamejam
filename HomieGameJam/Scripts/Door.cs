using Godot;
using System;

public class Door : Area2D
{
[Export]
public string SceneToLoad;

[Export]
public Vector2 EntrancePosition;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetNode<SceneManager>("/root/SceneManager").AddDoorPosition(EntrancePosition);
    }

    public void LoadScene()
    {
        GD.Print("KILL YOURSELF");
        GetNode<SceneManager>("/root/SceneManager").ChangeScene((SceneNames)Enum.Parse(typeof(SceneNames), SceneToLoad));   
    }


}
