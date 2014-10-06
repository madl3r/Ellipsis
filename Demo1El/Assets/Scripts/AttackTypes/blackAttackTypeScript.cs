﻿using UnityEngine;
using System.Collections;

public class blackAttackTypeScript : attackTypeScript {

	// Use this for initialization
	void Start () {
		defaultAttackSpeed = 2.0f;
		if (myPlayer != null)
		{		
			myPlayer.GetComponent<playerStats>().setBaseAttackSpd(defaultAttackSpeed);
			myPlayer.GetComponent<playerStats>().setBullet(bullet);
			myPlayer.GetComponent<SpriteRenderer>().color = new Color (0f, 0f, 0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}