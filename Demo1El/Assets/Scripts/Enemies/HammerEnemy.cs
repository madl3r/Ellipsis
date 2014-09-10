﻿using UnityEngine;
using System.Collections;

public class HammerEnemy : BaseEnemy {



	// Use this for initialization
	void Start () {
		hp = 3;
		dmg = 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		transform.position = new Vector2 (transform.position.x - 0.2f, transform.position.y);
	}

	void OffCameraLeft()
	{
		//Should maybe also choose a new line to go onto.
		transform.position = new Vector2 (11.0f, transform.position.y);
	}

	void OffCameraRight()
	{
		;
	}

//	//Currently ONLY collides when theHammer isKinematic... wtf!?
//	void OnCollisionEnter2D(Collision2D coll)
//	{
//		transform.position = new Vector2 (transform.position.x + 4, transform.position.y);
//		Debug.Log("THE HAMMER JUST HIT SOMETHING!");
//	}

	protected override void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.SendMessage("takeDamage", dmg);
			//Make noise and some effect
		}
		if (other.gameObject.tag == "bullet")
		{
			Debug.Log("HIT BY A BULLET");
			other.gameObject.SendMessage("dealDamage", gameObject);
		}
		//if the game object has a tag of player... then deal damage!
		//if the game object has the tag of a damage thingy (bullet we'll call them)... then take damage!
	}
	

}