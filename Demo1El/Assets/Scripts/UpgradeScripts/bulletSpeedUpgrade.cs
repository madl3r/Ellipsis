using UnityEngine;
using System.Collections;

public class bulletSpeedUpgrade : BaseUpgrade {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void giveUpgradeToPlayer (GameObject player, bool shopCalled)
	{
		player.GetComponent<playerStats>().addBnsBulletSpeed(2);
		if (!shopCalled)
			Destroy(gameObject);
	}
}
