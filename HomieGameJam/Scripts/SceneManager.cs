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
public class PlayerData
{
    public Vector2 Position;
    public DeathState DeathState;
    public PlayerData(Vector2 position, DeathState deathState)
    {
        Position = position;
        DeathState = deathState;
    }

}
public class SceneManager : Node2D
{
    private PlayerData _playerData;
    private int _currentDoorId;
    public static SceneManager Instance;
    //public List<Vector2> DoorPositions = new List<Vector2>();
    [Export]
    public Dictionary<int, Vector2> DoorPositions = new Dictionary<int, Vector2>();
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
        //_playerData= new PlayerData(GetNode<Player>("/root/Player").Position, Player.CurrentDeathState);
        _playerData= new PlayerData(GetTree().CurrentScene.GetNode<Player>("Player").Position, Player.CurrentDeathState);

        if (Instance != null)
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
        Timer timer = new Timer();
        timer.WaitTime = 0.3f; // Set the delay time in seconds
        timer.OneShot = true; // Set the timer to run only once
        _currentDoorId = GetTree().CurrentScene.GetNode<Door>("Door").Id;
        MovePlayer();
        
        //Pass after loading position 
        //GD.Print("Scene Changed to: " + sceneName.ToString());
    }

    private void MovePlayer()
    {
        //_currentDoorId =GetTree().CurrentScene.GetNode<Door>("Door").Id;
        //Switch Logic to work with PLayerData (stored in SceneManager), find player in the scene and set its position to the corresponding door position based on the current door ID.
        //Player.Instance.Position = DoorPositions.TryGetValue(_currentDoorId, out Vector2 position) ? position : Player.Instance.Position;
        //Player player = GetNode<Player>("/root/Player");
        Player player = GetTree().CurrentScene.GetNode<Player>("Player");
        Player.CurrentDeathState = _playerData.DeathState;
        //player.Position = DoorPositions.TryGetValue(_currentDoorId, out Vector2 position) ? position : player.Position;
        //player.GetNode<Node2D>("Node").Position = DoorPositions.TryGetValue(_currentDoorId, out Vector2 position) ? position : player.Position;
        player.Transform = new Transform2D(player.Transform.Rotation, DoorPositions.TryGetValue(_currentDoorId, out Vector2 position) ? position : player.Position);

        GD.Print("Player Position Set to: " + player.Position.ToString());
    }

    public void SetCurrentDoorId(int doorId)
    {
        _currentDoorId = doorId;
        GD.Print("Called from SceneManager - Currenr Door ID Set to: " + _currentDoorId);
    }
    public void AddDoorPosition(int id,Vector2 position)
    {
        GD.Print("Door Position Added: " + position.ToString());
    }

}
