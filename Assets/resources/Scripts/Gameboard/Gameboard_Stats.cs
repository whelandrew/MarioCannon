using UnityEngine;
using System.Collections;
using System;
[System.Serializable]

public class Gameboard_Stats : Singleton<Gameboard_Stats> 
{
	protected Gameboard_Stats () {} // guarantee this will be always a singleton only - can't use the constructor!
	
	public int levelDifficulty;
	
	public Language language = new Language();
	
	void Awake () 
	{
		// Your initialization code here
	}
}