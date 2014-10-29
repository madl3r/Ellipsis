using UnityEngine;
using System.Collections;

public class MoveHelperONCE : MonoBehaviour {


	//Touch Control Variables
	public static float minSwipeDistY = 50;
	public static float minSwipeDistX = 50;
	private Vector2 startPos;

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

		//Touch control got a right swipe input, so do the thing.
		if (Input.touchCount > 0) 
		{			
			Touch touch = Input.touches[0];						
			
			switch (touch.phase) 				
			{				
			case TouchPhase.Began:				
				startPos = touch.position;				
				break;								
				
			case TouchPhase.Ended:				
				float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
				float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;				
				if (swipeDistHorizontal > minSwipeDistX && swipeDistHorizontal > swipeDistVertical) 					
				{					
					float swipeValue = Mathf.Sign(touch.position.x - startPos.x);				
					if (swipeValue > 0) //Right Swipe
					{
						Movement.currentFirst = (Movement.currentFirst + 1) % 3;
					}	
				}
				
				break;
			}
		}

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
