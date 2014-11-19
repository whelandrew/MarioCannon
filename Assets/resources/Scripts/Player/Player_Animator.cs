using UnityEngine;
using System.Collections;

public class Player_Animator : MonoBehaviour {
	Animator animator;
	Player_Stats playerStats;
	public enum animationStates{NULL=0,IDLE=1,DAMAGED=2,DEFEAT=3};
	animationStates currentState;
	
	void Awake()
	{
		animator=GetComponent<Animator>();
		playerStats=Player_Stats.Instance;
	}
	
	public void BeginPlayerAnimation(animationStates state)
	{
		currentState=state;
	}
	
	void LateUpdate()
	{
		switch(currentState)
		{
		case animationStates.IDLE:
			animator.SetInteger("Idle",0);
			break;
		case animationStates.DEFEAT:
			animator.SetInteger("Idle",2);
			break;
		case animationStates.DAMAGED:
			break;
		default:
			currentState=animationStates.NULL;
			break;
		}
	}
}
