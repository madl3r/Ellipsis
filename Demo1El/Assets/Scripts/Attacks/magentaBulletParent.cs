using UnityEngine;
using System.Collections;

public class magentaBulletParent : attackTypeScript {

	// Use this for initialization
	void Start () {

		//TODO need to kill evenetually (I think is already done by the bullets)
		transform.position = new Vector2 (transform.position.x + 0.5f, transform.position.y);

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
