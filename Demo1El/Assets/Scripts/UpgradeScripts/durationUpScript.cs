using UnityEngine;
using System.Collections;

public class durationUpScript : BaseUpgrade {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void giveUpgradeToPlayer (GameObject player)
	{
		player.GetComponent<playerStats>().addBnsBulletDuration(2);
		Destroy(gameObject);
	}
}
