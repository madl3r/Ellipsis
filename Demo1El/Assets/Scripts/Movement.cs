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
		if (numInLine == 0 && nextInLine == null)
		{
			if (Input.GetKeyDown("up") && theYPos < 4.0f)
			{
				theYPos += 2f;
			}
			else if (Input.GetKeyDown("down") && theYPos > -4.0f)
			{
				theYPos -= 2f;
			}

			if(Mathf.Abs(transform.position.y - theYPos) < 0.05f)
				transform.position = new Vector2 (transform.position.x, theYPos);
		}
		else
		{
			if (Mathf.Abs(transform.position.y - nextInLine.transform.position.y) > 0.5f)
			{
				leadIsFar = true;
			}
			else if (Mathf.Abs(transform.position.y - nextInLine.transform.position.y) < 0.01f)
			{
				leadIsFar = false;
				transform.position = new Vector2 (transform.position.x, nextInLine.transform.position.y);
			}
		}

	}

	void FixedUpdate()
	{

		if (numInLine == 0 && nextInLine == null)
		{
			if (transform.position.y < theYPos)
				transform.position = new Vector2 (transform.position.x, transform.position.y + 0.2f);
			else if (transform.position.y > theYPos)
				transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
		}
		else
		{
			if (leadIsFar && transform.position.y < nextInLine.transform.position.y)
				transform.position = new Vector2 (transform.position.x, transform.position.y + 0.2f);
			else if (leadIsFar && transform.position.y > nextInLine.transform.position.y)
				transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
		}
	}
}