using UnityEngine;
using System.Collections;

public class bulletSpeedUpgrade : BaseUpgrade {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void giveUpgradeToPlayer (GameObject player)
	{
		player.GetComponent<playerStats>().addBnsBulletSpeed(15);
		Destroy(gameObject);
	}
}
