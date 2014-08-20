using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class World : MonoBehaviour {

	//This class is for managing the lines that exist and allowing or blocking acces to them.
	//This class will also issue commands to the camera to readjust based on actions taken above.
	//Finally this class also times out events (and keeps track of things that have happened in a single session) in the world throwing enemies at 
	//player and timing when boss shows up

	public List<GameObject> lines;
	public GameObject theCamera;

	private GameObject[] foundLines;
	private bool upgradesUnlocked;
	private bool bossUnlocked;

	
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
//		}

		foreach (GameObject line in lines)
		{
			Debug.Log(line.transform.position.y);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void printMessage()
	{
		Debug.Log("we have the world");
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
