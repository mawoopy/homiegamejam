using Godot;
using System;


public class DrPhil : Node2D
{
   
   [Export]
   public bool IsTalking = false;
   public bool IsDead = false;
   Timer TalkDurationTimer = new Timer();
   Timer StartTalkTimer = new Timer();


	[Export]
	public  AudioStream[] voiceClips = new AudioStream[5];

	private AudioStreamPlayer2D _audioStreamPlayer = new AudioStreamPlayer2D(); 

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TalkDurationTimer = GetNode<Timer>("TalkDurationTimer");
		StartTalkTimer = GetNode<Timer>("StartTalkTimer");
	   
		_audioStreamPlayer = GetTree().CurrentScene.GetNode<AudioStreamPlayer2D>("VoiceSource");

	   // _audioStreamPlayer = GetNode<AudioStreamPlayer>("VoiceSource");

		RunAudioClipByIndex(0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		if (!IsDead)
		{
			if (IsTalking)
			{
				GetNode<AnimatedSprite>("Are/AnimatedSprite").Play("talk");
			}
			else
			{
				GetNode<AnimatedSprite>("Are/AnimatedSprite").Play("idle");
			}
		}
		else
		{
			GetNode<AnimatedSprite>("Are/AnimatedSprite").Play("dead");
		}
		

	}

	public void OnStartTalkTimerTimeout()
	{
		RunRandomAudioClip();
		GD.Print("Start Talk Timer Timeout");
	}

	 public void OnTalkDurationTimerTimeout()
	{
		GD.Print("Talk Duration Timer Timeout");
		IsTalking = false;
		StartTalkTimer.Start((float)GD.RandRange(5, 10)); // Start the timer again for the next talk
		TalkDurationTimer.Stop();
	}

	public void RunRandomAudioClip()
	{

		int randomIndex = new Random().Next(0, voiceClips.Length);

		_audioStreamPlayer.Stream = voiceClips[randomIndex];

		float audioDuration = _audioStreamPlayer.Stream.GetLength();

		TalkDurationTimer.Start(audioDuration);
		
		IsTalking = true;
		_audioStreamPlayer.Play();
	}

	public void RunAudioClipByIndex(int index)
	{
		_audioStreamPlayer.Stream = voiceClips[index];
		float audioDuration = _audioStreamPlayer.Stream.GetLength();

		TalkDurationTimer.Start(audioDuration);
		
		IsTalking = true;
		_audioStreamPlayer.Play();
		StartTalkTimer.Stop();
	}

	
}
