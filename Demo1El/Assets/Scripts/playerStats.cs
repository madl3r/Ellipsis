using UnityEngine;
using System.Collections;

public class playerStats : MonoBehaviour {

	//This class keeps track of and updates the players stats (health and upgrades)
		//This class is then also in charge of using the upgrades that are currently on it... for now.
	private static int hp;


	//Attack type maybe for close range, shoot, and deflect... then add modifiers like spread and what not
	public int attackType;

	//Should include attack damage, and attack speed here as well too.

	// Use this for initialization
	void Start () {
		hp = 5;
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
		switch (attackType)
		{
		case 0:
			//Blue
			break;
		case 1:
			//Green
			break;
		case 2:
			//Red
			break;
		}
	}
}
