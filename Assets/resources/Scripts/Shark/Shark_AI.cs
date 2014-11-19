using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shark_AI : MonoBehaviour {
	public Transform target;//the target shark AI is reacting to

	public float moveSpeed=6f;
	private float minDistance=1;
	private float safeDistance=.5f;

	public enum AIState {Idle, Seek, Flee, Arrive, Pursuit, Evade}

	public AIState currentState;
	public Shark_Move_Behavior moveBehavior;

	void Update () 
	{		
		switch(currentState)
		{			
			case AIState.Idle:			
				break;
				
			case AIState.Seek:			
				Seek();			
				break;
				
			case AIState.Flee:			
				Flee();			
				break;
				
			case AIState.Arrive:			
				Arrive();			
				break;
				
			case AIState.Pursuit:			
				Pursuit();			
				break;
				
			case AIState.Evade:			
				Evade();			
				break;			
		}		
	}
	
	void Seek ()
	{
		/*
		Vector2 direction=target.position - transform.position;
		
		if(direction.magnitude > minDistance)
		{	
			Vector2 moveVector  = direction.normalized * moveSpeed * Time.deltaTime;
			transform.position += (Vector3)moveVector;
		}
		*/
	}	
	
	
	void Flee ()
	{
		/*
		Vector2 direction=target.position - transform.position;

		if(direction.magnitude < safeDistance)
		{			
			//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);			
			Vector2 moveVector = direction.normalized * moveSpeed * Time.deltaTime;			
			transform.position += (Vector3)moveVector;			
		}		
		*/
	}
	
	void Arrive()
	{		
		/*
		Vector2 direction=target.position - transform.position;

		float distance=direction.magnitude;
		float decelerationFactor=distance/5;
		float speed=moveSpeed*decelerationFactor;
		
		Vector2 moveVector = direction.normalized * Time.deltaTime * speed;		
		transform.position += (Vector3)moveVector;	
		*/
	}
	
	void Pursuit()
	{		
		/*
		int iterationAhead=30;
		Vector2 targetSpeed=moveBehavior.instantVelocity;
		Vector2 targetFuturePosition=(Vector2)target.transform.position+(targetSpeed*iterationAhead);
		Vector2 direction=targetFuturePosition-(Vector2)transform.position;
		
		if(direction.magnitude > minDistance)
		{	
			Vector2 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
			transform.position += (Vector3)moveVector;
		}
		*/
	}
	
	void Evade()
	{		
		int iterationAhead=30;
		Vector2 targetSpeed=moveBehavior.instantVelocity;

		Vector2 targetFuturePosition=(Vector2)target.position+(targetSpeed*iterationAhead);
		Vector2 direction=(Vector2)transform.position-targetFuturePosition;

		direction.x=0;

		if(direction.magnitude < safeDistance)
		{	
			Vector2 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
			transform.position += (Vector3)moveVector;
		}
	}
}
