using UnityEngine;
using System.Collections;

public class playerStats : MonoBehaviour {

	//This class keeps track of and updates the players stats (health and upgrades)
		//This class is then also in charge of using the upgrades that are currently on it... for now.

	//UI Stuff
	public static GameObject HPUI;
	public static GameObject theWorld;
	private static int levelNumber = 0;

	//Maybe swtich from this name to just wether not it's able to attack
	private bool isInRound;

	//Stats
	//Attack speed, although a separate stat from the attack type, will often be associate with which type of attack it has!
	public float baseAttackSpd;
	private bool canAttack;
	private float lastAttack;
	private float timeBetweenAttacks;

	private static int maxHP;
	private static int hp;

	//Bonuses/upgrades
	public GameObject attackType;
	public GameObject potion;
	private GameObject theBullet;
	private int bulBnsDmg;
	private float bulBnsSpd;
	private float bulBnsDuration; // divide time by the speed for ranged and that way you get distance
	private float bnsAttackSpd;
	private bool hasShield;
	private bool isHeartShape;
	private bool isThin;

	public Sprite defaultShape;

	//Should include attack damage, and attack speed here as well too.

	//TODO gonna have to do interesting stuff here for when loading into new levels
	void Start () {

		//Making the player stay between rounds always
		DontDestroyOnLoad(gameObject);

		HPUI = GameObject.Find("HPui");
		theWorld = GameObject.FindGameObjectWithTag("theWorld");
		isInRound = false;
		maxHP = 5;
		hp = maxHP;
		//HPUI.GetComponent<displayHP>.hearts = new GameObject[hp];
		HPUI.GetComponent<displayHP>().showHearts(hp, maxHP);
		bnsAttackSpd = 0.0f;
		bulBnsDmg = 0;
		bulBnsSpd = 0;
		bulBnsDuration = 0.0f;
		hasShield = false;
		isHeartShape = false;
		isThin = false;
		gameObject.GetComponent<BoxCollider2D>().enabled = false;


		//Make sure that you can attack from the start.
		lastAttack = -4.0f;

		attackType.GetComponent<attackTypeScript>().setBaseAttackSpeed(gameObject);
		attackType.GetComponent<attackTypeScript>().givePlayerBullet(gameObject);
		attackType.GetComponent<attackTypeScript>().myPlayer = gameObject;
		//attackType.SendMessage("setBaseAttackSpeed", gameObject);

		//bonusDmg = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.time - lastAttack > timeBetweenAttacks)
		{
			//Debug.Log("CAN ATTACK NOW");
			canAttack = true;
			//If can attack is true make some thing indicating that you can shoot
		}
		else
		{
			canAttack = false;
			//Whenever it's false indicate that you can't shoot
		}

	
	}

	void takeDamage (int dmg)
	{
		// if we have a shield then take no damage and get rid of the upgrade
		if (hasShield)
		{
			//no damage taken
			hasShield = false;
			//Reset the shape back to circle
			//gameObject.SpriteRenderer.sprite  = defaultShape;
			gameObject.GetComponent<SpriteRenderer>().sprite = defaultShape;
		}
		//otherwise proceed as normal
		else
			hp -= dmg;


		HPUI.GetComponent<displayHP>().showHearts(hp, maxHP);

		if (hp <= 0)
		{
			//Tell the world that we died
			theWorld.GetComponent<World>().gameOver();
		}

	}

	void addHp (int heal)
	{
		hp += heal;
	}

