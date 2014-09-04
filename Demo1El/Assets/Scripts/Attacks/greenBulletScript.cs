﻿using UnityEngine;
using System.Collections;

public class greenBulletScript : blueBulletScript {

	// Use this for initialization
	void Start () {
		dmg = 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void dealDamage(GameObject theHit)
	{
		if (theHit.tag == "player" || theHit.tag == "enemy")
		{
			theHit.SendMessage("takeDamage", dmg);
			Destroy(gameObject);
		}

	}


}
