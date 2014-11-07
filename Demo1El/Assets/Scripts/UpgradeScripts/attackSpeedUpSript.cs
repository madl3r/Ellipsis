﻿using UnityEngine;
using System.Collections;

public class attackSpeedUpSript : BaseUpgrade {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void giveUpgradeToPlayer (GameObject player)
	{
		player.GetComponent<playerStats>().addBnsAttackSpd(0.5f);
		player.GetComponent<SpriteRenderer>().sprite = upgradeSprite;
		Destroy(gameObject);
	}
}
