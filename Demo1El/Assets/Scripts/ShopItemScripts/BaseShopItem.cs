using UnityEngine;
using System.Collections;

public class BaseShopItem : MonoBehaviour {


	public int costBase; //base cost should be at least 2 always
	public GameObject theUpgrade; //What we're giving the player
	protected int cost; //The actual cost

	//For displaying the cost
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

	//gives the player this upgrade, and then is used.
	public virtual void buyThis(GameObject thePlayer)
	{
		theUpgrade.gameObject.GetComponent<BaseUpgrade>().giveUpgradeToPlayer(thePlayer, true);
		Destroy(gameObject);
	}

	public int getCost()
	{
		return cost;
	}
}
