using UnityEngine;
using System.Collections;

public class playerStats : MonoBehaviour {

	//This class keeps track of and updates the players stats (health and upgrades)
		//This class is then also in charge of using the upgrades that are currently on it... for now.
	private static int hp;
	
	//Attack type maybe for close range, shoot, and deflect... then add modifiers like spread and what not
	public GameObject attackType;
	private int bonusDmg;

	//Should include attack damage, and attack speed here as well too.

	// Use this for initialization
	void Start () {
		hp = 5;
		bonusDmg = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void takeDamage (int dmg)
	{
		hp -= dmg;

		if (hp <= 0)
		{
			Debug.Log ("HP is lower than 0 GG");
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
