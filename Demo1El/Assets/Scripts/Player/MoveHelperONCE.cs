using UnityEngine;
using System.Collections;

public class MoveHelperONCE : MonoBehaviour {

	//TODO NOTE!!
	//It is very important that this script is executed AFTER the "Movement" script.

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//the current first goes based on the original starting pos in the Q of the dot
		//this is sorta like an ID for it.
	
		//NOTICE that the addition and subtraction is opposite of that in the switchR and L methods of Movement.
		//That's because when you move 1 to 0 in movement you need to subtract. But here we had the orig at 0, and now the ID must be 1... so we add!

		//Just took out this one because pushing right should now just be used for potions!
		if (Input.GetKeyDown("right"))
		{
			//when switching right we want the next in line to be the new leader
			Movement.currentFirst = (Movement.currentFirst + 1) % 3;
		}

//		if (Input.GetKeyDown("left"))
//		{
//			//when switching left we want the prev in line to be the new leader.
//			Movement.currentFirst = (Movement.currentFirst + 2) % 3;
//		}

	}
}
