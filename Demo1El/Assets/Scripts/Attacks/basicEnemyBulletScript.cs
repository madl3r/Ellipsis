using UnityEngine;
using System.Collections;

public class basicEnemyBulletScript : BaseBulletScript {

	// Use this for initialization
	void Start () {
		dmg = 1;
		rigidbody2D.velocity = new Vector2 (-12f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -30)
			Destroy(gameObject);
	}

	void FixedUpdate()
	{
		//transform.position = new Vector2 (transform.position.x - 0.3f, transform.position.y);
	}
	
	void OffCameraLeft()
	{
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.SendMessage("takeDamage", dmg);
			Destroy(gameObject);
			//Make noise and some effect
		}
		if (other.gameObject.tag == "bullet")
		{
			//Destroy(other);
			other.gameObject.SendMessage("dealDamage", gameObject);
			Destroy(gameObject);
		}
		//if the game object has a tag of player... then deal damage!
		//if the game object has the tag of a damage thingy (bullet we'll call them)... then take damage!
	}

}
