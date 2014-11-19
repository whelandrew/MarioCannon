using UnityEngine;
using System.Collections;

public class GameboardHandling : MonoBehaviour {
	public Shark_Main sharkMain;
	public ObstacleGenerator obstacleGenerator;
	public ScrollingBackground sBackground;
	public ScrollingForeground sForeground;
	
	public void PlayerHitShark()
	{
		sharkMain.CollidedWithPlayer ();
		obstacleGenerator.DestroyAllObjects ();
	}
}
