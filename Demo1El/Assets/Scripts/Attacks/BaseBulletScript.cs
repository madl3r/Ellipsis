using UnityEngine;
using System.Collections;

public class BaseBulletScript : MonoBehaviour{
	
	//Bullet Stats
	protected int dmg;
	protected float bulletSpeed;
	protected float duration; //Duration is still necesarry for green and yellow attacks.
	protected float startTime;

	//Bonus damages
	protected int bnsDmg = 0;
	protected float bnsBulletSpeed = 0;
	protected float bnsDuration = 0;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
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

	//the basic dealDamage function of bullets only does damage to enemies and players by default
	//Need to override this if you want the bullet to destroy bullets that come against you
	protected virtual void dealDamage(GameObject theHit)
	{
		if (theHit.tag == "player" || theHit.tag == "enemy")
		{
			//Debug.Log("DEALING " + dmg + " DAMAGE");
			theHit.SendMessage("takeDamage", dmg);
			Destroy(gameObject);
		}
	}

	//Destroy when it goes off the to right.
	protected void OffCameraRight()
	{
		Destroy(gameObject);
	}

	protected void OffCameraLeft()
	{
		Destroy(gameObject);
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		dealDamage(other.gameObject);
	}

}
