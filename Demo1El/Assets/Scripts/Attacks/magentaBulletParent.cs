using UnityEngine;
using System.Collections;

public class magentaBulletParent : attackTypeScript {

	private float startTime;
	private float lifeTime;

	// Use this for initialization
	void Start () {
		lifeTime = 3.5f;
		startTime = Time.time;

		transform.position = new Vector2 (transform.position.x + 0.0f, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > lifeTime)
		{
			Destroy(gameObject);
		}
	}
}
