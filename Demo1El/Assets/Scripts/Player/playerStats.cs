using UnityEngine;
using System.Collections;

public class playerStats : MonoBehaviour {

	//This class keeps track of and updates the players stats (health and upgrades)
		//This class is then also in charge of using the upgrades that are currently on it... for now.

	//UI Stuff
	public static GameObject HPUI;
	public static GameObject theWorld;

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
	private GameObject theBullet;
	private int bulBnsDmg;
	private float bulBnsSpd;
	private float bulBnsDuration; // divide time by the speed for ranged and that way you get distance
	private float bnsAttackSpd;

	//Should include attack damage, and attack speed here as well too.

	// Use this for initialization
	void Start () {
		HPUI = GameObject.Find("HPui");
		theWorld = GameObject.FindGameObjectWithTag("theWorld");
//		isInRound = false;
		maxHP = 5;
		hp = maxHP;
		//HPUI.GetComponent<displayHP>.hearts = new GameObject[hp];
		HPUI.GetComponent<displayHP>().showHearts(hp);
		bnsAttackSpd = 0.0f;
		bulBnsDmg = 0;
		bulBnsSpd = 0;
		bulBnsDuration = 0.0f;

		//Make sure that you can attack from the start.
		lastAttack = -4.0f;

		attackType.GetComponent<attackTypeScript>().setBaseAttackSpeed(gameObject);
		attackType.GetComponent<attackTypeScript>().givePlayerBullet(gameObject);
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
		hp -= dmg;

		HPUI.GetComponent<displayHP>().showHearts(hp);

		if (hp <= 0)
		{
			Debug.Log ("HP is lower than 0 GG");
			GameObject.Find("GameOverScreen").renderer.enabled = true;
			Time.timeScale = 0;
			//Pause the game and show a game over screen
			//Run death animation
		}

	}

	void addHp (int heal)
	{
		hp += heal;
	}

	public void attack()
	{
		if (isInRound)
		{
			if (canAttack)
			{
				lastAttack = Time.time;
				Debug.Log("SHOOTING " + attackType);
				GameObject b = Instantiate(theBullet, transform.position, transform.rotation) as GameObject;
				b.BroadcastMessage("addBonusDmg", bulBnsDmg);
				b.BroadcastMessage("addBonusBulSpeed", bulBnsSpd);
				b.BroadcastMessage("addBonusDuration", bulBnsDuration);
			}
		}
		else if (theWorld.GetComponent<World>().getCurrentLine().tag == "lines")
		{
			//count down to next round
			theWorld.GetComponent<World>().decrementRoundCount();
		}
		else if (theWorld.GetComponent<World>().getCurrentLine().tag == "upgradeLines")
		{
			Debug.Log("GET THIS UPGRADE");
			if (theWorld.GetComponent<World>().getCurrentLine().GetComponent<upgradeLineScript>().theUpgrade != null)
			{
				theWorld.GetComponent<World>().getCurrentLine().GetComponent<upgradeLineScript>().theUpgrade.GetComponent<BaseUpgrade>().giveUpgradeToPlayer(gameObject);
				//Destroy(theWorld.GetComponent<World>().getCurrentLine().GetComponent<upgradeLineScript>().theUpgrade);
				//The upgrade should kill itself
			}
		}
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
		bnsAttackSpd += bns;
		updateTimeBetweenAttacks();
	}

	public void addBnsBulletDamage(int bns)
	{
		bulBnsDmg += bns;
	}

	public void addBnsBulletSpeed(float bns)
	{
		bulBnsSpd += bns;
	}

	public void addBnsBulletDuration(int bns)
	{
		bulBnsDuration += bns;
	}

	public void addBnsMaxHP(int bns)
	{
		maxHP += bns;
		//Also give the player that extra HP right now
		hp += bns;
		HPUI.GetComponent<displayHP>().showHearts(hp);
	}

	public void addHP(int heal)
	{
		hp += heal;
		HPUI.GetComponent<displayHP>().showHearts(hp);
	}
	//~~~~

	public void setBullet(GameObject b)
	{
		theBullet = b;
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
}
