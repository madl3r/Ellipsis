using UnityEngine;
using System.Collections;

public class greenBulletScript : BaseBulletScript {

	// Use this for initialization
	void Start () {
		//Good damage... maybe make it bigger?
		dmg = 2 + bnsDmg;
		bulletSpeed = 18.0f + bnsBulletSpeed;
		rigidbody2D.velocity = new Vector2(bulletSpeed, 0);
	}
	
	//probably don't need to be checking this...
	void Update () {
		if (transform.position.x > 30)
			Destroy(gameObject);
	}

	protected override void dealDamage(GameObject theHit)
	{
		//If we hit a player or enemy, then damage it and die
		if (theHit.tag == "player" || theHit.tag == "enemy")
		{
			theHit.SendMessage("takeDamage", dmg);
			Destroy(gameObject);
		}
		//This attack pierces through bullets
		else if (theHit.tag == "enemyBullet" || theHit.tag == "bullet")
		{
			Destroy(theHit);
		}

	}


}
