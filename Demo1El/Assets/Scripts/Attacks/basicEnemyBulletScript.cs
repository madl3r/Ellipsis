using UnityEngine;
using System.Collections;

public class basicEnemyBulletScript : BaseBulletScript {

	// Use this for initialization
	void Start () {
		dmg = 1 + bnsDmg;
		bulletSpeed = -12.0f + bnsBulletSpeed;
		rigidbody2D.velocity = new Vector2 (bulletSpeed, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < -30)
			Destroy(gameObject);
	}

	//Checks for when enemy bullets hit things
	void OnTriggerEnter2D(Collider2D other)
	{
		//If it hits the player then by default we'll deal damage by telling the player to take damage, and then destroy ourselves
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.SendMessage("takeDamage", dmg);
			Destroy(gameObject);
			//Make noise and some effect
		}
		//If we hit another bullet we tell them to damage to us (this bullet). This might result in this bullet being destroyed.
		if (other.gameObject.tag == "bullet")
		{
			other.gameObject.SendMessage("dealDamage", gameObject);
		}
	}

}
