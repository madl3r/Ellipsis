using UnityEngine;
using System.Collections;

public class blackBulletScript : BaseBulletScript {

	// Use this for initialization
	void Start () 
	{
		dmg = 1 + bnsDmg;
		bulletSpeed = 17.0f + bnsBulletSpeed;
		startTime = Time.time; //TODO might get rid of.
		rigidbody2D.velocity = new Vector2(bulletSpeed, 0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.x > 30)
			Destroy(gameObject);
	}

}
