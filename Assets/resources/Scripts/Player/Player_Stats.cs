using UnityEngine;
using System.Collections;
using System;
[System.Serializable]

public class Player_Stats : Singleton<Player_Stats> 
{
	protected Player_Stats () {} // guarantee this will be always a singleton only - can't use the constructor!
	
	//how many hearts of health
	public float totalHealth;
	
	//can be used to increase or decrease health based on imported stats
	public float healthBonus;
	
	public float currentHealth;
	
	//how many hits has the heart taken. After one element==0 begin reading the next element. When all are 0 then the game is over.
	//public int[] healthConditions=new int[totalHealth];
	
	//armor value defines how many hits can be taken before losing a health point from 4
	public float armorMax;
	
	public float speed;
	
	public float attackPower;

	public Language language = new Language();

	void Awake () 
	{
		// Your initialization code here
	}
}
