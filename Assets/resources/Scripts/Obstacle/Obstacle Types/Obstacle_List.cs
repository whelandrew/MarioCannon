
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemType{NULL,HAZARD_LOW,HAZARD_MID_HAZARD_HIGH,EXPLODER_LOW,EXPLODER_HIGH,INVINCIBLE,SUPERSHIELD,MULTIPLIER,POINTS};
public enum ReleaseType{RANDOM};

public class Obstacle_List
{
	public static Dictionary<string,ObstacleClass> obstacles=new Dictionary<string, ObstacleClass>()
	{
		//HAZARD_LOW
		{
			ItemType.HAZARD_LOW.ToString(),
			new ObstacleClass()
			{
				damage=1,
				currentPosition=Vector2.zero,
				explode=false,
				hp=1,
				isActive=true,
				itemType=ItemType.HAZARD_LOW,
				speed=5f,
				structureReleaseType=ReleaseType.RANDOM,
				yMovement=0
			}
		},

		//NULL
		{
			ItemType.NULL.ToString(),
			new ObstacleClass()
			{
				damage=0,
				currentPosition=new Vector2(-20,0),
				explode=false,
				hp=0,
				isActive=false,
				itemType=ItemType.NULL,
				speed=0,
				structureReleaseType=ReleaseType.RANDOM,
				yMovement=0
			}
		}
	};

	public static ObstacleClass GetClassType(string itemType)
	{
		//print ("LOOKING FOR "+itemType+"     "+ "RETURNING " + obstacles [itemType]); 
		return obstacles[itemType];
	}
}