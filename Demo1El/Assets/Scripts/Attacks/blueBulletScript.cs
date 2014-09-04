using UnityEngine;
using System.Collections;

public class blueBulletScript : attackTypeScript {
	
	// Use this for initialization
	void Start () {
		dmg = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > 30)
			Destroy(gameObject);
	}

	void FixedUpdate()
	{
		transform.position = new Vector2 (transform.position.x + 0.3f, transform.position.y);
	}

	void OffCameraRight()
	{
		Destroy(gameObject);
	}
	
}
