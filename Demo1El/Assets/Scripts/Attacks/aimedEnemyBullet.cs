using UnityEngine;
using System.Collections;

public class aimedEnemyBullet : BaseBulletScript {



	// Use this for initialization
	void Start () {
		dmg = 1 + bnsDmg;
		bulletSpeed = -12.0f + bnsBulletSpeed;
		//Giving speed to the bullet
		rigidbody2D.velocity = transform.right * bulletSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		//Destory this bullet if its too far left

		//TODO might want to get rid of this if we can ensure that the off Camera left can catch everything
		if (transform.position.x < -30)
			Destroy(gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		//hit the player and then die
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.SendMessage("takeDamage", dmg);
			Destroy(gameObject);
			//Make noise and some effect
		}
		//If it hits a bullet, deal damage to that bullet... but don't die yourself.
//		if (other.gameObject.tag == "bullet")
//		{
//			other.gameObject.SendMessage("dealDamage", gameObject);
//		}
	}
}
