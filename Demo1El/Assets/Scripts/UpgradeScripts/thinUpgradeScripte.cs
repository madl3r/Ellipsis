using UnityEngine;
using System.Collections;

public class thinUpgradeScripte : BaseUpgrade {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void giveUpgradeToPlayer (GameObject player)
	{
		player.GetComponent<playerStats>().addThinUpgrade();
		player.GetComponent<SpriteRenderer>().sprite = upgradeSprite;
		Destroy(gameObject);
	}
}
