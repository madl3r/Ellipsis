using UnityEngine;
using System.Collections;

public class displayKeys : MonoBehaviour {
	
	public GameObject[] numbers;
	public GameObject tensDigit;
	public GameObject onesDigit;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void showKeyAmt(int keyAmt)
	{
		//Give a max to the number of keys that can be held
		if (keyAmt > 99)
			keyAmt = 99;

		//Calculating what the digits should be and displaying them.
		int tens = keyAmt / 10;
		int ones = keyAmt - (tens * 10);
		tensDigit.gameObject.GetComponent<SpriteRenderer>().sprite = numbers[tens].gameObject.GetComponent<SpriteRenderer>().sprite;
		onesDigit.gameObject.GetComponent<SpriteRenderer>().sprite = numbers[ones].gameObject.GetComponent<SpriteRenderer>().sprite;
	}
}
