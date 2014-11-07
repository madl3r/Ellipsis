using UnityEngine;
using System.Collections;

public class playerStats : MonoBehaviour {

	//This class keeps track of and updates the players stats (health and upgrades)
	//This class is then also in charge of using the upgrades that are currently on it


	//UI Stuff
	public static GameObject HPUI;
	public static GameObject keyUI;
	public static GameObject coinUI;
	public static GameObject theWorld;

	private static int levelNumber = 0;
	private static string nextLvl;
	
	//Maybe swtich from this name to just wether not it's able to attack
	private bool isInRound;

	//Stats
	public float baseAttackSpd;
	private float knockBackSpd;
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

	//Potion
	private GameObject potionType;
	
	private static int coins;
	private static int keys;
	
	void Start () {

		knockBackSpd = -10.0f;

		//Making the player stay between rounds always
		DontDestroyOnLoad(gameObject);

		//Getting all of the UI objects
		HPUI = GameObject.Find("HPui");
		keyUI = GameObject.Find("keyDisplay");
		coinUI = GameObject.Find("coinDisplay");
		theWorld = GameObject.FindGameObjectWithTag("theWorld");

		//starting outside of a round
		isInRound = false;

		//Setting default starting stats
		maxHP = 5;
		hp = maxHP;
		coins = 0;
		keys = 1;

		//Displaying the stats into the UI
		HPUI.GetComponent<displayHP>().showHearts(hp, maxHP);
		keyUI.GetComponent<displayKeys>().showKeyAmt(keys);
		coinUI.GetComponent<displayCoins>().showCoinAmt(coins);

		//Setting Bonuses
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
	}
	
	// Update is called once per frame
	void Update () {

		//Checking if the player can attack again.
		if (Time.time - lastAttack > timeBetweenAttacks)
		{
			canAttack = true;
			//If can attack is true make some thing indicating that you can shoot
		}
		//If can't attack again yet, then make sure the player knows it.
		else
		{
			canAttack = false;
			//Whenever it's false indicate that you can't shoot
		}

	
	}

	//Method for taking damage
	void takeDamage (int dmg)
	{
		// if we have a shield then take no damage and get rid of the upgrade
		if (hasShield)
		{
			//no damage taken
			hasShield = false;
			//Reset the shape back to circle
			gameObject.GetComponent<SpriteRenderer>().sprite = defaultShape;
		}
		//otherwise proceed as normal
		else
			hp -= dmg;

		//Redisplay the HP with new values.
		HPUI.GetComponent<displayHP>().showHearts(hp, maxHP);

		//if HP is less than or = zero... then gg
		if (hp <= 0)
		{
			//Tell the world that we died
			theWorld.GetComponent<World>().gameOver();
		}

		//flash red on the screen, also give invincibility for a sec... knockback?

	}


