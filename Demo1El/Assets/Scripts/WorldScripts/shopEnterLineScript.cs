using UnityEngine;
using System.Collections;

public class shopEnterLineScript : LineScript {
	
	private bool isLocked;
	public GameObject theKeyhole;
	
	// Use this for initialization
	void Start () {
		isLocked = true;

		//Just set to false by default because we know if it has this script then it won't be true...?
		if (tag == "lines")
			canEnter = true;
		else
			canEnter = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setIsLocked(bool status)
	{
		isLocked = status;

		//if it's locked, then make sure that you can't enter the shopLines
		if (isLocked)
		{
			foreach (GameObject line in GameObject.FindGameObjectsWithTag("shopLines"))
			{
				line.GetComponent<LineScript>().canEnter = false;
			}
		}
		//otherwise destroy the keyhole, and make it so you can enter those lines.
		else
		{
			Destroy(theKeyhole);
			foreach (GameObject line in GameObject.FindGameObjectsWithTag("shopLines"))
			{
				line.GetComponent<LineScript>().canEnter = true;
			}
		}
	}

	public bool getIsLocked()
	{
		return isLocked;
	}

}
