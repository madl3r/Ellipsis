using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class World : MonoBehaviour {

	//This class is for managing the lines that exist and allowing or blocking acces to them.
	//This class will also issue commands to the camera to readjust based on actions taken above.
	//Finally this class also times out events (and keeps track of things that have happened in a single session) in the world throwing enemies at 
	//player and timing when boss shows up


	private GameObject[] thePlayers;

	//this public list of lines in the scene is input into the object from top to bottom.
	public List<GameObject> lines;
	public GameObject[] upgradeLines;

	//public GameObject upgradeLine;
	public GameObject upgradeArrow;
	public GameObject preBossArrow;
	public GameObject bossSkull;
	public GameObject shopArrow;
	
	
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
	private bool shopUnlocked;
	public bool bossDefeated;

	//Round and spawrning variables.
	public List<GameObject> prefabEnemies;
	public List<GameObject> prefabBossEncounters;
	public int round;

	//Upgrade round information
	int roundsToFirstUpgrade;
	public GameObject[] upgradePrefabs;
	public GameObject[] shopItems;
	public GameObject[] minorPickups;

	//Boss area round info
	int roundsToBossArea;

	//Shop area round stuff
	int roundsToShopArea;
	public GameObject keyHolePrefab;
	//Count down stuff
	public GameObject theLvlRoundCounter;
	public GameObject countDownObj;
	public GameObject bossCountDownObj;
	private bool roundStarted;
	private bool bossRoundStarted;
	private int enemiesKilledThisRound;
	private int enemiesSpawnedThisRound;
	private GameObject[] currentRoundEnemies;
	private int timeBtwnRound;
	private int timeLeftBtwnRound;


	//Will need to look for the players and set up what they need to know on the beginning of a new level
	//Also need to put the players onto the center line
	void Start () {
		//For now the lines have just been put into descending Y order manually.
		cameraYPos = 0.0f;

		//Get the line in the middle where the player starts. This can only be done with a magic number like this because the line order was put in manually. 

		//Should really be looking for the line with a y pos of 0.
		currentLine = lines[7];

		//Start at round 0
		round = 0;
		roundStarted = false;
		bossRoundStarted = false;
		enemiesKilledThisRound = 0;
		enemiesSpawnedThisRound = 0;

		//Setting how many times the play needs to attack between rounds.
		timeBtwnRound = 7;
		timeLeftBtwnRound = timeBtwnRound;

		roundsToFirstUpgrade = Random.Range(1, 7);
//		Debug.Log("THE FIRST UPGRADE WILL APPEAR AFTER ROUND " + roundsToFirstUpgrade);

		roundsToBossArea = Random.Range(1, 7);
//		Debug.Log("THE LEVEL WILL BE OVER AFTER ROUND " + roundsToBossArea);

		roundsToShopArea = Random.Range(1, 7);


//		Debug.Log("Getting the players for this level");
		//Giving each player the new world and setting the line target to where we want to be!

		//Getting the camera
		theCamera = GameObject.FindGameObjectWithTag("MainCamera");

		playerStats.setNextLvl(theNextLvl);
		thePlayers = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject pChar in thePlayers)
		{
			pChar.GetComponent<Movement>().newLevelRestart(gameObject);
		}

		bossDefeated = false;

	}
	
	// Update is called once per frame
	void Update () {
	
		//Locking camera into position.
		if(Mathf.Abs(theCamera.transform.position.y - cameraYPos) < 0.3f)
			theCamera.transform.position = new Vector3 (theCamera.transform.position.x, cameraYPos, -1.0f);

		//If we're currently in a round and all of the enemies have been killed for this round... then end the round
		if (roundStarted && enemiesKilledThisRound >= enemiesSpawnedThisRound)
		{
			finishRound();
		}

		//Reset this variable whenever the player goes into the update area
		if (timeLeftBtwnRound <= 0 && currentLine.tag == "lines")
		{
			timeLeftBtwnRound = timeBtwnRound;
			newRound();
		}
		else if (timeLeftBtwnRound <= 0 && currentLine.tag == "bossLines" && !bossRoundStarted && !bossDefeated)
		{
			timeLeftBtwnRound = timeBtwnRound;
			newBossRound();
		}
		else if (timeLeftBtwnRound <= 0 && currentLine.tag == "bossLines" && !bossRoundStarted && bossDefeated)
		{
			thePlayers[0].GetComponent<playerStats>().playNewLevel();
		}

		//If you move away from the main area then reset the counter for the round.
		if (currentLine.tag != "lines" && currentLine.tag != "bossLines")
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

	//Follows the position of the player and lets the camera know where the player is and if it should move
	public void updateCurrentLine(GameObject newLine)
	{
		//if the newLine has a different tag than the current one that tells us to move the camera accordingly
		if (currentLine != null && currentLine.tag != newLine.tag)
		{
			updateCameraPosition(newLine);
		}

		currentLine = newLine;
	}

	//Moving the camera
	void updateCameraPosition(GameObject newLine)
	{
		GameObject[] simLines;
		simLines = GameObject.FindGameObjectsWithTag(newLine.tag);
		float total = 0;

		foreach (GameObject line in simLines)
		{
			total += line.transform.position.y;
			cameraYPos = total / simLines.Length;
		}
	}



	public void finishRound()
	{
		enemiesKilledThisRound = 0; //So that it only goes into this once
		//Make sure that the enemies are all destroyed
		foreach (GameObject leftovers in currentRoundEnemies)
		{
			Destroy(leftovers);
		}

		//Tell the player that the round is over
		//			GameObject[] thePlayers = GameObject.FindGameObjectsWithTag("Player");
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
		
		//Spawn something
		if (Random.Range(0, 4) > 1)
			Instantiate(minorPickups[Random.Range(0, minorPickups.Length)], new Vector2(9.0f, 0.0f), Quaternion.identity);
		
		
		//Display the number of hits before the next round
		countDownObj.GetComponent<CountDownScript>().nums[timeLeftBtwnRound].renderer.enabled = true;
		bossCountDownObj.GetComponent<CountDownScript>().nums[timeLeftBtwnRound].renderer.enabled = true;
		//theLvlRoundCounter.renderer.enabled = true;
		theLvlRoundCounter.GetComponent<lvlRoundCounter>().updateCounter();


		//Let the player always re enter the basic lines in between rounds!
		foreach (GameObject line in lines)
		{
			if (line.tag == "lines")
			{
				line.GetComponent<LineScript>().canEnter = true;
			}
		}
		
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
		//If the player has survived enough rounds to go to the boss area, then open it up!
		if (round >= roundsToBossArea)
		{
			preBossArrow.renderer.enabled = true;
			bossSkull.renderer.enabled = true;
			
			foreach (GameObject line in lines)
			{
				if (line.tag == "preBossLines" || line.tag == "bossLines")
				{
					line.GetComponent<LineScript>().canEnter = true;
				}
			}
			
		}
		//If the player has survived enough rounds to go to the shop area, then open it up!
		if (round >= roundsToShopArea)
		{
			if (round == roundsToShopArea)
				populateShopLines();
			
			shopArrow.renderer.enabled = true;
			GameObject theEntrance = GameObject.FindGameObjectWithTag("shopEnterLine");
			theEntrance.GetComponent<LineScript>().canEnter = true;
			
			//If the entrance is NOT locked then you can go to the shop lines
			if (!theEntrance.GetComponent<shopEnterLineScript>().getIsLocked())
			{
				foreach (GameObject line in lines)
				{
					if (line.tag == "shopLines")
					{
						line.GetComponent<LineScript>().canEnter = true;
					}
				}
			}
		}
	}

	public void finishBossRound()
	{
		bossDefeated = true;
		bossRoundStarted = false;
		finishRound();
	}

	//Makes a new round
	//TODO When we have more enemies and stuff:
		//Get the LVL number from playerStats and then spawn the correct rounds accordingly
		//Maybe have this passed a base difficulty which will modify certain stats of spawning
		//Also depending on the level we will have the world be given a different list of enemy prefabs (maybe similar enemies but different stats)

	void newRound()
	{
		//Letting players know that the round has begun
//		GameObject[] thePlayers = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in thePlayers)
		{
			player.BroadcastMessage("setRoundStatus", true);
		}

		//Destroying left over pick up objects... by looking for them in the scene... there's got to be a better way.
		GameObject[] theMinUpgrades = GameObject.FindGameObjectsWithTag("minorPickup");
		foreach (GameObject mU in theMinUpgrades)
		{
			Destroy(mU);
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
		bossSkull.renderer.enabled = false;
		shopArrow.renderer.enabled = false;
		//theLvlRoundCounter.renderer.enabled = false;

		//Round things
		roundStarted = true;
		round++;
		currentRoundEnemies = new GameObject[Random.Range(2,6)];
		enemiesKilledThisRound = 0;
		enemiesSpawnedThisRound = currentRoundEnemies.Length;
		for (int i = 0; i < currentRoundEnemies.Length; i++)
		{
			GameObject e = Instantiate(prefabEnemies[Random.Range(0, prefabEnemies.Count)], new Vector2(0, 0), Quaternion.identity) as GameObject;
			e.GetComponent<BaseEnemy>().setTheWorld(gameObject);
			e.GetComponent<BaseEnemy>().isWorldSpawned(true);
			currentRoundEnemies[i] = e;
		}
	}

	void newBossRound()
	{
		Debug.Log("IN THE BOSS ROUND");
		bossRoundStarted = true;
		round++;
		//Spawn the boss and restrict player movement to the boss lines.

		//Letting players know that the round has begun and that they can attack
		foreach (GameObject player in thePlayers)
		{
			player.BroadcastMessage("setRoundStatus", true);
		}
		
		//Destroying left over pick up objects... by looking for them in the scene... there's got to be a better way.
		GameObject[] theMinUpgrades = GameObject.FindGameObjectsWithTag("minorPickup");
		foreach (GameObject mU in theMinUpgrades)
		{
			Destroy(mU);
		}
		
		//Deleting all out of round UI
		foreach (GameObject line in lines)
		{
			if (line.tag != "bossLines")
			{
				line.GetComponent<LineScript>().canEnter = false;
			}
		}

		upgradeArrow.renderer.enabled = false;
		preBossArrow.renderer.enabled = false;
		bossSkull.renderer.enabled = false;
		shopArrow.renderer.enabled = false;

		//This is so that the empty child gets deleted after the boss is destroyed
		currentRoundEnemies = new GameObject[1];
		GameObject e = Instantiate(prefabBossEncounters[Random.Range(0, prefabBossEncounters.Count)], new Vector2(8.0f, -20.0f), Quaternion.identity) as GameObject;
		currentRoundEnemies[0] = e;
		e.BroadcastMessage("setTheWorld", gameObject);
		e.BroadcastMessage("isWorldSpawned", false);

	}

	public void enemyKilled()
	{
		enemiesKilledThisRound++;
	}

	public void enemySpawned()
	{
		enemiesKilledThisRound--;
	}

	public void decrementRoundCount()
	{
		//Set the current one invisible
		countDownObj.GetComponent<CountDownScript>().nums[timeLeftBtwnRound].renderer.enabled = false;
		bossCountDownObj.GetComponent<CountDownScript>().nums[timeLeftBtwnRound].renderer.enabled = false;
		//decrement
		timeLeftBtwnRound--;
		//if we can set this one to visible
		if (timeLeftBtwnRound >= 0)
		{
			countDownObj.GetComponent<CountDownScript>().nums[timeLeftBtwnRound].renderer.enabled = true;
			bossCountDownObj.GetComponent<CountDownScript>().nums[timeLeftBtwnRound].renderer.enabled = true;
		}	
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

		foreach (GameObject number in bossCountDownObj.GetComponent<CountDownScript>().nums)
		{
			number.renderer.enabled = false;
		}
	}

	public GameObject getCurrentLine()
	{
		return currentLine;
	}

	public int getRoundNum()
	{
		return round;
	}

	//Populates all of the base upgrade lines that start out there
	void populateUpgradeLines()
	{
		GameObject thisUpgrade;
		bool locked;

		//Determine if it's locked
		//About 1/3 chance of a drop after each round
		locked = Random.Range(0,9) < 3;

		foreach (GameObject line in upgradeLines)
		{
			line.GetComponent<upgradeLineScript>().setIsLocked(locked);
			//if it's locked then we put up a keyhole.
			if (line.GetComponent<upgradeLineScript>().getIsLocked())
				thisUpgrade = Instantiate(keyHolePrefab, line.transform.position, Quaternion.identity) as GameObject;//thisUpgrade Instantiate a lock. When locks are attacked by a key, they then disappear and either spawn an upgrade (if on an upgrade line)
			//Otherwise just put the upgrade there.
			else
				thisUpgrade = Instantiate (upgradePrefabs[Random.Range(0, upgradePrefabs.Length)], line.transform.position, Quaternion.identity) as GameObject;
	
			//Updating the things that they need to know.
			thisUpgrade.GetComponent<BaseUpgrade>().upgradeLine = line;
			line.GetComponent<upgradeLineScript>().theUpgrade = thisUpgrade;
		}

	}

	//Adds items to the shop lines
	void populateShopLines()
	{
		//TODO... again maybe don't use the 'find' functions? could just look in the little one that we have here.
		foreach (GameObject sLine in GameObject.FindGameObjectsWithTag("shopLines"))
		{
			GameObject thisItem = Instantiate (shopItems[Random.Range(0, shopItems.Length)], sLine.transform.position, Quaternion.identity) as GameObject;
			if (thisItem.GetComponent<BaseShopItem>().theUpgrade != null)
				thisItem.GetComponent<BaseShopItem>().theUpgrade.GetComponent<BaseUpgrade>().upgradeLine = sLine;
			sLine.GetComponent<shopLinesScript>().theItem = thisItem; 
		}
	}

	//
	public void giveUpgradeLinesUpgrades()
	{
		foreach (GameObject uLine in upgradeLines)
		{
			GameObject thisUpgrade;
			uLine.GetComponent<upgradeLineScript>().setIsLocked(false);
			//Desetroying the current upgrade (in case of keyhole) and then giving a new upgrade to the line
			Destroy(uLine.GetComponent<upgradeLineScript>().theUpgrade);
			thisUpgrade = Instantiate (upgradePrefabs[Random.Range(0, upgradePrefabs.Length)], uLine.transform.position, Quaternion.identity) as GameObject;
			thisUpgrade.GetComponent<BaseUpgrade>().upgradeLine = uLine;
			uLine.GetComponent<upgradeLineScript>().theUpgrade = thisUpgrade;
		}
	}

	public  void deleteRemainingUpgrades()
	{
		foreach (GameObject uLine in upgradeLines)
		{
			Destroy(uLine.GetComponent<upgradeLineScript>().theUpgrade);
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

}
