using UnityEngine;
using System.Collections;

public class dmgUpScript : BaseUpgrade {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void giveUpgradeToPlayer (GameObject player)
	{
		player.GetComponent<playerStats>().addBnsBulletDamage(1);
		Destroy(gameObject);
	}

}
