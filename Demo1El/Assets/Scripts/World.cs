using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class World : MonoBehaviour {

	public List<GameObject> lines;

	
	// Use this for initialization
	void Start () {
		//Gather all lines in the scene. Order them in decreasing Y position value.
	}
	
	// Update is called once per frame
	void Update () {
		//This class is for managing the lines that exist and allowing or blocking acces to them.
		//This class will also issue commands to the camera to readjust based on actions taken above.
		//Finally this class also times out events (and keeps track of things that have happened in a single session) in the world throwing enemies at 
			//player and timing when boss shows up

	}
}
