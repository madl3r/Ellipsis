using UnityEngine;
using System.Collections;

public class redSlashScript : BaseBulletScript {
	


	// Use this for initialization
	void Start () {
		startTime = Time.time;
		dmg = 2 + bnsDmg;
		bulletSpeed = 1.42f + bnsBulletSpeed;
		duration = 0.5f + bnsDuration;
		rigidbody2D.velocity = new Vector2 (0, bulletSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > duration)
		{
			Destroy(transform.parent.gameObject);
			Destroy(gameObject);
		}

	}

	void FixedUpdate()
	{
		//transform.position = new Vector2 (transform.position.x, transform.position.y + 0.025f);
	}

}
