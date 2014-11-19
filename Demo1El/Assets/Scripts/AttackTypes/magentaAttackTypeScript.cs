using UnityEngine;
using System.Collections;

public class magentaAttackTypeScript : attackTypeScript {

	// Use this for initialization
	void Start () {
		defaultAttackSpeed = 4.0f;
		knockBackPos = -6.5f;
		knockBackSpeed = -14.0f;
		if (myPlayer != null)
		{		
			myPlayer.GetComponent<playerStats>().setBaseAttackSpd(defaultAttackSpeed);
			myPlayer.GetComponent<playerStats>().setBullet(bullet);
			myPlayer.GetComponent<Movement>().setKnockBackPos(knockBackPos);
			myPlayer.GetComponent<playerStats>().setKnockBack(knockBackSpeed);
			myPlayer.GetComponent<SpriteRenderer>().color = new Color (255f, 0f, 255f);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
