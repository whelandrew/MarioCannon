using UnityEngine;
using System.Collections;

public class Shark_Animator : MonoBehaviour {
	Animator animator;
	Shark_Stats sharkStats;
	Shark_Main sharkMain;
	public enum animationStates{NULL=0,ESCAPE=1,LEAVE=2,DEFEAT=3};
	animationStates currentState;

	float time;

	Vector2 startLocation=new Vector2(21.1f,0.1f);
	Vector2 displaceLocation;

	Quaternion turnRight=new Quaternion(0,180,0,0);

	public ParticleSystem bubbles;

	void Awake()
	{
		animator=GetComponent<Animator>();
		sharkMain=GetComponent<Shark_Main>();
		sharkStats=Shark_Stats.Instance;
	}

	public void BeginSharkAnimation(animationStates state)
	{
		currentState=state;
		switch(currentState)
		{
		case animationStates.ESCAPE:
			displaceLocation=new Vector2(transform.position.x,transform.position.y+1.1f);
			animator.SetInteger("Idle",1);
			break;
		case animationStates.LEAVE:
			transform.rotation=turnRight;
			displaceLocation=startLocation;
			bubbles.transform.position=transform.position;
			bubbles.Emit(20);
			break;
		case animationStates.DEFEAT:
			animator.SetInteger("Idle",1);
			time=Time.time+1f;
			break;
		default:
			currentState=animationStates.NULL;
			break;
		}
	}

	void SharkDefeated()
	{
		//Vector2 movement=Vector2.Lerp(transform.position,displaceLocation,sharkStats.speed);
		Vector3 movement=Vector3.Slerp(transform.position,new Vector3(-0.1f,0.1f,transform.position.z),.02f);
		if(Time.time>time)
		{
			currentState=animationStates.NULL;
		}
		else
		{
			transform.position=movement;
			transform.Rotate(Vector3.forward*(Time.deltaTime/.001f));
		}
	}

	void SharkEscape()
	{
		Vector2 movement=Vector2.Lerp(transform.position,displaceLocation,sharkStats.speed);
		if((Vector2)transform.position==movement)
			BeginSharkAnimation(animationStates.LEAVE);
		else
			transform.position=movement;		
	}

	void SharkMove()
	{
		Vector2 movement=Vector2.Lerp(transform.position,displaceLocation,sharkStats.speed);
		if((Vector2)transform.position==displaceLocation)
		{
			currentState=animationStates.NULL;
			transform.rotation=new Quaternion(0,0,0,0);
			animator.SetInteger("Idle",0);
			sharkMain.BeginSharkTimer();
		}
		else
			transform.position=movement;
	}

	void LateUpdate()
	{
		switch(currentState)
		{
		case animationStates.ESCAPE:
			SharkEscape();
			break;	
		case animationStates.LEAVE:
			SharkMove();
			break;
		case animationStates.DEFEAT:
			SharkDefeated();
			break;
		default:
			currentState=animationStates.NULL;
			break;
		}
	}
}
