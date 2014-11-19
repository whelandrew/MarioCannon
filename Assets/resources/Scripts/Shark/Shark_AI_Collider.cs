using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shark_AI_Collider : MonoBehaviour {
	List<GameObject> targets=new List<GameObject>();
	public GameObject obstacles;
	public GameObject player;

	bool isColliding;

	Player_Stats playerStats;
	public Shark_AI sharkAI;

	void Awake()
	{
		playerStats=player.GetComponent<Player_Stats>();
		
		foreach(Transform obs in obstacles.transform)
			targets.Add(obs.gameObject);
		targets.Add(player);
	}

	/*
	void FixedUpdate()
	{
		foreach(GameObject target in targets)
		{
			if (Vector2.Distance(target.transform.position, transform.position) < GetComponent<CircleCollider2D>().radius)
			{
				Debug.Log("Collission Detected with "+target.tag);
				if (!isColliding)
				{
					isColliding = true;
					OnEnterCollider(target.transform);
				}
			}
			else if (isColliding)
			{
				Debug.Log(target.tag + " has exited collisions with Shark");
				isColliding = false;
				OnExitCollider(target.transform);
			}
		}
	}
	void OnEnterCollider(Transform target)
	{
		if(target.gameObject.tag=="Player")
		{
			sharkAI.target=target;
			sharkAI.currentState=Shark_AI.AIState.Evade;
		}
	}
	
	void OnExitCollider(Transform target)
	{
		sharkAI.target=null;
		sharkAI.currentState=Shark_AI.AIState.Idle;
	}
	 */
	void OnCollision2DEnter(Collision2D collider)
	{
		Debug.Log("Shark AI Triggered from "+collider.gameObject.tag);
	}
}
