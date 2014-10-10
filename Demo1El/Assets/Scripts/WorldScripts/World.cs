using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class World : MonoBehaviour {

	//This class is for managing the lines that exist and allowing or blocking acces to them.
	//This class will also issue commands to the camera to readjust based on actions taken above.
	//Finally this class also times out events (and keeps track of things that have happened in a single session) in the world throwing enemies at 
	//player and timing when boss shows up



	//this public list of lines in the scene is input into the object from top to bottom.
	public List<GameObject> lines;
	//For dynamically making upgrade lines as more upgrades come in... we'll see... but for now I think that I'll just have two upgrades max

	// TODO Ultimately though... you should allow as many as you can to exist.

	//public GameObject upgradeLine;
	public GameObject upgradeArrow;
	public GameObject preBossArrow;
	//The camera for the scene because this script will be controling it.
	public GameObject theCamera;
	public string theNextLvl;

	//probably want to actually do the lines dynamically eventually.
	private GameObject[] foundLines;

	//Keeps track of the current line that the player is on. To be used for updating camera position
	private GameObject currentLine;
	//thePosition that the camera is going to
	private float cameraYPos;

	//bools that keep track of which areas the player is allowed to go into.
	private bool upgradesUnlocked;
	private bool bossUnlocked;

	//Round and spawrning variables.
	public List<GameObject> prefabEnemies;
	public int round;

	//Upgrade round information
	int roundsToFirstUpgrade;
	public GameObject[] upgradePrefabs;

	//Boss area round info
	int roundsToBossArea;

	//Count down stuff
	public GameObject countDownObj;
	private bool roundStarted;
	private int enemiesKilledThisRound;
	private int enemiesSpawnedThisRound;
	private GameObject[] currentRoundEnemies;
	private int timeBtwnRound;
	private int timeLeftBtwnRound;


	//Will need to look for the players and set up what they need to know on the beginning of a new level
	//Also need to put the players onto the center line
	void Start () {

		//Gather all lines in the scene. Order them in decreasing Y position value.
			//Could add different tags to the lines making them inherently different
			//like boss lines or upgrade lines.
			//This would allow for dynamically created lines in the level.
			//Also makes it so that I don't need to manually put these lines into world each time...
//		foundLines = GameObject.FindGameObjectsWithTag("lines");
//
//		sortLinesForY(foundLines);
//
//		for (int i = 0; i < foundLines.Length; i++)
//		{
//			Debug.Log(foundLines[i].transform.position.y);
//		}\

		//For now the lines have just been put into descending Y order manually.
		cameraYPos = 0.0f;
//		foreach (GameObject line in lines)
//		{
//			//Debug.Log(line.transform.position.y);
//		}

		//Get the line in the middle where the player starts. This can only be done with a magic number like this because the line order was put in manually. Should really be looking for the line with a y pos of 0.
		currentLine = lines[5];

		//Start at round 0
		round = 0;
		roundStarted = false;
		enemiesKilledThisRound = 0;
		enemiesSpawnedThisRound = 0;
		//newRound();

		//Setting how many times the play needs to attack between rounds.
		timeBtwnRound = 7;
		timeLeftBtwnRound = timeBtwnRound;

		roundsToFirstUpgrade = Random.Range(1, 7);
		Debug.Log("THE FIRST UPGRADE WILL APPEAR AFTER ROUND " + roundsToFirstUpgrade);

		roundsToBossArea = Random.Range(1, 7);
		Debug.Log("THE LEVEL WILL BE OVER AFTER ROUND " + roundsToBossArea);


		Debug.Log("Getting the players for this level");
		//Giving each player the new world and setting the line target to where we want to be!

		//Getting the camera
		theCamera = GameObject.FindGameObjectWithTag("MainCamera");

		playerStats.setNextLvl(theNextLvl);
		foreach (GameObject pChar in GameObject.FindGameObjectsWithTag("Player"))
		{
			pChar.GetComponent<Movement>().newLevelRestart(gameObject);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		//Locking camera into position.
		if(Mathf.Abs(theCamera.transform.position.y - cameraYPos) < 0.3f)
			theCamera.transform.position = new Vector3 (theCamera.transform.position.x, cameraYPos, -1.0f);

		//If we're currently in a round and all of the enemies have been killed for this round... then end the round
		if (roundStarted && enemiesKilledThisRound == enemiesSpawnedThisRound)
		{
			//Tell the player that the round is over
			GameObject[] thePlayers = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject player in thePlayers)
			{
				player.BroadcastMessage("setRoundStatus", false);
			}

			//Round is over!
			roundStarted = false;
			Debug.Log("Round " + round + " survived!!");
			timeLeftBtwnRound = timeBtwnRound;

			//Destroys all bullets in the level so that player no longer needs to dodge.
			GameObject[] levelBullets;
			levelBullets = GameObject.FindGameObjectsWithTag("enemyBullet");
			foreach (GameObject enBul in levelBullets)
			{
				Destroy(enBul);
			}

			//Display the number of hits before the next round
			countDownObj.GetComponent<CountDownScript>().nums[timeLeftBtwnRound].renderer.enabled = true;

			//If the round is over and we have survived x rounds then a new upgrade is spawned

			//When the round is over let the player enter the upgrade lines
			if (round >= roundsToFirstUpgrade)
			{
				//If this is when we first get to it then populate the upgrade lines
				if (round == roundsToFirstUpgrade)
					populateUpgradeLines();

				//Always after that let the player enter the upgrade area between rounds
				upgradeArrow.renderer.enabled = true;
				foreach (GameObject line in lines)
				{
					if (line.tag == "upgradeLines")
					{
						line.GetComponent<LineScript>().canEnter = true;
					}
				}
			}
			if (round >= roundsToBossArea)
			{
				preBossArrow.renderer.enabled = true;

				foreach (GameObject line in lines)
				{
					if (line.tag == "preBossLines")
					{
						line.GetComponent<LineScript>().canEnter = true;
					}
				}

			}
		}
		//Reset this variable whenever the player goes into the update area
		if (timeLeftBtwnRound <= 0)
		{
			//Wait a second or two between the two rounds

			//Kill the display thang

			//StartCoroutine("waitThenNewRound");
			newRound();
			timeLeftBtwnRound = timeBtwnRound;
		}

		//If you move away from the main area then reset the counter for the round.
		if (currentLine.tag != "lines")
		{
			timeLeftBtwnRound = timeBtwnRound;
			setCounterInvisible();
		}


	}

	void FixedUpdate()
	{
		//Moving the Camera
		if (theCamera.transform.position.y < cameraYPos)
			theCamera.transform.position = new Vector3(theCamera.transform.position.x, theCamera.transform.position.y + 0.3f, -1.0f);
		else if (theCamera.transform.position.y > cameraYPos)
			theCamera.transform.position = new Vector3(theCamera.transform.position.x, theCamera.transform.position.y - 0.3f, -1.0f);
	}

	void printMessage()
	{
		//Debug.Log("we have the world");
	}

	void sortLines()
	{
		//when we dynamically pick up lines this method will sort them by Y value.
	}

	public void updateCurrentLine(GameObject newLine)
	{
		//if the newLine has a different tag than the current one that tells us to move the camera accordingly
		if (currentLine != null && currentLine.tag != newLine.tag)
		{
			updateCameraPosition(newLine);
		}

		currentLine = newLine;
	}

	void updateCameraPosition(GameObject newLine)
	{
		GameObject[] simLines;
		simLines = GameObject.FindGameObjectsWithTag(newLine.tag);
		float total = 0;

		foreach (GameObject line in simLines)
		{
			total += line.transform.position.y;
			cameraYPos = total / simLines.Length;
			//theCamera.transform.position = new Vector3(theCamera.transform.position.x, total / simLines.Length, -1.0f);
		}

		//Move the camera to the middle of the new set of lines.
		//Debug.Log("MOVING THE CAMERA " + theCamera.transform.position);


	}


	//Makes a new round
	//TODO When we have more enemies and stuff:
		//Get the LVL number from playerStats and then spawn the correct rounds accordingly
		//Maybe have this passed a base difficulty which will modify certain stats of spawning
		//Also depending on the level we will have the world be given a different list of enemy prefabs (maybe similar enemies but different stats)
	void newRound()
	{
		//Letting players know that the round has begun
		GameObject[] thePlayers = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in thePlayers)
		{
			player.BroadcastMessage("setRoundStatus", true);
		}

		//Deleting all out of round UI
		foreach (GameObject line in lines)
		{
			if (line.tag != "lines")
			{
				line.GetComponent<LineScript>().canEnter = false;
			}
		}
		upgradeArrow.renderer.enabled = false;
		preBossArrow.renderer.enabled = false;

		//Round things
		roundStarted = true;
		round++;
		currentRoundEnemies = new GameObject[Random.Range(2,6)];
		enemiesKilledThisRound = 0;
		enemiesSpawnedThisRound = currentRoundEnemies.Length;
		for (int i = 0; i < currentRoundEnemies.Length; i++)
		{
			GameObject e = Instantiate(prefabEnemies[Random.Range(0, prefabEnemies.Count)], new Vector2(Random.Range(6, 9), Random.Range(-1, 3)*2), Quaternion.identity) as GameObject;
			currentRoundEnemies[i] = e;
		}
	}

	void enemyKilled()
	{
		enemiesKilledThisRound++;
	}

	public void decrementRoundCount()
	{
		//Set the current one invisible
		countDownObj.GetComponent<CountDownScript>().nums[timeLeftBtwnRound].renderer.enabled = false;
		//decrement
		timeLeftBtwnRound--;
		//if we can set this one to visible
		if (timeLeftBtwnRound >= 0)
			countDownObj.GetComponent<CountDownScript>().nums[timeLeftBtwnRound].renderer.enabled = true;
		//At end get rid of counter
		if (timeLeftBtwnRound <= 0)
		{
			setCounterInvisible();
		}
	}

	//Makes the counter thin invisible
	void setCounterInvisible()
	{
		foreach (GameObject number in countDownObj.GetComponent<CountDownScript>().nums)
		{
			number.renderer.enabled = false;
		}
	}

	public GameObject getCurrentLine()
	{
		return currentLine;
	}

	//Populates all of the base upgrade lines that start out there
	void populateUpgradeLines()
	{
		foreach (GameObject uLine in GameObject.FindGameObjectsWithTag("upgradeLines"))
		{
			GameObject thisUpgrade = Instantiate (upgradePrefabs[Random.Range(0, upgradePrefabs.Length)], uLine.transform.position, Quaternion.identity) as GameObject;
			uLine.GetComponent<upgradeLineScript>().theUpgrade = thisUpgrade;
			thisUpgrade.GetComponent<BaseUpgrade>().upgradeLine = uLine;
		}
	}

	//For when the player dies
	//Kill the players, and then reset to the main menu
	public void gameOver()
	{
		Debug.Log ("HP is lower than 0 GG");
		GameObject.Find("GameOverScreen").renderer.enabled = true;
		//Kill all the players
		foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
		{
			Destroy(p);
		}

		Application.LoadLevel("MainMenu");
	}

	//TODO dont wait for a new round (in seconds)... just let the player choose. wait for the player to attack some amount of times
	IEnumerator waitThenNewRound()
	{
		Debug.Log("IN THE NUMERBATOR");
		//When the round is over let the player enter the upgrade lines

		yield return new WaitForSeconds(2.0f);
		newRound();
	}

//	void sortLinesForY(GameObject[] theLines)
//	{
//		//TODO A NICE SORTING BASED ON Y POSITION.
//		for (int i = 0; i < theLines.Length; i++)
//		{
//			for (int j = i + 1; j < theLines.Length; j++)
//			{
//				//
//			}
//		}
//	}

}
