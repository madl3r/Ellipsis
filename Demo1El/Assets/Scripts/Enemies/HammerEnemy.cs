﻿using UnityEngine;
using System.Collections;

public class HammerEnemy : BaseEnemy {

	private bool recentlyDamaged;
	private float attackTime;
	private float timeBtwnAttacks;
	// Use this for initialization
	void Start () {
		recentlyDamaged = false;
		timeBtwnAttacks = 0.5f;
		hp = 3;
		dmg = 2;
		rigidbody2D.velocity = new Vector2 (-11.33f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (recentlyDamaged && (Time.time - attackTime) > timeBtwnAttacks)
			recentlyDamaged = false;

	}

	void FixedUpdate()
	{
		//transform.position = new Vector2 (transform.position.x - 0.2f, transform.position.y);
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
		//if we're hitting a player, and we haven't recently damaged them then deal damage.
		if (other.gameObject.tag == "Player" && !recentlyDamaged)
		{
			Debug.Log("Hitting player");
			recentlyDamaged = true;
			other.gameObject.SendMessage("takeDamage", dmg);
			//wait half a second before being able to deal damage again

			attackTime = Time.time;
			//Make noise and some effect
		}
		if (other.gameObject.tag == "bullet")
		{
			//Debug.Log("HIT BY A BULLET");
			other.gameObject.SendMessage("dealDamage", gameObject);
		}
		//if the game object has a tag of player... then deal damage!
		//if the game object has the tag of a damage thingy (bullet we'll call them)... then take damage!
	}
	

}
