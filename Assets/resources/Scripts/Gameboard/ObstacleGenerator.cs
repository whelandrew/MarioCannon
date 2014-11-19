using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour 
{
	Globals global;
	public GameObject obstacleSpawnPoints;
	private Transform[] spawnPoints;
	private List<GameObject> obstacles=new List<GameObject>();
	private int maxObjectsReleased;

	private float timeToReach;
	private float dispertionRate;

	void Awake()
	{
		global=Globals.Instance;

		foreach(ObstacleClass sprite in GetComponentsInChildren(typeof(ObstacleClass)))
		{
			GameObject newObject=sprite.gameObject;
			obstacles.Add(newObject);
		}
		spawnPoints = obstacleSpawnPoints.GetComponentsInChildren<Transform> ();
		SetReleaseTime();

		dispertionRate = 1 - (.15f * global.levelDifficulty);
	}

	void Update()
	{
		if(!global.isSharkDefeated)
		{
			/*
			foreach(GameObject activeCheck in obstacles)
			{
				ObstacleClass checker=activeCheck.GetComponent<ObstacleClass>();
				if(checker.isActive)
				{
					if(activeCheck.transform.position.x<-10)
						checker.GetComponent<Obstacle_Main>().AssignObstacleData(Obstacle_List.GetClassType (ItemType.NULL.ToString()));
					else
						activeCheck.GetComponent<Obstacle_Main>().MoveObstacle(Time.deltaTime);
				}
			}
			*/
			TimedRelease();
		}

		if(global.isSharkDefeated)
		{
			DestroyAllObjects();
		}
	}

	public void DestroyAllObjects()
	{
		foreach(GameObject activeCheck in obstacles)
		{
			if(activeCheck.transform.position.x>-20)
			{
				activeCheck.GetComponent<Obstacle_Main>().Explode();
				activeCheck.transform.position=transform.position;
			}
		}
	}

	void SetReleaseTime()
	{
		timeToReach = Time.time + dispertionRate;
	}

	void TimedRelease()
	{
		if(Time.time>timeToReach)
		{
			ObstacleClass newObs=Obstacle_List.GetClassType(ItemType.HAZARD_LOW.ToString());
			InitializeObstacle(newObs);
			SetReleaseTime();
		}
	}

	void InitializeObstacle(ObstacleClass newObs)
	{
		foreach(GameObject obstacleArray in obstacles)
		{
			if(!obstacleArray.GetComponent<ObstacleClass>().isActive)
			{
				ObstacleClass initObject=newObs;
				Obstacle_Main newAssignment=obstacleArray.GetComponent<Obstacle_Main>();

				print ("INITIALIZING "+ initObject.itemType);

				if(initObject.structureReleaseType==ReleaseType.RANDOM)
				{
					int spawnNumber=Random.Range(1,spawnPoints.Length);
					initObject.currentPosition=new Vector2(spawnPoints[spawnNumber].position.x,spawnPoints[spawnNumber].position.y);
				}

				newAssignment.AssignObstacleData(initObject);
				return;
			}
		}
	}
}