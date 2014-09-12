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
	private static int hp;
	//Bonuses/upgrades
	public GameObject attackType;
	private int bulBnsDmg;
	private int bulBnsSpd;
	private float bulBnsDuration; // divide time by the speed for ranged and that way you get distance
	private float bnsAttackSpd;

	//Should include attack damage, and attack speed here as well too.

	// Use this for initialization
	void Start () {
		HPUI = GameObject.Find("HPui");
		theWorld = GameObject.FindGameObjectWithTag("theWorld");
//		isInRound = false;
		hp = 5;
		//HPUI.GetComponent<displayHP>.hearts = new GameObject[hp];
		HPUI.GetComponent<displayHP>().showHearts(hp);
		bnsAttackSpd = 0.0f;
		bulBnsDmg = 0;
		bulBnsSpd = 0;
		bulBnsDuration = 0.0f;

		//bonusDmg = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
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

	void attack()
	{
		if (isInRound){
			//Debug.Log("SHOOTING " + attackType);
			GameObject b = Instantiate(attackType, transform.position, transform.rotation) as GameObject;
			b.BroadcastMessage("addBonusDmg", bulBnsDmg);
			b.BroadcastMessage("addBonusBulSpeed", bulBnsSpd);
			b.BroadcastMessage("addBonusDuration", bulBnsDuration);
		}
		else if (theWorld.GetComponent<World>().getCurrentLine().tag == "lines")
		{
			//count down to next round
			theWorld.GetComponent<World>().decrementRoundCount();
		}
		else if (theWorld.GetComponent<World>().getCurrentLine().tag == "upgradeLine")
		{
			Debug.Log("GET THIS UPGRADE");
			//TODO Whenever you get an upgrade you should get your base attack speed updated to the defaultAttackSpeed of that attack.
		}
	}

	void setRoundStatus(bool round)
	{
		isInRound = round;
	}
}
