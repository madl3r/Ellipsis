using UnityEngine;
using System.Collections;

public class colorRedUpgradeScript : BaseUpgrade {

	public GameObject redAType;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public override void giveUpgradeToPlayer (GameObject player, bool shopCalled)
	{
		player.GetComponent<playerStats>().setAttackType(redAType);
		if (!shopCalled)
			Destroy(gameObject);
	}
}
