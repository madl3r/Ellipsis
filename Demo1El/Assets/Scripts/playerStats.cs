using UnityEngine;
using System.Collections;

public class playerStats : MonoBehaviour {

	//This class keeps track of and updates the players stats (health and upgrades)
		//This class is then also in charge of using the upgrades that are currently on it... for now.
	private static int hp;
	public static GameObject HPUI;

	//Attack type maybe for close range, shoot, and deflect... then add modifiers like spread and what not
	public GameObject attackType;

	private int bonusDmg;

	//Should include attack damage, and attack speed here as well too.

	// Use this for initialization
	void Start () {
		HPUI = GameObject.Find("HPui");

		hp = 5;
		//HPUI.GetComponent<displayHP>.hearts = new GameObject[hp];
		HPUI.GetComponent<displayHP>().showHearts(hp);
		bonusDmg = 0;
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
		Debug.Log("SHOOTING " + attackType);
		GameObject b = Instantiate(attackType, transform.position, transform.rotation) as GameObject;
		b.BroadcastMessage("setDamageBonus", bonusDmg);
	}
}
