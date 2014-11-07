using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {


	private float knockBackPosition;
	
	//Touch Control Variables
	public static float minSwipeDistY = 50;
	public static float minSwipeDistX = 50;
	private Vector2 startPos;

	//To avoid multiple activations of switches which would occur based on arbitrary exec order of dots movement scripts.
	public static int currentFirst = 0;

	//current position in the Q
	public int numInQ;
	//handles to the next and prev objects in line.
	public GameObject nextInQ;
	public GameObject prevInQ;
	//the name
	public string thisName;
		
	//The position that it will follow towards.
	private float theYPos;
	//checks if the next in line is far enough away that it should start to follow.
	private bool leadIsFar;
	//The original position in the Q
	//This variable shouldn't change past start and acts as a sort of ID for the dot.
	private int origNumInQ;

	//to be used with the "World" script. the next line that this wants to go to
	private GameObject lineTarget;
	//index of the lineTarget
	private int lineTargetIndex;
	//handle to the empty GameObject with the "World" script on it
	private GameObject theWorld;

	//temp vars for switching
	public Vector2 posOnSwitch;
	public float theYPosOnSwitch;
	
	//TODO might need to put all of this into a separate method to be called by the world when a new level is loaded.
	void Start () 
	{
		knockBackPosition = -6.25f;
		//getting this dots original position in the Q.
		origNumInQ = numInQ;
		//theYPost defaults to 0
		theYPos = 0.0f;
		//getting that empty GameObject
		theWorld = GameObject.FindGameObjectWithTag("theWorld");

		//starting the lineTarget to be the line that we're currently on.
		foreach (GameObject line in theWorld.GetComponent<World>().lines)
		{
			if (line.transform.position.y  == theYPos)
				lineTarget = line;
		}
		//now getting it's index.
		lineTargetIndex = theWorld.GetComponent<World>().lines.IndexOf(lineTarget);

		//Telling the world where we start.
		theWorld.GetComponent<World>().updateCurrentLine(lineTarget);//theWorld.SendMessage("updateCurrentLine", lineTarget);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//#if UNITY_ANDROID
		if (Input.touchCount > 0) 
		{			
			Touch touch = Input.touches[0];						
			
			switch (touch.phase) 				
			{				
			case TouchPhase.Began:				
				startPos = touch.position;				
				break;								
				
			case TouchPhase.Ended:				
				//Get the two swipe distance types at the same time
				float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;				
				float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;	
				//Swipe verticle if it betters our min swipe distance AND if it's larger than horizontal
				if (swipeDistVertical > minSwipeDistY && swipeDistVertical > swipeDistHorizontal) 					
				{					
					float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
					//Up Swipe
					if (swipeValue > 0)//&& (lineTargetIndex - 1) >= 0
//					    && theWorld.GetComponent<World>().lines[lineTargetIndex - 1].GetComponent<LineScript>().canEnter)
					{
						//For each line above this line
						for (int i = 1; (lineTargetIndex - i) >= 0; i++)
						{
							//If that is a line that we can enter... then enter it.
							if ((lineTargetIndex - i) >= 0 && theWorld.GetComponent<World>().lines[lineTargetIndex - i].GetComponent<LineScript>().canEnter)
							{
								lineTarget = theWorld.GetComponent<World>().lines[lineTargetIndex - i];
								lineTargetIndex -= i;
								theYPos = lineTarget.transform.position.y;
								theWorld.GetComponent<World>().updateCurrentLine(lineTarget);
								//Break out if we were able to move to a line
								break;
							}
						}
					}
						
					//Down Swipe
					if (swipeValue < 0)// && (lineTargetIndex + 1) < theWorld.GetComponent<World>().lines.Count
//					         && theWorld.GetComponent<World>().lines[lineTargetIndex + 1].GetComponent<LineScript>().canEnter)
					{
						//For each line above this line
						for (int i = 1; (lineTargetIndex + i) < theWorld.GetComponent<World>().lines.Count; i++)
						{
							//If that is a line that we can enter... then enter it skipping the lines that we're not allowed onto.
							if ((lineTargetIndex + i) < theWorld.GetComponent<World>().lines.Count &&
							    theWorld.GetComponent<World>().lines[lineTargetIndex + i].GetComponent<LineScript>().canEnter)
							{
								lineTarget = theWorld.GetComponent<World>().lines[lineTargetIndex + i];
								lineTargetIndex += i;
								theYPos = lineTarget.transform.position.y;
								theWorld.GetComponent<World>().updateCurrentLine(lineTarget);
								//Break out if we were able to move to a line
								break;
							}
						}
					}
				}				
				//Again, make this move if it's more than the minimal distance... but then also only do the more intended one.
				if (swipeDistHorizontal > minSwipeDistX && swipeDistHorizontal > swipeDistVertical) 					
				{					
					float swipeValue = Mathf.Sign(touch.position.x - startPos.x);				
					if (swipeValue > 0 && numInQ == 0 && origNumInQ == currentFirst) //Right Swipe
					{
						switchRight(0);
					}
					else if (swipeValue < 0 && numInQ == 0)//Left Swipe
					{
						gameObject.GetComponent<playerStats>().usePotion();
					}		
				}
				//Tap Attack
				if (swipeDistHorizontal <= minSwipeDistX && swipeDistVertical <= minSwipeDistY && numInQ == 0)
				{
					gameObject.GetComponent<playerStats>().attack();
				}

				break;
			}
		}
			//If first in line update this one's theYPos to the next line
			//In the future it will NOT be a hard coded value. Instead will go to the next line up or down!!

			//TODO Should do an axis for controllers and whatever is needed for touch screens but for now will just do getKeyDown

		//Detecting shooting
		if ((Input.GetKey("z") || Input.GetKey("space")) && numInQ == 0)
		{
			gameObject.GetComponent<playerStats>().attack();
		}

		//For using potions now! (not yet implemented)
		if (Input.GetKeyDown("left") && numInQ == 0)
		{
			gameObject.GetComponent<playerStats>().usePotion();
		}

		//Switches the player to the right
		if (Input.GetKeyDown("right") && numInQ == 0 && origNumInQ == currentFirst)
		{
			switchRight(0);
		}

		//If detect up, and we're not at the last line, and we're allowed to move up... then move up!
		if (Input.GetKeyDown("up"))// && (lineTargetIndex - 1) >= 0)
		    //&& theWorld.GetComponent<World>().lines[lineTargetIndex - 1].GetComponent<LineScript>().canEnter)
		{
			//For each line above this line
			for (int i = 1; (lineTargetIndex - i) >= 0; i++)
			{
				//If that is a line that we can enter... then enter it.
				if ((lineTargetIndex - i) >= 0 && theWorld.GetComponent<World>().lines[lineTargetIndex - i].GetComponent<LineScript>().canEnter)
				{
					lineTarget = theWorld.GetComponent<World>().lines[lineTargetIndex - i];
					lineTargetIndex -= i;
					theYPos = lineTarget.transform.position.y;
					theWorld.GetComponent<World>().updateCurrentLine(lineTarget);
					//Break out if we were able to move to a line
					break;
				}
			}
		}

		//Same as above, but with down input.
		if (Input.GetKeyDown("down"))
		{
			//For each line above this line
			for (int i = 1; (lineTargetIndex + i) < theWorld.GetComponent<World>().lines.Count; i++)
			{
				//If that is a line that we can enter... then enter it skipping the lines that we're not allowed onto.
				if ((lineTargetIndex + i) < theWorld.GetComponent<World>().lines.Count &&
				    theWorld.GetComponent<World>().lines[lineTargetIndex + i].GetComponent<LineScript>().canEnter)
				{
					lineTarget = theWorld.GetComponent<World>().lines[lineTargetIndex + i];
					lineTargetIndex += i;
					theYPos = lineTarget.transform.position.y;
					theWorld.GetComponent<World>().updateCurrentLine(lineTarget);
					//Break out if we were able to move to a line
					break;
				}
			}


//			lineTarget = theWorld.GetComponent<World>().lines[lineTargetIndex + 1];
//			lineTargetIndex++;
//			theYPos = lineTarget.transform.position.y;
//			theWorld.GetComponent<World>().updateCurrentLine(lineTarget);
		}

		if (numInQ == 0 && transform.position.x <= knockBackPosition)
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x + 3, 0);		
		}

		if (numInQ == 0 && transform.position.x > -5.9)
		{
			rigidbody2D.velocity = new Vector2 (0, 0);
			transform.position = new Vector2 (-6.0f, transform.position.y);
		}

		//Locking onto a line
		if(Mathf.Abs(transform.position.y - theYPos) < 0.05f)
		transform.position = new Vector2 (transform.position.x, theYPos);

		//If you're not first in line then you want to pay attention to how far the next in line is from you.
		if (numInQ != 0)
		{
			if (Mathf.Abs(transform.position.y - nextInQ.transform.position.y) > 0.5f)
			{
				leadIsFar = true;
			}
			else if (Mathf.Abs(transform.position.y - nextInQ.transform.position.y) < 0.05f)
			{
				leadIsFar = false;
				//clicking onto the line and updating theYPos
				transform.position = new Vector2 (transform.position.x, nextInQ.transform.position.y);
			}
		}

	

	}

	//Errr... use velocity?
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
		//Not first in line following movement.
		else
		{
			if (leadIsFar && transform.position.y < nextInQ.transform.position.y)
				transform.position = new Vector2 (transform.position.x, transform.position.y + 0.2f);
			else if (leadIsFar && transform.position.y > nextInQ.transform.position.y)
				transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
		}
	}

	//TODO
	//Switch the "attempt" with magic numbers to edge cases in a list of the number of dots
	//This way we get to have dynamic Q size... Not sure if I want that though...

	//Potentially get rid of. Right input will be fore using potions

	//Rcursively going from back player to first in Q switch the players around
	void switchRight (int attempt)
	{
		//Stop the thing from moving in the x direction on switch. The one that ends up in front should just move forward anyways
		rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
		gameObject.GetComponent<playerStats>().resetAttackTime();

		//base case
		if (attempt > 2)
		{
			//set the temp position to null.
			return;
		}
		else
		{
			//Debug.Log(attempt + " done by: " + thisName);

			//if (attempt == 0)
			//store current position in the temp value
			//Also store theYPos
			if (attempt == 0)
			{
				posOnSwitch = new Vector2 (transform.position.x, transform.position.y);
				theYPosOnSwitch = theYPos;
			}

			//if attempt == 0, or 1 then move to next in lines position (also copy theYPos)
			if (attempt == 0 || attempt == 1)
			{
				transform.position = new Vector2 (nextInQ.transform.position.x, nextInQ.transform.position.y);
			}
			//else if (attempt == 2) move to next in lines temp (because that one will have already have moved)
			else if (attempt == 2)
			{
				transform.position = new Vector2 (nextInQ.GetComponent<Movement>().posOnSwitch.x, nextInQ.GetComponent<Movement>().posOnSwitch.y);
				theYPos = nextInQ.GetComponent<Movement>().theYPosOnSwitch;
			}

			//No matter who it is. When we move right we want to decrease the position in Q for each dot.
			// Note that  +2%3 is equivalent to -1%3... but unity does mods in a way that (-1)%3 = -1 instead of 2.
			//Hence doing +2 instead of -1
			numInQ = (numInQ + 2) % 3;

			//Always incremenet the attempt because no matter what we will move onto the next dot.
			attempt++;

			//Send message (calling this method) to next in Q
			nextInQ.GetComponent<Movement>().switchRight(attempt);//SendMessage("switchRight", attempt);
		}

		//Only want collisions with the first in line... or DO WE?!?
		if (numInQ == 0)
		{
			collider2D.enabled = true;
		}
		else
		{
			collider2D.enabled = false;
		}
	}

	void switchLeft (int attempt)
	{
		//Stop the thing from moving in the x direction on switch. The one that ends up in front should just move forward anyways
		rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
		gameObject.GetComponent<playerStats>().resetAttackTime();

		//Same as above but negative
		
		if (attempt > 2)
		{
			//set the temp position to null.
			//Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~");
			return;
		}
		else
		{
			//Debug.Log(attempt + " LEFT done by: " + thisName);
			
			//if (attempt == 0)
			//store current position in the temp value
			//Also store theYPos
			if (attempt == 0)
			{
				posOnSwitch = new Vector2 (transform.position.x, transform.position.y);
				theYPosOnSwitch = theYPos;
			}
			
			//if attempt == 0, or 1 then move to next in lines position (also copy theYPos)
			if (attempt == 0 || attempt == 1)
			{
				transform.position = new Vector2 (prevInQ.transform.position.x, prevInQ.transform.position.y);
			}
			
			//else if (attempt == 2) move to next in lines temp
			else if (attempt == 2)
			{
				transform.position = new Vector2 (prevInQ.GetComponent<Movement>().posOnSwitch.x, prevInQ.GetComponent<Movement>().posOnSwitch.y);
				theYPos = prevInQ.GetComponent<Movement>().theYPosOnSwitch;
			}
			
			//No matter who it is, when going left we inscrease their pos in the Q by 1 (mod 3)
			numInQ = (numInQ + 1) % 3;

			//Always incremenet the attempt because no matter what we will move onto the next dot.
			attempt++;
			
			//Send message (calling this method) to prev in Q
			prevInQ.SendMessage("switchLeft", attempt);
		}

		//Only want collisions with the first in line!
		if (numInQ == 0)
		{
			//collider2D.enabled = true;
			gameObject.GetComponent<playerStats>().onCollider(true);
		}
		else
		{
			//collider2D.enabled = false;
			gameObject.GetComponent<playerStats>().onCollider(false);
		}
	}

	//Used for going to the next level
	public void newLevelRestart(GameObject world)
	{
		//The world that found us is the new world!
		theWorld = world;

		//Doing the lines thing again!
		foreach (GameObject line in theWorld.GetComponent<World>().lines)
		{
			if (line.transform.position.y == 0.0f)
				lineTarget = line;
		}
		//Start back in the middle
		theYPos = 0.0f;
		//The index is important here ... make sure that it matches up with the line at 0.0y... actually I don't know why I'm not just looking like I do in start...
		//lineTargetIndex = 7;
		lineTargetIndex = theWorld.GetComponent<World>().lines.IndexOf(lineTarget);
		transform.position = new Vector2 (transform.position.x, 0.0f);

		//Also tell the stats that we're in a world
		gameObject.GetComponent<playerStats>().newLvlWorld(theWorld);

	}

	//Manually set the line target... I don't think that this is being used...
	public void setLineTarget(GameObject trgt)
	{
		lineTarget = trgt;
	}

	public void setKnockBackPos (float kb)
	{
		knockBackPosition = kb;
	}


	//Mobile Android input:




}