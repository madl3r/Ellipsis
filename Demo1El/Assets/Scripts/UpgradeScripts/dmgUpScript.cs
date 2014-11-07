using UnityEngine;
using System.Collections;

public class dmgUpScript : BaseUpgrade {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void giveUpgradeToPlayer (GameObject player, bool shopCalled)
	{
		player.GetComponent<playerStats>().addBnsBulletDamage(1);
		//Also set the player stats current passive upgrade to set
		//Will also need to manually set the hit collider box within script. Just do it here so that we don't need to worry about it in player stats
		player.GetComponent<SpriteRenderer>().sprite = upgradeSprite;
		if (!shopCalled)
			Destroy(gameObject);
	}

}