	public void attack()
	{
		//Actually Attacking
		if (isInRound)
		{
			if (canAttack)
			{
				lastAttack = Time.time;
				//Debug.Log("SHOOTING " + attackType);
				GameObject b = Instantiate(theBullet, transform.position, transform.rotation) as GameObject;
				b.BroadcastMessage("addBonusDmg", bulBnsDmg);
				b.BroadcastMessage("addBonusBulSpeed", bulBnsSpd);
				b.BroadcastMessage("addBonusDuration", bulBnsDuration);
			}
		}
		//Counting down to next round
		else if (theWorld.GetComponent<World>().getCurrentLine().tag == "lines")
		{
			//count down to next round
			theWorld.GetComponent<World>().decrementRoundCount();
		}
		//getting upgrade
		else if (theWorld.GetComponent<World>().getCurrentLine().tag == "upgradeLines")
		{
//			Debug.Log("GET THIS UPGRADE");
			if (theWorld.GetComponent<World>().getCurrentLine().GetComponent<upgradeLineScript>().theUpgrade != null)
			{
				theWorld.GetComponent<World>().getCurrentLine().GetComponent<upgradeLineScript>().theUpgrade.GetComponent<BaseUpgrade>().giveUpgradeToPlayer(gameObject);
				//Destroy(theWorld.GetComponent<World>().getCurrentLine().GetComponent<upgradeLineScript>().theUpgrade);
				//The upgrade should kill itself
			}
		}
		//If we're on the pre boss lines and we attack then... Load the next level!
		else if (theWorld.GetComponent<World>().getCurrentLine().tag == "preBossLines")
		{
		
			upLevelNumber();
			Debug.Log("Level number is: " + levelNumber);
			Application.LoadLevel("secondLevel");

//			//Giving each player the new world and setting the line target to where we want to be!
//			foreach (GameObject pChar in GameObject.FindGameObjectsWithTag("Player"))
//			{
//				pChar.GetComponent<Movement>().newLevelRestart();
//			}
//			//Finding our world!
//			theWorld = GameObject.FindGameObjectWithTag("theWorld");


			//Here call "getNewLvlLineTarget" on this guys movement script
			//Also probably need to set all game objects (like theWorld) to new things for the new level

//			foreach (GameObject line in GameObject.FindGameObjectWithTag("theWorld").GetComponent<World>().lines)
//			{
//				if (line.transform.position.y  == theYPos)
//					lineTarget = line;
//			}
		}

	}

	public void usePotion()
	{
		Debug.Log("USING POTION");
	}

	//Setting base attack speed (to be done when the attack type is gotten)
	public void setBaseAttackSpd(float spd)
	{
		baseAttackSpd = spd;
		updateTimeBetweenAttacks();
	}

	//~~~
	//Bonus adding methods
	public void addBnsAttackSpd(float bns)
	{
		bnsAttackSpd = bns;
		hasShield = false;
		isThin = false;
		gameObject.GetComponent<CircleCollider2D>().enabled = !isThin;
		gameObject.GetComponent<BoxCollider2D>().enabled = isThin;
		bulBnsDuration = 0;
		bulBnsDmg = 0;
		bulBnsSpd = 0;
		updateTimeBetweenAttacks();
		//if we were a heart before this upgrade then take away the extra hp and make us not a heart anymore
		if (isHeartShape)
			subBnsMaxHP();
	}

	public void addBnsBulletDamage(int bns)
	{
		bulBnsDmg = bns;
		hasShield = false;
		isThin = false;
		gameObject.GetComponent<CircleCollider2D>().enabled = !isThin;
		gameObject.GetComponent<BoxCollider2D>().enabled = isThin;
		bnsAttackSpd = 0;
		bulBnsDuration = 0;
		bulBnsSpd = 0;
		updateTimeBetweenAttacks();
		//if we were a heart before this upgrade then take away the extra hp and make us not a heart anymore
		if (isHeartShape)
			subBnsMaxHP();
	}

	public void addSquareUpgrade()
	{
		hasShield = true;
		isThin = false;
		gameObject.GetComponent<CircleCollider2D>().enabled = !isThin;
		gameObject.GetComponent<BoxCollider2D>().enabled = isThin;
		bulBnsDmg = 0;
		bnsAttackSpd = 0;
		bulBnsDuration = 0;
		bulBnsSpd = 0;
		updateTimeBetweenAttacks();
		//if we were a heart before this upgrade then take away the extra hp and make us not a heart anymore
		if (isHeartShape)
			subBnsMaxHP();
	}

