using UnityEngine;
using System.Collections;

public class lvlRoundCounter : MonoBehaviour {

	//The actual content
	private int lvlNum;
	private int roundNum;
	public GameObject theWorld;
	
	//For displaying the lvl - round
	public GameObject[] numbers;

	public GameObject lvlTensDigit;
	public GameObject lvlOnesDigit;

	public GameObject roundTensDigit;
	public GameObject roundOnesDigit;




	// Use this for initialization
	void Start () {
		theWorld = GameObject.FindGameObjectWithTag("theWorld");
		theWorld.GetComponent<World>().theLvlRoundCounter = gameObject;

//		lvlNum = playerStats.staticGetLvlNum();
//		roundNum = theWorld.GetComponent<World>().getRoundNum();
		updateCounter();
	}

	public void updateCounter()
	{
		lvlNum = playerStats.staticGetLvlNum();
		roundNum = theWorld.GetComponent<World>().getRoundNum();

		int lvlTens = lvlNum / 10;
		int lvlOnes = lvlNum - (lvlTens * 10);

		int roundTens = roundNum / 10;
		int roundOnes = roundNum - (roundTens * 10);
		
		roundTensDigit.gameObject.GetComponent<SpriteRenderer>().sprite = numbers[roundTens].gameObject.GetComponent<SpriteRenderer>().sprite;
		roundOnesDigit.gameObject.GetComponent<SpriteRenderer>().sprite = numbers[roundOnes].gameObject.GetComponent<SpriteRenderer>().sprite;

		lvlTensDigit.gameObject.GetComponent<SpriteRenderer>().sprite = numbers[lvlTens].gameObject.GetComponent<SpriteRenderer>().sprite;
		lvlOnesDigit.gameObject.GetComponent<SpriteRenderer>().sprite = numbers[lvlOnes].gameObject.GetComponent<SpriteRenderer>().sprite;

	}
	

}
