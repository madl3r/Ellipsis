using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

	private float theYPos;
	private bool leadIsFar;

	public float numInQ;
	public GameObject nextInQ;

	private GameObject lineTarget;
	private int lineTargetIndex;
	private GameObject theWorld;

	// Use this for initialization
	void Start () 
	{
		theYPos = 0.0f;
		theWorld = GameObject.FindGameObjectWithTag("theWorld");

		theWorld.SendMessage("printMessage");

		foreach (GameObject line in theWorld.GetComponent<World>().lines)
		{
			if (line.transform.position.y  == theYPos)
				lineTarget = line;
		}

		lineTargetIndex = theWorld.GetComponent<World>().lines.IndexOf(lineTarget);

		Debug.Log("the lineTarget's position" + lineTarget.transform.position + "With Index at: " + lineTargetIndex);

	}
	
	// Update is called once per frame
	void Update () 
	{
		//Y MOVEMENT
		//~~~~~~~WITH this if taken out things will continue to move to the correct line even when order
			// is switched mid move. THERE IS A JUMP THOUGH THAT IS DUE TO KEEPING THE SAME Y VALUE DURING A SWITCH
			// THIS JUMP MIGHT BE FIXED THROUGH THE EVENTUAL LERPING... OR BY MAKING THE Y VALUE EQUAL TO WHOEVER IS INFRONT OF OR BEHIND DEPENDING ON THE SWITCH

			//Maybe don't physically move them... instead on this movement just change the stats of each one including color. That might be weird with order of things...
				//Probably better to just find out how to smooth out the jumping
			//ACTUALLY PROBABLY DON'T MOVE THEM! WHENEVER DOING A SWITCH (L or R) send messages to each one so you know the order that it will happen in!!
				//Don't do a move within the game, instead do an animation to black and then change colors.

		//if (numInLine == 0)
		//{
			//If first in line update this one's theYPos to the next line
			//In the future it will NOT be a hard coded value. Instead will go to the next line up or down!!

			//TODO Should do an axis for controllers... but for now will just do getKeyDown

			//instead of manually changing up or down should get the next line up or down in the list of lines from world.
				//if it's allowed to do that based on "World's" decision then set theYPos to that lines y pos.
			if (Input.GetKeyDown("up") && theYPos < 4.0f)
			{
				theYPos += 2f;
			}

			if (Input.GetKeyDown("down") && theYPos > -4.0f)
			{
				theYPos -= 2f;
			}

			//Locking onto a line
			if(Mathf.Abs(transform.position.y - theYPos) < 0.05f)
				transform.position = new Vector2 (transform.position.x, theYPos);
		//}
		//Else slowly follow the person in front of you.
		//else
		if (numInQ != 0)
		{
			if (Mathf.Abs(transform.position.y - nextInQ.transform.position.y) > 0.5f)
			{
				leadIsFar = true;
			}
			else if (Mathf.Abs(transform.position.y - nextInQ.transform.position.y) < 0.01f)
			{
				leadIsFar = false;
				//clicking onto the line and updating theYPos
				transform.position = new Vector2 (transform.position.x, nextInQ.transform.position.y);
				//theYPos = transform.position.y;
			}
		}


		//Input detects movement right.
		if (Input.GetKeyDown("right"))
		{
			//first go to back
			if (numInQ == 0)
			{
				//Move to back
				transform.position = new Vector2 (transform.position.x - 2, transform.position.y);
				numInQ = 2;
				//now have a next in line... get it :/ nvm the order never changes
				
			}
			//second or third move up one
			else if (numInQ != 0)
			{
				//third keeps the same in front of it
				transform.position = new Vector2 (transform.position.x + 1, transform.position.y);
				numInQ -= 1;
			}
			
		}
		//Input movement left
		if (Input.GetKeyDown("left"))
		{
			//move back one
			if ((numInQ == 0) || (numInQ == 1))
			{
				transform.position = new Vector2 (transform.position.x - 1, transform.position.y);
				numInQ += 1;
			}
			//last move to the front
			else if (numInQ == 2)
			{
				transform.position = new Vector2 (transform.position.x + 2, transform.position.y);
				numInQ = 0;
			}
			
		}

	}

	void FixedUpdate()
	{
		//Following "theYPos" This lets the thing move slowly, instead of teleporting.
		if (numInQ == 0)
		{
			if (transform.position.y < theYPos)
				transform.position = new Vector2 (transform.position.x, transform.position.y + 0.2f);
			else if (transform.position.y > theYPos)
				transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
		}
		//later in line following movement.
		else
		{
			if (leadIsFar && transform.position.y < nextInQ.transform.position.y)
				transform.position = new Vector2 (transform.position.x, transform.position.y + 0.2f);
			else if (leadIsFar && transform.position.y > nextInQ.transform.position.y)
				transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
		}
	}
}