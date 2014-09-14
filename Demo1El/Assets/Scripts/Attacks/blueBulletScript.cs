using UnityEngine;
using System.Collections;

public class blueBulletScript : BaseBulletScript {


//	int dmg = 1;
//	float bulletSpeed = 17.0f;
//	float duration = 1.0f;
//	float defaultAttackSpeed = 4.0f;

	// Use this for initialization
	void Start () {
		dmg = 1 + bnsDmg;
		bulletSpeed = 17.0f + bnsBulletSpeed;
		//duration = 0.5f;
		startTime = Time.time;
		//defaultAttackSpeed = 4.0f;
		rigidbody2D.velocity = new Vector2(bulletSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > 30)
			Destroy(gameObject);

//		if (Time.time - startTime > duration)
//			Destroy(gameObject);


	}

	void FixedUpdate()
	{
		//transform.position = new Vector2 (transform.position.x + 0.3f, transform.position.y);
	}

	void OffCameraRight()
	{
		Destroy(gameObject);
	}

	
}
