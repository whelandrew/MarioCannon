using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	Globals global;

	void Awake()
	{
		global=Globals.Instance;
	}

	void OnClick()
	{
		if(global.paused)
			global.Pause(false);
		else
			global.Pause(true);
	}
}