	public void addThinUpgrade()
	{
		//updating the hitbox accordingly
		isThin = true;
		gameObject.GetComponent<CircleCollider2D>().enabled = !isThin;
		gameObject.GetComponent<BoxCollider2D>().enabled = isThin;
		hasShield = false;
		bulBnsDmg = 0;
		bnsAttackSpd = 0;
		bulBnsDuration = 0;
		bulBnsSpd = 0;
		updateTimeBetweenAttacks();
		//if we were a heart before this upgrade then take away the extra hp and make us not a heart anymore
		if (isHeartShape)
			subBnsMaxHP();
	}

	//Depricated
	public void addBnsBulletSpeed(float bns)
	{
		bulBnsSpd = bns;
		hasShield = false;
		bnsAttackSpd = 0;
		bulBnsDuration = 0;
		bulBnsDmg = 0;
		updateTimeBetweenAttacks();
	}

	//Depricated
	public void addBnsBulletDuration(int bns)
	{
		bulBnsDuration = bns;
		hasShield = false;
		bnsAttackSpd = 0;
		bulBnsDmg = 0;
		bulBnsSpd = 0;
		updateTimeBetweenAttacks();
	}

	//I don't think that we should have this... MAAAYYYBE
	public void addBnsMaxHP()
	{
		if (!isHeartShape)
		{
			bnsAttackSpd = 0;
			hasShield = false;
			isThin = false;
			gameObject.GetComponent<CircleCollider2D>().enabled = !isThin;
			gameObject.GetComponent<BoxCollider2D>().enabled = isThin;
			bulBnsDuration = 0;
			bulBnsDmg = 0;
			bulBnsSpd = 0;

			isHeartShape = true;
			maxHP += 1;
		}
		//Even if it's already a heart always give at least HP
		hp += 1;
		HPUI.GetComponent<displayHP>().showHearts(hp, maxHP);
	}

	public void subBnsMaxHP()
	{
		isHeartShape = false;
		maxHP -= 1;
		HPUI.GetComponent<displayHP>().showHearts(hp, maxHP);
	}

	public void addHP(int heal)
	{
		hp += heal;
		HPUI.GetComponent<displayHP>().showHearts(hp, maxHP);
	}
	//~~~~

	public void setBullet(GameObject b)
	{
		theBullet = b;
	}

	//Called for when the player switches from the first spot in the Queue
	public void onCollider(bool on)
	{
		//if we want the collider on, then turn on the correct one.
		if (on)
		{
			gameObject.GetComponent<CircleCollider2D>().enabled = !isThin;
			gameObject.GetComponent<BoxCollider2D>().enabled = isThin;
		}
		//else just turn them both off
		else
		{
			gameObject.GetComponent<CircleCollider2D>().enabled = false;
			gameObject.GetComponent<BoxCollider2D>().enabled = false;
		}
	}

	//Help method that updates the time between attacks whenever attackSpeed is updated.
	public void updateTimeBetweenAttacks()
	{
		Debug.Log("baseAttackSpeed is: " + baseAttackSpd);
		timeBetweenAttacks = 1.0f / (baseAttackSpd + bnsAttackSpd);
		lastAttack = Time.time;
//		Debug.Log("time between attacks is: " + timeBetweenAttacks + "for char " + gameObject);
	}

	public void setAttackType(GameObject aType)
	{
		Debug.Log("Upgrading the attack type");
		Destroy(attackType);
		attackType = Instantiate(aType, transform.position, transform.rotation) as GameObject;
		attackType.GetComponent<attackTypeScript>().myPlayer = gameObject;
		Debug.Log("After instantiate");
//		attackType.GetComponent<attackTypeScript>().setBaseAttackSpeed(gameObject); // these seem to be running before the start of the attackTypeScript
//		attackType.GetComponent<attackTypeScript>().givePlayerBullet(gameObject);
	}

	void setRoundStatus(bool round)
	{
		isInRound = round;
	}

	public void newLvlWorld(GameObject world)
	{
		theWorld = world;
		HPUI = GameObject.FindGameObjectWithTag("HP");
		HPUI.GetComponent<displayHP>().showHearts(hp, maxHP);
	}

	public void upLevelNumber()
	{
		levelNumber++;
	}

	public int getLevelNumber()
	{
		return levelNumber;
	}


}
