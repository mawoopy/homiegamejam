using Godot;
using System;
using System.Collections.Generic;

public enum SceneNames
{
    StartMenu = 0,
    MainScene = 1,
    Interior = 2, //Rename
    F_RoomA = 3,
    FredTest = 4,
}
public class SceneManager : Node2D
{
    public static SceneManager Instance;

    public Dictionary<SceneNames, SceneData> SceneDictionary = new Dictionary<SceneNames, SceneData>()
    {
        {SceneNames.StartMenu, new SceneData("res://Scenes/Levels/TestSceneSwitch.tscn", "Start Menu", false)},
        {SceneNames.MainScene, new SceneData("res://Scenes/Levels/TestSceneSwitch2.tscn", "Main Scene", false)},
        {SceneNames.Interior, new SceneData("res://Scenes/Levels/TestSceneSwitch3.tscn", "Interior", false)},
        {SceneNames.F_RoomA, new SceneData("res://Scenes/Levels/F_RoomA.tscn", "F_RoomA", false)},
        {SceneNames.FredTest, new SceneData("res://Scenes/Levels/FredTest.tscn", "FredTest", false)},
    };

    

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if(Instance != null)
        {
            //QueueFree();
            return;
        }
        else
        {
            Instance = this; 
        }
        //GetTree().ChangeSceneToFile("res://Scenes/Levels/TestSceneSwitch2.tscn"); Only from Godot 4
    }


    public void OnButtonPressed()
    {
        //GD.Print("Button Pressed");
        ChangeScene(SceneNames.MainScene);
    }
    public void ChangeScene(SceneNames sceneName)
    {
       string scenePath = SceneDictionary[sceneName].Path;
       GetTree().ChangeScene(scenePath);
       //GD.Print("Scene Changed to: " + sceneName.ToString());
    }

}
