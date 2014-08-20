using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private float theYPos;
	private bool leadIsFar;

	public float numInLine;
	public GameObject nextInLine;

	// Use this for initialization
	void Start () 
	{
		theYPos = 0.0f;
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
		if (numInLine != 0)
		{
			if (Mathf.Abs(transform.position.y - nextInLine.transform.position.y) > 0.5f)
			{
				leadIsFar = true;
			}
			else if (Mathf.Abs(transform.position.y - nextInLine.transform.position.y) < 0.01f)
			{
				leadIsFar = false;
				//clicking onto the line and updating theYPos
				transform.position = new Vector2 (transform.position.x, nextInLine.transform.position.y);
				//theYPos = transform.position.y;
			}
		}


		//Input detects movement right.
		if (Input.GetKeyDown("right"))
		{
			//first go to back
			if (numInLine == 0)
			{
				//Move to back
				transform.position = new Vector2 (transform.position.x - 2, transform.position.y);
				numInLine = 2;
				//now have a next in line... get it :/ nvm the order never changes
				
			}
			//second or third move up one
			else if (numInLine != 0)
			{
				//third keeps the same in front of it
				transform.position = new Vector2 (transform.position.x + 1, transform.position.y);
				numInLine -= 1;
			}
			
		}
		//Input movement left
		if (Input.GetKeyDown("left"))
		{
			//move back one
			if ((numInLine == 0) || (numInLine == 1))
			{
				transform.position = new Vector2 (transform.position.x - 1, transform.position.y);
				numInLine += 1;
			}
			//last move to the front
			else if (numInLine == 2)
			{
				transform.position = new Vector2 (transform.position.x + 2, transform.position.y);
				numInLine = 0;
			}
			
		}

	}

	void FixedUpdate()
	{
		//Following "theYPos" This lets the thing move slowly, instead of teleporting.
		if (numInLine == 0)
		{
			if (transform.position.y < theYPos)
				transform.position = new Vector2 (transform.position.x, transform.position.y + 0.2f);
			else if (transform.position.y > theYPos)
				transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
		}
		//later in line following movement.
		else
		{
			if (leadIsFar && transform.position.y < nextInLine.transform.position.y)
				transform.position = new Vector2 (transform.position.x, transform.position.y + 0.2f);
			else if (leadIsFar && transform.position.y > nextInLine.transform.position.y)
				transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
		}
	}
}