using UnityEngine;
using System.Collections;

public class enemySpreadShotScript : BaseBulletScript {

	// Use this for initialization
	void Start () {
		
		dmg = 1 + bnsDmg;
		bulletSpeed = -(10.0f + bnsBulletSpeed);
		duration = 4f + bnsDuration;
		startTime = Time.time;
		
		//The Math for it is here.
		//abs(v) = sqrt(x^2 + y^2);
		// v^2 = (x^2 + y^2)
		// sqrt(v^2 - x^2) = sqrt(y^2)
		
		//Choosing a random speed for the xSpeed
		float xSpeed = Random.Range((12.5f/13.0f) * bulletSpeed, bulletSpeed);
		//Now setting the ySpeed based on the random xSpeed (should also randomize if the y it pos or negative
		float negOrNot;
		if (Random.Range(0, 2) == 0)
			negOrNot = -1;
		else
			negOrNot = 1;
		
		float ySpeed = Mathf.Sqrt((bulletSpeed*bulletSpeed) - (xSpeed*xSpeed)) * negOrNot;
		
		rigidbody2D.velocity = new Vector2(xSpeed, ySpeed);
	}
	
	void Update () {
		if (transform.position.x < -30)
		{
			Destroy(gameObject);
			Destroy(transform.parent.gameObject);
		}
		
		if (Time.time - startTime > duration)
		{
			Destroy(transform.parent.gameObject);
			Destroy(gameObject);
		}
		
	}
	
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
