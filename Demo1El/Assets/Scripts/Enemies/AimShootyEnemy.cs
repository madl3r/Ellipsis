﻿using UnityEngine;
using System.Collections;

public class AimShootyEnemy : BaseEnemy {

	public GameObject attackType;
	private float timeBetweenShot;
	private float prevShotTime;

	private GameObject playerToLookAt;
	private Vector3 eulerAngleOffset;

	// Use this for initialization
	void Start () {
		hp = 2;
		dmg = 1;
		prevShotTime = Time.time;

		//Making so that we're looking at the -x direction instead of the Z direction.
		eulerAngleOffset = new Vector3(0, 90, 0);

		playerToLookAt = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		//Look at the player
		transform.LookAt(playerToLookAt.transform.position);
		transform.Rotate(eulerAngleOffset, Space.Self);

		//Every now and again move
		if (Time.time - prevShotTime > timeBetweenShot)
		{
			attack ();
		}
	}

	protected virtual void attack()
	{
		prevShotTime = Time.time;
		timeBetweenShot = Random.Range(0.75f, 1.0f);
		Instantiate(attackType, transform.position, transform.rotation);
	}
}