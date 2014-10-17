using UnityEngine;
using System.Collections;

public class tealBulletScript : BaseBulletScript {

	// Use this for initialization
	void Start () 
	{
		dmg = 1 + bnsDmg;
		bulletSpeed = 19.0f + bnsBulletSpeed;
		startTime = Time.time;
		rigidbody2D.velocity = new Vector2(bulletSpeed, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.x > 30)
			Destroy(gameObject);
	}
	
	protected override void dealDamage(GameObject theHit)
	{
		if (theHit.tag == "player" || theHit.tag == "enemy")
		{
			theHit.SendMessage("takeDamage", dmg);
			Destroy(gameObject);
		}
		//Destroys bullets that it hits, a defensive trait
		else if (theHit.tag == "enemyBullet")
		{
			//Destory the bullet
			Destroy(theHit);
		}
		//I think it's too OP if it just pierces bullets... so for now it always dies.
		Destroy(gameObject);
		
	}

}
