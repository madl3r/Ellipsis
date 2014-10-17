using UnityEngine;
using System.Collections;

public class displayCoins : MonoBehaviour {
	
	public GameObject[] numbers;
	public GameObject tensDigit;
	public GameObject onesDigit;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void showCoinAmt(int coinAmt)
	{
		//Give a max to the number of keys that can be held
		if (coinAmt > 99)
			coinAmt = 99;

		//Setting the digits of the sprites that display how much money the player has
		int tens = coinAmt / 10;
		int ones = coinAmt - (tens * 10);
		tensDigit.gameObject.GetComponent<SpriteRenderer>().sprite = numbers[tens].gameObject.GetComponent<SpriteRenderer>().sprite;
		onesDigit.gameObject.GetComponent<SpriteRenderer>().sprite = numbers[ones].gameObject.GetComponent<SpriteRenderer>().sprite;
	}
}
