using UnityEngine;
using System.Collections;
using System;
[System.Serializable]

public class Globals : Singleton<Globals> 
{
	protected Globals () {} // guarantee this will be always a singleton only - can't use the constructor!

	public bool usingAccelerometer;

	public bool paused;

	public bool isSharkDefeated;

	public bool isPlayerInvincible;

	public float levelSpeed;
	public int levelDifficulty;

	public float backgroundSpeed;
	public float foregroundSpeed;
		
	public Language language = new Language();
	
	void Awake () 
	{
		// Your initialization code here
	}

	public void Pause(bool pauseStatus) 
	{
		paused = pauseStatus;
		if(pauseStatus)
			Time.timeScale=0;
		else
			Time.timeScale=1;
	}
}

public class Language 
{
	public string current;
	public string lastLang;
}
