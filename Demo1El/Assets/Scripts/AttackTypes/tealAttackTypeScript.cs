using UnityEngine;
using System.Collections;

public class tealAttackTypeScript : attackTypeScript {

	// Use this for initialization
	void Start () {
		defaultAttackSpeed = 3.5f;
		knockBackPos = -6.1f;
		knockBackSpeed = -10.0f;
		if (myPlayer != null)
		{		
			myPlayer.GetComponent<playerStats>().setBaseAttackSpd(defaultAttackSpeed);
			myPlayer.GetComponent<playerStats>().setBullet(bullet);
			myPlayer.GetComponent<Movement>().setKnockBackPos(knockBackPos);
			myPlayer.GetComponent<SpriteRenderer>().color = new Color (0f, 255f, 255f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
