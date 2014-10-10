﻿using UnityEngine;
using System.Collections;

public class greenBulletScript : BaseBulletScript {

	// Use this for initialization
	void Start () {
		dmg = 2 + bnsDmg;
		bulletSpeed = 18.0f + bnsBulletSpeed;
		rigidbody2D.velocity = new Vector2(bulletSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
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
		else if (theHit.tag == "enemyBullet")
		{
			//Destory the bullet
			Destroy(theHit);
		}

	}


}
