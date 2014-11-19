using UnityEngine;
using System.Collections;

public class yellowAttackTypeScript : attackTypeScript {

	// Use this for initialization
	void Start () {
		defaultAttackSpeed = 2.0f;
		knockBackPos = -6.1f;
		knockBackSpeed = 0.0f;
		if (myPlayer != null)
		{		
			myPlayer.GetComponent<playerStats>().setBaseAttackSpd(defaultAttackSpeed);
			myPlayer.GetComponent<playerStats>().setBullet(bullet);
			myPlayer.GetComponent<Movement>().setKnockBackPos(knockBackPos);
			myPlayer.GetComponent<playerStats>().setKnockBack(knockBackSpeed);
			myPlayer.GetComponent<SpriteRenderer>().color = new Color (255f, 255f, 0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
