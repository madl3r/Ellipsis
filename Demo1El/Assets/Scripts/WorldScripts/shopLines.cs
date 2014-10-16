using UnityEngine;
using System.Collections;

public class shopLines : LineScript {

	public GameObject theItem;
	
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
