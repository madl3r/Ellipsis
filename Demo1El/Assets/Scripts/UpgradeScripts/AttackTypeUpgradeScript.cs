using UnityEngine;
using System.Collections;

public class AttackTypeUpgradeScript : BaseUpgrade {

	public GameObject attackTypeUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	public override void giveUpgradeToPlayer (GameObject player)
	{
		//Debug.Log("WHAT THE FUUCCKK?");
		player.GetComponent<playerStats>().setAttackType(attackTypeUp);
		Destroy(gameObject);
	}
}
