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
	//The camera for the scene because this script will be controling it.
	public GameObject theCamera;

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
	private bool roundStarted;
	private int enemiesKilledThisRound;
	private int enemiesSpawnedThisRound;
	private GameObject[] currentRoundEnemies;

	
	// Use this for initialization
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
		foreach (GameObject line in lines)
		{
			Debug.Log(line.transform.position.y);
		}

		//Get the line in the middle where the player starts. This can only be done with a magic number like this because the line order was put in manually. Should really be looking for the line with a y pos of 0.
		currentLine = lines[4];

		//Start at round 0
		round = 0;
		roundStarted = false;
		enemiesKilledThisRound = 0;
		enemiesSpawnedThisRound = 0;
		newRound();

	}
	
	// Update is called once per frame
	void Update () {
	
		//Locking camera into position.
		if(Mathf.Abs(theCamera.transform.position.y - cameraYPos) < 0.3f)
			theCamera.transform.position = new Vector3 (theCamera.transform.position.x, cameraYPos, -1.0f);

		if (roundStarted && enemiesKilledThisRound == enemiesSpawnedThisRound)
		{
			//Round is over!
			roundStarted = false;
			Debug.Log("Round " + round + " survived!!");
			StartCoroutine("waitThenNewRound");

			//Wait for a little bit (maybe present a UI option to spawn the next round)
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
		Debug.Log("we have the world");
	}

	void sortLines()
	{
		//when we dynamically pick up lines this method will sort them by Y value.
	}

	void updateCurrentLine(GameObject newLine)
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

	void newRound()
	{
		Debug.Log("Spawning");
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

	IEnumerator waitThenNewRound()
	{
		//Debug.Log("IN THE NUMERBATOR");
		yield return new WaitForSeconds(3.0f);
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