	//Attack or action method. Depending on status of the game and what kind of line that we're on will do different things
	public void attack()
	{

		if (canAttack)
		{
			lastAttack = Time.time;
		//Actually Attacking
		if (isInRound)
		{
			if (canAttack)
			{

				//Debug.Log("SHOOTING " + attackType);
				GameObject b = Instantiate(theBullet, transform.position, transform.rotation) as GameObject;
				b.BroadcastMessage("addBonusDmg", bulBnsDmg);
				gameObject.rigidbody2D.velocity = new Vector2(knockBackSpd, 0);//Random.Range(-20.0f, 20.01f));
				//b.BroadcastMessage("addBonusBulSpeed", bulBnsSpd);
				//b.BroadcastMessage("addBonusDuration", bulBnsDuration);
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
			//If the upgrade isn't null
			if (theWorld.GetComponent<World>().getCurrentLine().GetComponent<upgradeLineScript>().theUpgrade != null)
			{
				//if it's locked... then unlock it and spawn a treat
				if (theWorld.GetComponent<World>().getCurrentLine().GetComponent<upgradeLineScript>().getIsLocked())
				{
					//Only if we have keys
					if (keys > 0)
					{
						//Unlocking and spawning the upgrade
						spendKey();
						theWorld.GetComponent<World>().getCurrentLine().GetComponent<upgradeLineScript>().setIsLocked(false);
						theWorld.GetComponent<World>().giveUpgradeLinesUpgrades();
					}
				}
				//if not locked, then get the upgrade
				else
				{
					theWorld.GetComponent<World>().getCurrentLine().GetComponent<upgradeLineScript>().theUpgrade.GetComponent<BaseUpgrade>().giveUpgradeToPlayer(gameObject, false);
					//then delete the upgrade that we didn't choose.
					theWorld.GetComponent<World>().deleteRemainingUpgrades();
				}
			}
		}
		//Else if we're on the shopEnter line, and it's locked and we have at least 1 key... then unlock it!
		else if (theWorld.GetComponent<World>().getCurrentLine().tag == "shopEnterLine" &&
		         theWorld.GetComponent<World>().getCurrentLine().GetComponent<shopEnterLineScript>().getIsLocked() &&
		         keys > 0)
		{
			spendKey();
			theWorld.GetComponent<World>().getCurrentLine().GetComponent<shopEnterLineScript>().setIsLocked(false);
		}
		//If we're in the shop itself then...
		else if (theWorld.GetComponent<World>().getCurrentLine().tag == "shopLines")
		{
			//make sure that there is an item on this line
			if (theWorld.GetComponent<World>().getCurrentLine().GetComponent<shopLinesScript>().theItem != null)
			{
				//Then check if we can aford it
				if (coins >= theWorld.GetComponent<World>().getCurrentLine().GetComponent<shopLinesScript>().theItem.GetComponent<BaseShopItem>().getCost())
				{
					//If we can then we get the item and spend the money here.
					spendCoins(theWorld.GetComponent<World>().getCurrentLine().GetComponent<shopLinesScript>().theItem.GetComponent<BaseShopItem>().getCost());
					theWorld.GetComponent<World>().getCurrentLine().GetComponent<shopLinesScript>().theItem.GetComponent<BaseShopItem>().buyThis(gameObject);
				}
			}

		}
		//If we're on the pre boss lines and we attack then... Load the next level! (TODO, in the future nothing will happend when attack in pre boss lines)
		else if (theWorld.GetComponent<World>().getCurrentLine().tag == "bossLines")// && !theWorld.GetComponent<World>().bossDefeated)
		{
			//upLevelNumber();
			//Debug.Log("Level number is now: " + levelNumber);
			//Application.LoadLevel(nextLvl);
			theWorld.GetComponent<World>().decrementRoundCount();
		}

		}

	}
	
	//Used for setting the string that is the name of the next lvl. World gives this to us each time a new one is spawned (in each lvl). So each world will have to be given manually the next level that it wants to go to!
	public static void setNextLvl(string newLvl)
	{
		nextLvl = newLvl;
	}


	//Right now does nothing. Will be used for expending potion in the inventory, and then doing something with it. (will be similar to attack)
	public void usePotion()
	{
		//Call on the potion its "activate" (or w/e) method
		Debug.Log("USING POTION");
	}

	//Setting base attack speed (to be done when the attack type is gotten)
	public void setBaseAttackSpd(float spd)
	{
		baseAttackSpd = spd;
		updateTimeBetweenAttacks();
	}

	public void setKnockBack (float kb)
	{
		knockBackSpd = kb;
	}

	//~~~

	//Bonus adding methods
	//These bonuses methods set the bns that we got, and remove all other bonuses on this player (they don't stack)

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

	//Depricated; no bonus for this
	public void addBnsBulletSpeed(float bns)
	{
		bulBnsSpd  = bns;
		hasShield = false;
		bnsAttackSpd = 0;
		bulBnsDuration = 0;
		bulBnsDmg = 0;
		updateTimeBetweenAttacks();
	}

	//Depricated; no bonus for this
	public void addBnsBulletDuration(int bns)
	{
		bulBnsDuration = bns;
		hasShield = false;
		bnsAttackSpd = 0;
		bulBnsDmg = 0;
		bulBnsSpd = 0;
		updateTimeBetweenAttacks();
	}

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

	//~~~

	public void addHP(int heal)
	{
		hp += heal;
		HPUI.GetComponent<displayHP>().showHearts(hp, maxHP);
	}

	public void addKey (int keysAmt)
	{
		keys += keysAmt;
		keyUI.GetComponent<displayKeys>().showKeyAmt(keys);
	}

	//This function should only be called if the player has at least 1 key
	public void spendKey()
	{
		keys--;
		keyUI.GetComponent<displayKeys>().showKeyAmt(keys);
	}

	public void addCoins (int amt)
	{
		coins += amt;
		coinUI.GetComponent<displayCoins>().showCoinAmt(coins);
	}

	public void spendCoins(int amt)
	{
		coins -= amt;
		coinUI.GetComponent<displayCoins>().showCoinAmt(coins);
	}

	//Sets the bullet that we will be firing with attack. (set usually from attackType scripts)
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
	}

	public void setAttackType(GameObject aType)
	{
		Destroy(attackType);
		attackType = Instantiate(aType, transform.position, transform.rotation) as GameObject;
		attackType.GetComponent<attackTypeScript>().myPlayer = gameObject;
	}

	void setRoundStatus(bool round)
	{
		isInRound = round;
	}

	//Make sure to find the UI objects and to display the stats at the beginning of each new level.
	public void newLvlWorld(GameObject world)
	{
		theWorld = world;
		HPUI = GameObject.FindGameObjectWithTag("HP");
		keyUI = GameObject.Find("keyDisplay");
		coinUI = GameObject.Find("coinDisplay");
		HPUI.GetComponent<displayHP>().showHearts(hp, maxHP);
		keyUI.GetComponent<displayKeys>().showKeyAmt(keys);
		coinUI.GetComponent<displayCoins>().showCoinAmt(coins);
	}

	public void upLevelNumber()
	{
		levelNumber++;
	}

	public void playNewLevel()
	{
		upLevelNumber();
		Debug.Log("Level number is now: " + levelNumber);
		Application.LoadLevel(nextLvl);
	}

	public int getLevelNumber()
	{
		return levelNumber;
	}

	public void resetAttackTime()
	{
		lastAttack = Time.time;
	}


}
