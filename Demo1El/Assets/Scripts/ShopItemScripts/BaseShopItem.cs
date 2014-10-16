using UnityEngine;
using System.Collections;

public class BaseShopItem : MonoBehaviour {

	//base cost should be at least 2 always
	public int costBase;
	public GameObject theUpgrade;
	private int cost;
	
	public GameObject[] numbers;
	public GameObject tensDigit;
	public GameObject onesDigit;

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

	public void buyThis(GameObject thePlayer)
	{
		theUpgrade.gameObject.GetComponent<BaseUpgrade>().giveUpgradeToPlayer(thePlayer);
		Destroy(gameObject);
	}

	public int getCost()
	{
		return cost;
	}
}
