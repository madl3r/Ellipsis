using UnityEngine;
using System.Collections;

public class attackTypeScript : MonoBehaviour
{
	//To be used for setting the default attack speed of the player that is currently using this attackType
	protected float defaultAttackSpeed;

	protected int dmg;
	protected int bulletSpeed;
	protected float duration;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//Will need to have a separate method for each bullet bonus type (three)
	protected void addBonusDmg (int bonus)
	{
		dmg += bonus;
	}

	protected void addBonusBulSpeed(int bonus)
	{
		bulletSpeed += bonus;
	}

	protected void addBonusDuration (float bonus)
	{
		duration += bonus;
	}


	public virtual void setBaseAttackSpeed(GameObject attacker)
	{
		attacker.GetComponent<playerStats>().baseAttackSpd = defaultAttackSpeed;
	}
}
