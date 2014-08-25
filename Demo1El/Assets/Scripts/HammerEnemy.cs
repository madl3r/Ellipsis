using UnityEngine;
using System.Collections;

public class HammerEnemy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		transform.position = new Vector2 (transform.position.x - 0.1f, transform.position.y);
	}

	void OffCameraLeft()
	{
		//Should maybe also choose a new line to go onto.
		transform.position = new Vector2 (11.0f, transform.position.y);
	}

	//Currently ONLY collides when theHammer isKinematic... wtf!?
	void OnCollisionEnter2D(Collision2D coll)
	{
		transform.position = new Vector2 (transform.position.x + 4, transform.position.y);
		Debug.Log("THE HAMMER JUST HIT SOMETHING!");
	}
}
