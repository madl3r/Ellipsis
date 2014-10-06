using UnityEngine;
using System.Collections;

public class maxHpUpScript : BaseUpgrade {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void giveUpgradeToPlayer (GameObject player)
	{
		player.GetComponent<playerStats>().addBnsMaxHP();
		player.GetComponent<SpriteRenderer>().sprite = upgradeSprite;
		Destroy(gameObject);
	}
}
