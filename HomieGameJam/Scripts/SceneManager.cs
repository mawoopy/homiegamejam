using Godot;
using System;
using System.Collections.Generic;

public enum SceneNames
{
    StartMenu = 0,
    MainScene = 1,
    Interior = 2, //Rename
}
public class SceneManager : Node2D
{
    public static SceneManager Instance;

    public Dictionary<SceneNames, SceneData> SceneDictionary = new Dictionary<SceneNames, SceneData>()
    {
        {SceneNames.StartMenu, new SceneData("res://Scenes/Levels/TestSceneSwitch.tscn", "Start Menu", false)},
        {SceneNames.MainScene, new SceneData("res://Scenes/Levels/TestSceneSwitch2.tscn", "Main Scene", false)},
        {SceneNames.Interior, new SceneData("res://Scenes/Levels/TestSceneSwitch3.tscn", "Interior", false)},
    };
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if(Instance != null)
        {
            QueueFree();
            return;
        }
        else
        {
            Instance = this; 
        }
        //GetTree().ChangeSceneToFile("res://Scenes/Levels/TestSceneSwitch2.tscn"); Only from Godot 4
    }
    public void OnButtonDown()
    {
        ChangeScene(SceneNames.MainScene);
        GD.Print("Button Pressed");
    }
    public void ChangeScene(SceneNames sceneName)
    {
       string scenePath = SceneDictionary[sceneName].Path;
       GetTree().ChangeScene(scenePath);
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
