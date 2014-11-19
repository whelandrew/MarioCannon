using UnityEngine;
using System.Collections;

public class ObstacleClass : MonoBehaviour 
{
	public GameObject obstacleObject;
	public bool isActive=false;
	public Vector2 currentPosition;
	
	public float speed;
	public float yMovement;
	public float hp;
	
	public float damage;
	public bool explode;
	
	public ItemType itemType;
	public ReleaseType structureReleaseType;

	public void Initialize(ObstacleClass fromList)
	{
		damage = fromList.damage;
		currentPosition = fromList.currentPosition;
		explode = fromList.explode;
		hp = fromList.hp;
		isActive = fromList.isActive;
		itemType = fromList.itemType;
		speed = fromList.speed;
		structureReleaseType = fromList.structureReleaseType;
		yMovement = fromList.yMovement;
	}
}