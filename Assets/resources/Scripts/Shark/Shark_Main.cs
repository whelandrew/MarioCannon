using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shark_Main : MonoBehaviour 
{
	Shark_Stats sharkStats;
	Shark_Animator sharkAnimations;

	Player_Stats playerStats;

	public GameboardUI gameboardUI;
	Shark_AI sharkAI;

	public float healthTotal;
	public float speed;

	bool hasCollided;

	Vector2 newSharkPosition;
	bool movingToScreen;

	void Awake()
	{
		sharkStats=Shark_Stats.Instance;
		sharkAnimations=GetComponent<Shark_Animator>();

		playerStats=Player_Stats.Instance;
		sharkAI=GetComponent<Shark_AI>();
	}

	void FixedUpdate()
	{
		if(!hasCollided)
			StopCoroutine("DeactivateCollision");
		if(movingToScreen)
			OnToScreenMovement();
	}

	void OnToScreenMovement()
	{
		transform.position=Vector2.Lerp(transform.position,newSharkPosition,sharkStats.speed);

		if(transform.position.x<=7.6f)
			movingToScreen=false;
	}

	IEnumerator DeactivateCollision()
	{
		for (float f = 2f; f >= 0; f -= 0.1f) 
		{
			yield return new WaitForSeconds(.1f);
		}

		hasCollided=false;
		collider2D.enabled=true;
		yield return 0;
	}
	public void BeginSharkTimer()
	{
		StartCoroutine("BringSharkToScreen");
	}

	IEnumerator BringSharkToScreen()
	{
		yield return new WaitForSeconds(sharkStats.waitTime);
		MoveSharkToScreen();
	}
	/*
	void OnTriggerEnter2D(Collider2D collision) 
	{
		Debug.Log("Shark Collided with "+collision.gameObject.tag);
		if(collision.gameObject.tag=="Obstacle")
		{

		}
		if(collision.gameObject.tag=="Player")
		{
			collider2D.enabled=false;
			hasCollided=true;
			if(gameboardUI.IsSharkDefeated(playerStats.attackPower))
				SharkDefeated();
			else
				sharkAnimations.BeginSharkAnimation(Shark_Animator.animationStates.ESCAPE);
		}	
	}
	*/
	public void CollidedWithPlayer()
	{
		collider2D.enabled=false;
		hasCollided=true;
		if(gameboardUI.IsSharkDefeated(playerStats.attackPower))
			SharkDefeated();
		else
			sharkAnimations.BeginSharkAnimation(Shark_Animator.animationStates.ESCAPE);
	}

	void MoveSharkToScreen()
	{
		transform.position=new Vector2(20f,0);
		float y=Random.Range(2.75f,-4f);
		newSharkPosition=new Vector2(7.5f,y);
		movingToScreen=true;
		collider2D.enabled=true;
	}

	void SharkDefeated()
	{
		//Debug.LogError("YOU WIN!");
		sharkAnimations.BeginSharkAnimation(Shark_Animator.animationStates.DEFEAT);
	}
}
