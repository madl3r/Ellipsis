using UnityEngine;
using System.Collections;

public class redBulletScript : BaseBulletScript {

	// Use this for initialization
	void Start () {
		
		dmg = 1 + bnsDmg;
		bulletSpeed = 12.0f + bnsBulletSpeed;
		duration = 4f + bnsDuration;
		startTime = Time.time;

		//The Math for it is here.
		//abs(v) = sqrt(x^2 + y^2);
		// v^2 = (x^2 + y^2)
		// sqrt(v^2 - x^2) = sqrt(y^2)

		//Choosing a random speed for the xSpeed
		float xSpeed = Random.Range((12.9f/13.0f) * bulletSpeed, bulletSpeed);
		//Now setting the ySpeed based on the random xSpeed (should also randomize if the y it pos or negative
		float negOrNot;
		if (Random.Range(0, 2) == 0)
			negOrNot = -1;
		else
			negOrNot = 1;

		float ySpeed = Mathf.Sqrt((bulletSpeed*bulletSpeed) - (xSpeed*xSpeed)) * negOrNot;

		rigidbody2D.velocity = new Vector2(xSpeed, ySpeed);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > 30)
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

	//This dies against everything that it hits
	protected override void dealDamage(GameObject theHit)
	{
		if (theHit.tag == "enemy")
		{
			Debug.Log("RED BULLET HITTING ENEMY!!!");
			theHit.SendMessage("takeDamage", dmg);
			Destroy(gameObject);
//			hitting enemy!
		}
		else if (theHit.tag == "enemyBullet")
		{
			Destroy(gameObject);
		}

	}
	
	protected void OnTriggerStay2D(Collider2D other)
	{
		dealDamage(other.gameObject);
	}

	protected void OnTriggerExit2D(Collider2D other)
	{
		dealDamage(other.gameObject);
	}

}
