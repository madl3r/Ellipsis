using UnityEngine;
using System.Collections;

public class magentaBladeBullet : BaseBulletScript {

	//Camera vars
	private GameObject theCamGameObj;
	private Camera theCam;
	Vector3 posOnScreen;
	//timing vars
	private float theStartTime;
	private float playerDeadlyTime;
	//vars for shotgun effect of this bullet
	private float xSpeed;
	private float ySpeed;
	
	// Use this for initialization
	void Start () 
	{
		theStartTime = Time.time;
		playerDeadlyTime = 0.5f;
		theCamGameObj = GameObject.FindGameObjectWithTag("MainCamera");
		theCam = theCamGameObj.GetComponent<Camera>();
		dmg = 1 + bnsDmg;
		bulletSpeed = 13.0f + bnsBulletSpeed;

		//Choosing a random speed for the xSpeed
		xSpeed = Random.Range((12.75f/13.0f) * bulletSpeed, bulletSpeed);
		//Now setting the ySpeed based on the random xSpeed (should also randomize if the y it pos or negative
		float negOrNot;
		if (Random.Range(0, 2) == 0)
			negOrNot = -1;
		else
			negOrNot = 1;
		//Some vector math here
		ySpeed = Mathf.Sqrt((bulletSpeed*bulletSpeed) - (xSpeed*xSpeed)) * negOrNot;
		
		rigidbody2D.velocity = new Vector2(xSpeed, ySpeed);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Still might not be needed.
		if (transform.position.x > 30)
			Destroy(gameObject);

		if (transform.position.x < -40)
			Destroy(gameObject);

		//always tracking the bullets position on the screen.
		posOnScreen = theCam.WorldToViewportPoint(gameObject.transform.position);

		//If we hit any edge of the screen then bounce off in a reasonable way and make it deadly to the player.
		//Also speed up so that the player is freaking out more!
		if (posOnScreen.x > 0.99f)
		{
			rigidbody2D.velocity = new Vector2 (xSpeed * -1.5f, ySpeed * -1.5f);
			gameObject.GetComponent<Collider2D>().isTrigger = true;
		}
		if (posOnScreen.y > 0.99f)
		{
			rigidbody2D.velocity = new Vector2 (xSpeed * 1.5f, ySpeed * -1.5f);
			gameObject.GetComponent<Collider2D>().isTrigger = true;
		}
		if (posOnScreen.y < 0.01f)
		{
			rigidbody2D.velocity = new Vector2 (xSpeed * 1.5f, ySpeed * -1.5f);
			gameObject.GetComponent<Collider2D>().isTrigger = true;
		}
	}
	
	// rotate for coolness!
	void FixedUpdate()
	{
		transform.RotateAround(transform.position, new Vector3 (0, 0, 1), 15f);
	}
	
	//Need to override this if you want the bullet to destroy bullets that come against you
	protected override void dealDamage(GameObject theHit)
	{
		if (theHit.tag == "player" || theHit.tag == "enemy")
		{
			theHit.SendMessage("takeDamage", dmg);
		}
		//Destroy(gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		//if we collided with a player, make that player take damage.
		if (other.gameObject.tag == "Player" && (Time.time - theStartTime) > playerDeadlyTime)
		{
			other.gameObject.SendMessage("takeDamage", dmg);
			Destroy(gameObject);
			//Make noise and some effect
		}
//		//if we collided with a bullet, have the bullet deal damage to us and then die
//		if (other.gameObject.tag == "enemyBullet")
//		{
//			other.gameObject.SendMessage("dealDamage", gameObject);
//			Destroy(gameObject);
//		}
	}
}
