using UnityEngine;
using System.Collections;

public class attackSpeedUpSript : BaseUpgrade {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void giveUpgradeToPlayer (GameObject player)
	{
		player.GetComponent<playerStats>().addBnsAttackSpd(10);//Just 10 for now... that's probably pretty OP
		Destroy(gameObject);
	}
}
