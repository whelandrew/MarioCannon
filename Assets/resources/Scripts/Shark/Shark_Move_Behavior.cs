using UnityEngine;
using System.Collections;

public class Shark_Move_Behavior : MonoBehaviour 
{
	public int moveSpeed;
	public Vector2 instantVelocity;	
	
	void Start () 
	{		
		instantVelocity = Vector3.zero;		
	}
	
	void Update () 
	{		
		Vector2 pos=transform.position;
		float horMovement=Input.GetAxis("Horizontal");
		float forwardMovement=Input.GetAxis("Vertical");
		
		if (horMovement>0) 
		{			
			transform.Translate(transform.right * horMovement * Time.deltaTime * moveSpeed);			
		} 		
		
		if (forwardMovement>0) 
		{			
			transform.Translate(transform.forward * forwardMovement * Time.deltaTime * moveSpeed);			
		}		
		
		instantVelocity = (Vector2)transform.position - pos;		
	}
}
