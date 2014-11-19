using UnityEngine;
using System.Collections;
using System;
[System.Serializable]

public class Shark_Stats : Singleton<Shark_Stats> 
{
	protected Shark_Stats () {} // guarantee this will be always a singleton only - can't use the constructor!

	public float totalHealth;

	public float speed;

	public float waitTime;
	
	public Language language = new Language();
	
	void Awake () 
	{
		// Your initialization code here
	}
}
