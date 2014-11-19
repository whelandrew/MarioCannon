using UnityEngine;
using System.Collections;
using System;
[System.Serializable]

//The Obstacle_Stats will store the details of the available obstacles encountered in a level set.
//The Obstacle_Stats will store the details of collectables in a level set.
public class ObstacleTypes
{
	public float damage;
	public float knockback;
	public bool destructable;
	public Particle explosion;
}

public class Obstacle_Stats : Singleton<Obstacle_Stats> 
{
	protected Obstacle_Stats () {} // guarantee this will be always a singleton only - can't use the constructor!
	
	public Language language = new Language();

	public float obstacleType;

	public ObstacleTypes[] obstacles;
}
