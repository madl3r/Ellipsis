using UnityEngine;
using System.Collections;

public class squareUpgradeScript : BaseUpgrade {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void giveUpgradeToPlayer (GameObject player, bool shopCalled)
	{
		player.GetComponent<playerStats>().addSquareUpgrade();
		player.GetComponent<SpriteRenderer>().sprite = upgradeSprite;
		if (!shopCalled)
			Destroy(gameObject);
	}
}
