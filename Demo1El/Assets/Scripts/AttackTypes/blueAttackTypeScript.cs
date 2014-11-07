using UnityEngine;
using System.Collections;

public class blueAttackTypeScript : attackTypeScript {

	// Use this for initialization
	void Start () {
		defaultAttackSpeed = 5.0f;
		knockBackPos = -6.15f;
		knockBackSpeed = -12.5f;
		if (myPlayer != null)
		{		
			myPlayer.GetComponent<playerStats>().setBaseAttackSpd(defaultAttackSpeed);
			myPlayer.GetComponent<playerStats>().setBullet(bullet);
			myPlayer.GetComponent<Movement>().setKnockBackPos(knockBackPos);
			myPlayer.GetComponent<playerStats>().setKnockBack(knockBackSpeed);
			myPlayer.GetComponent<SpriteRenderer>().color = new Color (0f, 0f, 255f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
