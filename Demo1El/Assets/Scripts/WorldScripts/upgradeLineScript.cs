using UnityEngine;
using System.Collections;

public class upgradeLineScript : LineScript {


	public GameObject theUpgrade;
	private bool isLocked;

	// Use this for initialization
	void Start () {

		isLocked = false;

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
	}

	public bool getIsLocked()
	{
		return isLocked;
	}
}
