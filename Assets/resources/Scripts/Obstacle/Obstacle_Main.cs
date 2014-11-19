using UnityEngine;
using System.Collections;

public class Obstacle_Main : MonoBehaviour 
{
	private ParticleSystem explodeParticle;
	private ObstacleClass obstacleClass;
	Globals global;
	Transform xForm;

	private void Awake()
	{
		global = Globals.Instance;
		xForm = GetComponent<Transform> ();
		obstacleClass = GetComponent<ObstacleClass> ();
		explodeParticle = GetComponentInChildren<ParticleSystem> ();
		print (explodeParticle);
		AssignObstacleData(Obstacle_List.GetClassType (ItemType.NULL.ToString()));
	}

	private void Update()
	{
		if (!global.isSharkDefeated) 
		{
			if(obstacleClass.isActive)
				if(xForm.position.x<-10)
					AssignObstacleData(Obstacle_List.GetClassType (ItemType.NULL.ToString()));
			else
				MoveObstacle(Time.deltaTime);
		}
	}

	public void AssignObstacleData(ObstacleClass newObstacle)
	{
		obstacleClass = GetComponent<ObstacleClass> ();

		obstacleClass.currentPosition = newObstacle.currentPosition;
		obstacleClass.damage = newObstacle.damage;
		obstacleClass.explode = newObstacle.explode;
		obstacleClass.hp = newObstacle.hp;
		obstacleClass.isActive = newObstacle.isActive;
		obstacleClass.itemType = newObstacle.itemType;
		obstacleClass.obstacleObject = newObstacle.obstacleObject;
		obstacleClass.speed = newObstacle.speed;
		obstacleClass.structureReleaseType = newObstacle.structureReleaseType;
		obstacleClass.yMovement = newObstacle.yMovement;

		transform.position = obstacleClass.currentPosition;
	}

	void OnTriggerEnter2D(Collider2D collision) 
	{
		Debug.Log("Obstacle Collided with "+collision.gameObject.tag);
	}

	public void MoveObstacle(float _time)
	{
		float newSpeed=_time*(obstacleClass.speed*Globals.Instance.levelSpeed);
		transform.Translate(Vector3.left*newSpeed);
	}

	public void Explode()
	{
		explodeParticle.transform.position = transform.position;
		explodeParticle.Emit (20);
	}
}
