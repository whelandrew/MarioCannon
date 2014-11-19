using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollingBackground : MonoBehaviour 
{
	Globals global;

	void Awake()
	{
		global=Globals.Instance;
	}

	void Update()
	{
		if(!global.isSharkDefeated)
			renderer.material.mainTextureOffset=new Vector2(Time.time*global.backgroundSpeed,0f);
	}
}