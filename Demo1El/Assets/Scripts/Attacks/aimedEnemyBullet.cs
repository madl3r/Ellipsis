using UnityEngine;
using System.Collections;

public class aimedEnemyBullet : BaseBulletScript {



	// Use this for initialization
	void Start () {
		dmg = 1 + bnsDmg;
		bulletSpeed = -12.0f + bnsBulletSpeed;

		rigidbody2D.AddForce(gameObject.transform.right * bulletSpeed);
		rigidbody2D.velocity = transform.right * bulletSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -30)
			Destroy(gameObject);
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
			//Destroy(gameObject);
		}
		//if the game object has a tag of player... then deal damage!
		//if the game object has the tag of a damage thingy (bullet we'll call them)... then take damage!
	}
}
