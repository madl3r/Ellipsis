using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

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

	// Use this for initialization
	void Start () 
	{
		//getting this dots original position in the Q.
		origNumInQ = numInQ;
		//theYPost defaults to 0
		theYPos = 0.0f;
		//getting that empty GameObject
		theWorld = GameObject.FindGameObjectWithTag("theWorld");

		//DebugCode
		//theWorld.SendMessage("printMessage");

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

		//DebugCode
		//Debug.Log("the lineTarget's position" + lineTarget.transform.position + "With Index at: " + lineTargetIndex);

	}
	
	// Update is called once per frame
	void Update () 
	{
			//If first in line update this one's theYPos to the next line
			//In the future it will NOT be a hard coded value. Instead will go to the next line up or down!!

			//TODO Should do an axis for controllers... but for now will just do getKeyDown

			//instead of manually changing up or down should get the next line up or down in the list of lines from world.
			//if it's allowed to do that based on "World's" decision then set theYPos to that lines y pos.

		//Detecting shooting
		if ((Input.GetKeyDown("z") || Input.GetKeyDown("space")) && numInQ == 0)
		{
			gameObject.SendMessage("attack");
		}

		//If detect up, and we're not at the last line, and we're allowed to move up... then move up!
		if (Input.GetKeyDown("up") && (lineTargetIndex - 1) >= 0
		    && theWorld.GetComponent<World>().lines[lineTargetIndex - 1].GetComponent<LineScript>().canEnter)
		{
			//theYPos += 2f;
			lineTarget = theWorld.GetComponent<World>().lines[lineTargetIndex - 1];
			lineTargetIndex--;
			theYPos = lineTarget.transform.position.y;
			theWorld.GetComponent<World>().updateCurrentLine(lineTarget);
		}
		//Same as above, but with down input.
		if (Input.GetKeyDown("down") && (lineTargetIndex + 1) < theWorld.GetComponent<World>().lines.Count
		    && theWorld.GetComponent<World>().lines[lineTargetIndex + 1].GetComponent<LineScript>().canEnter)
		{
			lineTarget = theWorld.GetComponent<World>().lines[lineTargetIndex + 1];
			lineTargetIndex++;
			theYPos = lineTarget.transform.position.y;
			theWorld.GetComponent<World>().updateCurrentLine(lineTarget);
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
				//theYPos = transform.position.y; DON'T NEED THIS ANYMORE B/C THEYPOS WILL ALWAYS BE UPDATED
			}
		}

		//Switching right
		//If we get right input, only do on the first in line, and make sure that this is only the first time detected (SEE MoveHelperONCE)
		if (Input.GetKeyDown("right") && numInQ == 0 && origNumInQ == currentFirst)
		{
			//Debug.Log("GETKEYDOWN TOO MUCH. Current First is: " + currentFirst);
			switchRight(0);
		}
		//Same as above, but for switching left
		if (Input.GetKeyDown("left") && numInQ == 0 && origNumInQ == currentFirst)
		{
			//Debug.Log("GETKEYDOWN TOO MUCH ~~~~~~ LLL " + currentFirst);
			switchLeft(0);
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
		//Not first in line following movement.
		else
		{
			if (leadIsFar && transform.position.y < nextInQ.transform.position.y)
				transform.position = new Vector2 (transform.position.x, transform.position.y + 0.2f);
			else if (leadIsFar && transform.position.y > nextInQ.transform.position.y)
				transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
		}
	}

	//Switch attempt things with magic numbers to edge cases in a list of the number of dots
		//This way we get to have dynamic Q size
	void switchRight (int attempt)
	{

		if (attempt > 2)
		{
			//set the temp position to null.
			//Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~");
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
			nextInQ.SendMessage("switchRight", attempt);
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
}