using Godot;
using System;

public class Door : Area2D
{
[Export]
public string SceneToLoad;

[Export]
public Vector2 ENtrancePosition;
[Export]
public int Id;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        GD.Print("Door Ready: " + SceneToLoad + " Entrance Position: " + ENtrancePosition.ToString() + " ID: " + Id);
    }

    public void LoadScene()
    {
        GD.Print("KILL YOURSELF");
        GetNode<SceneManager>("/root/SceneManager").ChangeScene((SceneNames)Enum.Parse(typeof(SceneNames), SceneToLoad));   
        GetNode<SceneManager>("/root/SceneManager").SetCurrentDoorId(Id);
        GD.Print("Id Set to: " + Id);
    }


}
