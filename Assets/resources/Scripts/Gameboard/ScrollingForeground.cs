using UnityEngine;
using System.Collections;

public class ScrollingForeground : MonoBehaviour {
	Globals global;
	
	void Awake()
	{
		global=Globals.Instance;
	}
	
	void Update()
	{
		if(!global.isSharkDefeated)
			renderer.material.mainTextureOffset=new Vector2(Time.time*global.foregroundSpeed,0f);
	}
}
