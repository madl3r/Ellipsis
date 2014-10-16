using UnityEngine;
using System.Collections;

public class shopEnterLineScript : LineScript {

	private bool isLocked;
	public GameObject theKeyhole;
	
	// Use this for initialization
	void Start () {
		isLocked = true;

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

		if (isLocked)
		{
			foreach (GameObject line in GameObject.FindGameObjectsWithTag("shopLines"))
			{
				line.GetComponent<LineScript>().canEnter = false;
			}
		}
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
