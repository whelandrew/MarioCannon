using UnityEngine;
using System.Collections;

public class Debug_Tools : MonoBehaviour {
	Globals global;
	public UIButton accelerometerButton;
	public UIButton invincibleButton;
	// Use this for initialization
	void Start () {
		global = Globals.Instance;

		accelerometerButton.GetComponentInChildren<UILabel> ().text = "Accelerometer " + global.usingAccelerometer;
		invincibleButton.GetComponentInChildren<UILabel> ().text = "Invincibility " + global.isPlayerInvincible;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ActivateAccelerometer()
	{
		if (global.usingAccelerometer)
				global.usingAccelerometer = false;
		else
				global.usingAccelerometer = true;
		accelerometerButton.GetComponentInChildren<UILabel> ().text = "Accelerometer " + global.usingAccelerometer;
	}

	void DefeatShark()
	{
		global.isSharkDefeated = true;
	}

	void Invincible()
	{
		if (global.isPlayerInvincible)
				global.isPlayerInvincible = false;
		else
				global.isPlayerInvincible = true;
		invincibleButton.GetComponentInChildren<UILabel> ().text = "Invincibility " + global.isPlayerInvincible;
	}
}
