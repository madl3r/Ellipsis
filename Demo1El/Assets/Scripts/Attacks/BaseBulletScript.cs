using UnityEngine;
using System.Collections;

public class BaseBulletScript : MonoBehaviour{

	
	protected int dmg;// = 1;
	protected float bulletSpeed;// = 17.0f;
	protected float duration; // divide time by the speed for ranged and that way you get distance
	protected float startTime;

	protected int bnsDmg = 0;
	protected float bnsBulletSpeed = 0;
	protected float bnsDuration = 0;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > duration)
			Destroy(gameObject);

	}

	//Will need to have a separate method for each bullet bonus type (three)
	protected void addBonusDmg (int bonus)
	{
		bnsDmg += bonus;
	}
	
	protected void addBonusBulSpeed(int bonus)
	{
		bnsBulletSpeed += bonus;
	}
	
	protected void addBonusDuration (float bonus)
	{
		bnsDuration += bonus;
	}

	//Need to override this if you want the bullet to destroy bullets that come against you
	protected virtual void dealDamage(GameObject theHit)
	{
		if (theHit.tag == "player" || theHit.tag == "enemy")
		{
			//Debug.Log("DEALING " + dmg + " DAMAGE");
			theHit.SendMessage("takeDamage", dmg);
		}
		Destroy(gameObject);
	}


	
}
