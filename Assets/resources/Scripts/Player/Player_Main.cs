using UnityEngine;
using System.Collections;

public class Player_Main : MonoBehaviour 
{
	Globals global;
	private Player_Stats playerStats;
	private Player_Animator playerAnimator;
	public GameboardUI gameboarUI;
	public GameboardHandling gameboardHandling;
	SpriteRenderer playerSprite;

	private bool movePlayer;
	private Vector2 newPlayerPos;

	bool lastHit;

	public UISprite armor;
	float armorReducer;
	bool fadeArmorIndicator;

	public bool stopTimer;
	float startTime;

	float obstacleReducer;

	int colliderValue;

	Vector2 endPosition=new Vector2(20,0);
	
	Vector2 knockBackPosition;
	bool isKnockback;

	void Awake () 
	{
		playerStats=Player_Stats.Instance;
		playerAnimator=GetComponent<Player_Animator>();
		playerSprite=GetComponent<SpriteRenderer>();

		global=Globals.Instance;

		SetupArmorHealth();
		ResetPlayerUI();
	}
	// Update is called once per frame
	void Update () 
	{
		if(!global.isSharkDefeated && movePlayer && !global.paused)
		{
			transform.position = Vector2.Lerp(transform.position, newPlayerPos, playerStats.speed);
			Vector2 endMove=new Vector2(Mathf.Round(transform.position.x)-Mathf.Round(newPlayerPos.x),Mathf.Round(transform.position.y)-Mathf.Round(newPlayerPos.y));
			if(endMove==Vector2.zero)
				movePlayer=false;
		}

		if(global.isSharkDefeated)
			transform.position=Vector2.Lerp(transform.position,endPosition,0.5f);

		if(isKnockback)
			transform.position=Vector2.Lerp(transform.position,knockBackPosition,.5f);

		if(!movePlayer)
			transform.rotation = Quaternion.AngleAxis(0, Vector3.zero);
	}

	void FixedUpdate()
	{
		switch(colliderValue)
		{
		case 0://no collisions
			StopCoroutine("DeactivateCollision");
			StopCoroutine("FadeIndicators");
			StopCoroutine("ReturnToCenter");
			ResetPlayerUI();
			break;
		case 1://collisions with obstacles
			StartCoroutine("DeactivateCollision");
			break;
		case 2://collisions with shark
			StartCoroutine("ReturnToCenter");
			break;
		case 3://player loses
			GameOver();
			break;
		case 4://player wins
			break;
		case 5://reset

			break;
		}
	}

	void ResetPlayerUI()
	{
		if(!collider2D.enabled)
			collider2D.enabled=true;
		if(playerSprite.color.a<1)
			playerSprite.color=new Color(1,1,1,1);
		if(!fadeArmorIndicator)
			armor.enabled=false;
		colliderValue=0;
	}

	void SetupArmorHealth()
	{
		//healthReducer=(1/playerStats.totalHealth);
		armorReducer=(1/playerStats.armorMax);
		obstacleReducer=1;
	}

	public void MovePlayer(Vector3 newPosition)
	{
		newPlayerPos=new Vector2(newPosition.x,newPosition.y);  
		movePlayer=true;

		LookRotation (newPosition);
	}

	void LookRotation(Vector3 newPosition)
	{
		Vector3 lookPos = newPosition - transform.position;
		lookPos.x=Mathf.Clamp(1,-1,1);
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	IEnumerator ReturnToCenter()
	{
		print ("RETURN TO CENTER");

		Vector3 destination = new Vector3 (-.1f, .1f, 0f);
		Vector3 movement=Vector3.Slerp(transform.position,destination,.08f);
		global.paused = true;

		Vector3 lookPos = movement - transform.position;
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		if(Time.time>startTime)
		{
			MovePlayer(transform.position);
			collider2D.enabled = true;
			global.paused = false;
			colliderValue = 0;
		}
		else
			transform.position=movement;

		yield return 0;
	}

	IEnumerator FadeIndicators()
	{
		if(fadeArmorIndicator)
		{
			for (float f = 1f; f >= 0; f -= 0.1f) 
			{
				armor.color=new Color(1,1,1,f);
				yield return new WaitForSeconds(.05f);
			}
			armor.enabled=false;
			fadeArmorIndicator=false;

			yield return 0;
		}
	}

	IEnumerator DeactivateCollision()
	{
		bool alphaOn=false;
		int alpha=0;
		stopTimer=true;
		for (float f = 2f; f >= 0; f -= 0.1f) 
		{
			if(!alphaOn)
			{
				alpha=0;
				alphaOn=true;
			}
			else
			{
				alpha=1;
				alphaOn=false;
			}
			playerSprite.color=new Color(1,1,1,alpha);

			yield return new WaitForSeconds(.1f);
		}

		colliderValue=0;
		stopTimer=false;
		yield return 0;
	}

	IEnumerator KnockBack()
	{
		for (float f = 2f; f >= 0; f -= 0.1f) 
		{
			isKnockback=true;
			yield return new WaitForSeconds(.01f);
		}
		isKnockback=false;
		yield return 0;
	}

	void GameOver()
	{
		Debug.LogError("GAME OVER");
		playerAnimator.BeginPlayerAnimation(Player_Animator.animationStates.DEFEAT);
		colliderValue=3;
	}

	void OnTriggerEnter2D(Collider2D collision) 
	{
		Debug.Log ("Player Collided with " + collision.gameObject.tag);
		if (!global.isPlayerInvincible) 
		{
			collider2D.enabled = false;
			if (collision.gameObject.tag == "Obstacle") 
			{
				if (lastHit)
						GameOver ();
				else 
				{
					//damage is (totalHealth-(reducer*obstacleDamage) if armor total is 0;
					//obstacleReducer=collision.gameObject.GetComponent<Obstacle_Main>().obstacleClass.damage;
					armor.fillAmount = armor.fillAmount - (armorReducer * obstacleReducer);
					colliderValue = 1;
					armor.enabled = true;

					CalculateKnockBack ();

					fadeArmorIndicator = true;
					StartCoroutine ("FadeIndicators");

					if (armor.fillAmount <= 0)
							lastHit = true;
				}
			}
		}

		if(collision.gameObject.tag == "Shark")
		{
			//check hitpoints of the shark and respond appropriately
			startTime=Time.time+1;
			colliderValue=2;
			collider2D.enabled=false;
			gameboardHandling.PlayerHitShark();
		}
	}

	void CalculateKnockBack()
	{
		float x=transform.position.x-3f;
		float y=transform.position.y-2f;

		if(x<-8)
			x=-8f;
		if(y<-4)
			y=-4f;

		knockBackPosition=new Vector2(x,y);
		StartCoroutine("KnockBack");
	}

	void OnTriggerExit2D(Collider2D collision)
	{
		Debug.Log("Player has ended collision with "+collision.gameObject.tag);
	}
}
