using UnityEngine;
using System.Collections;

public class LineScript : MonoBehaviour {

	//Lines should only know about themselves and store info about themselves. And then give that info to the World.

	public bool canEnter;


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
