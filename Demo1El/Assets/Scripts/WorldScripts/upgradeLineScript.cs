using UnityEngine;
using System.Collections;

public class upgradeLineScript : LineScript {


	public GameObject theUpgrade;

	// Use this for initialization
	void Start () {
		if (tag == "lines")
			canEnter = true;
		else
			canEnter = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
