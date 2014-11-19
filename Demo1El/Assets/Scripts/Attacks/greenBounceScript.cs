using UnityEngine;
using System.Collections;

public class greenBounceScript : BaseBulletScript{

	//The bullet that it shoots back.
	public GameObject reflection;
	
	// Use this for initialization
	void Start () {
		dmg = 1 + bnsDmg;
		duration = 0.2f + bnsDuration;
		startTime = Time.time;
		transform.position = new Vector2 (transform.position.x + 1, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > duration)
			Destroy(gameObject);
	}

	//Can still deal damage to enemies.
	protected override void dealDamage(GameObject theHit)
	{
		//If it's an enemy damage it
		if (theHit.tag == "enemy")
			theHit.SendMessage("takeDamage", dmg);
		//If it's a bullet then reflect it.
		else if (theHit.tag == "enemyBullet" || theHit.tag == "bullet")
		{
			//reflect bullet
			GameObject b = Instantiate(reflection, transform.position, transform.rotation) as GameObject;
			//Broadcast here too because until now the bullet hasn't been created!
			b.BroadcastMessage("addBonusDmg", bnsDmg);
			b.BroadcastMessage("addBonusBulSpeed", bnsBulletSpeed);
			b.BroadcastMessage("addBonusDuration", bnsDuration);
			Destroy(theHit);
			Destroy(gameObject);
		}

		//Getting rid of this for now for slight buff.

		//Keep testing this... it makes it so that the bounce stays there for the entire time no matter what and reflects everything that hits it... actually kinda cool!

//		Destroy(gameObject);
	}

}
