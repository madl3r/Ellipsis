using UnityEngine;
using System.Collections;

public class redSlashScript : attackTypeScript {
	


	// Use this for initialization
	void Start () {
		dmg = 2;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate()
	{
		transform.position = new Vector2 (transform.position.x, transform.position.y + 0.025f);
	}

}
