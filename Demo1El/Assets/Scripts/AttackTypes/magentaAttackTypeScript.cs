using UnityEngine;
using System.Collections;

public class magentaAttackTypeScript : attackTypeScript {

	// Use this for initialization
	void Start () {
		defaultAttackSpeed = 2.0f;
		if (myPlayer != null)
		{		
			myPlayer.GetComponent<playerStats>().setBaseAttackSpd(defaultAttackSpeed);
			myPlayer.GetComponent<playerStats>().setBullet(bullet);
			myPlayer.GetComponent<SpriteRenderer>().color = new Color (255f, 0f, 255f);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
