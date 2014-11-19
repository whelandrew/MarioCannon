using UnityEngine;
using System.Collections;

public class GameboardUI : MonoBehaviour {
	public UILabel score_Label;
	Shark_Stats sharkStats;
	public Player_Main playerMain;

	Gameboard_Stats gameboardStats;

	Globals global;

	private float currentScore=0;
	private float bonusScore=1;

	int i=0;

	public UISprite[] sharkHealthIcons;

	void Awake()
	{
		sharkStats=Shark_Stats.Instance;
		gameboardStats=Gameboard_Stats.Instance;
		global=Globals.Instance;

		for(i=0;i<sharkStats.totalHealth;i++)
			sharkHealthIcons[i].enabled=true;
	}

	void Update()
	{
		if(!playerMain.stopTimer)
		{
			currentScore+=bonusScore;
			score_Label.text=currentScore.ToString();
		}
	}

	public bool IsSharkDefeated(float damage)
	{
		float finalResult=(damage/gameboardStats.levelDifficulty);
		Debug.Log("Damage to Shark "+finalResult);
		global.isSharkDefeated=true;

		for(int i=(sharkHealthIcons.Length-1);i>=0;i--)
		{
			if(sharkHealthIcons[i].enabled)
			{
				float fill=sharkHealthIcons[i].fillAmount-finalResult;
				if(fill<=0)
					sharkHealthIcons[i].enabled=false;
				else
					sharkHealthIcons[i].fillAmount=fill;
				break;
			}
		}

		foreach(UISprite health in sharkHealthIcons)
		{
			if(health.enabled)
				global.isSharkDefeated=false;
		}

		return global.isSharkDefeated;
	}
}
