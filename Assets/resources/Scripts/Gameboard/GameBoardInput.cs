using UnityEngine;
using System.Collections;

public class GameBoardInput : MonoBehaviour {
	Vector2 newTouchLocation;
	public Player_Main playerMainScript;
	private Globals global;

	void Awake()
	{
		global=Globals.Instance;
	}

	void Update () 
	{
		if(!global.paused)
			TouchDetections();
	}

	void TouchDetections()
	{
		if (global.usingAccelerometer) 
		{
			float GetAxisV = Mathf.Clamp (Input.acceleration.y * 10, -4, 3);
			float GetAxisH = Mathf.Clamp (Input.acceleration.x * 10, -8, 8);

			Vector2 newPosition = new Vector2 (GetAxisH, GetAxisV);
			playerMainScript.MovePlayer (newPosition);
		} 
		else if (Input.touchCount > 0) 
		{
			if(Input.GetTouch(0).phase==TouchPhase.Moved)
			{
				Vector2 touchPosition=Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
				float GetAxisV = Mathf.Clamp(touchPosition.y, 	-4, 3);
				float GetAxisH = Mathf.Clamp(touchPosition.x-1f,-8, 8);
				
				Vector2 newPosition = new Vector2 (GetAxisH, GetAxisV);
				playerMainScript.MovePlayer (newPosition);
			}
		}
		else if (Input.GetMouseButton(0))
		{
			Vector2 mousePosition = Input.mousePosition;			
			mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

			float GetAxisV = Mathf.Clamp(mousePosition.y, 	-4, 3);
			float GetAxisH = Mathf.Clamp(mousePosition.x-1f,-8, 8);

			Vector2 newPosition=new Vector2(GetAxisH,GetAxisV);
			playerMainScript.MovePlayer(newPosition);
		}
	}
}
