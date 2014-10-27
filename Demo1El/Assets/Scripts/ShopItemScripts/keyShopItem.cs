using UnityEngine;
using System.Collections;

public class keyShopItem : BaseShopItem {

	// Use this for initialization
	void Start () {
		cost = costBase + Random.Range(-1, 5);
		
		//Displaying the cost of the item
		//Give a max to the number of keys that can be held
		if (cost > 99)
			cost = 99;
		
		int tens = cost / 10;
		int ones = cost - (tens * 10);
		
		tensDigit.gameObject.GetComponent<SpriteRenderer>().sprite = numbers[tens].gameObject.GetComponent<SpriteRenderer>().sprite;
		onesDigit.gameObject.GetComponent<SpriteRenderer>().sprite = numbers[ones].gameObject.GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void buyThis(GameObject thePlayer)
	{
		//In this just do the upgrade directly through this script since there is no upgrade for giving keys.
		thePlayer.GetComponent<playerStats>().addKey(1);
		Destroy(gameObject);
	}
}
