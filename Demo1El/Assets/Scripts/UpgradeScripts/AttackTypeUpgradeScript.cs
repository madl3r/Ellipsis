using UnityEngine;
using System.Collections;

public class AttackTypeUpgradeScript : BaseUpgrade {

	//sets the players attack type (different colors)

	public GameObject attackTypeUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public override void giveUpgradeToPlayer (GameObject player, bool shopCalled)
	{
		player.GetComponent<playerStats>().setAttackType(attackTypeUp);
		if (!shopCalled)
			Destroy(gameObject);
	}
}
