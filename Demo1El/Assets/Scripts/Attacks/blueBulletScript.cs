using UnityEngine;
using System.Collections;

public class blueBulletScript : BaseBulletScript {

	//Camera variables used for bouncing off of things.
	private GameObject theCamGameObj;
	private Camera theCam;
	Vector3 posOnScreen;
	//Variables that track the time that this has been alive for and when the bullets become dangerous to the player
	private float theStartTime;
	private float playerDeadlyTime;

	// Use this for initialization
	void Start () 
	{
		theStartTime = Time.time;
		playerDeadlyTime = 0.5f;
		theCamGameObj = GameObject.FindGameObjectWithTag("MainCamera");
		theCam = theCamGameObj.GetComponent<Camera>();
		dmg = 1 + bnsDmg;
		bulletSpeed = 19.0f + bnsBulletSpeed;
		startTime = Time.time;
		rigidbody2D.velocity = new Vector2(bulletSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Constantly getting this bullets position on the screen.
		posOnScreen = theCam.WorldToViewportPoint(gameObject.transform.position);

		//If we hit the end of the screen bounce back towards the player.
		if (posOnScreen.x > 0.99f)
		{
			rigidbody2D.velocity = new Vector2 (-bulletSpeed * 1.25f, 0);
			//Now it needs to act like an enemy bullet as well as a player bullet.
			gameObject.GetComponent<Collider2D>().isTrigger = true;
		}
	}

	//Rotate around for looking cool
	void FixedUpdate()
	{
		transform.RotateAround(transform.position, new Vector3 (0, 0, 1), 15f);
	}

	//Slightly overriden dealDamage method, this gun never dies on any traditional contact.
	protected override void dealDamage(GameObject theHit)
	{
		if (theHit.tag == "player" || theHit.tag == "enemy")
		{
			theHit.SendMessage("takeDamage", dmg);
		}
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
		//if we collided with a bullet, have the bullet deal damage to us.
		//This is incase some enemy bullet is supposed to totally destory all things it hits.
		if (other.gameObject.tag == "bullet")
		{
			other.gameObject.SendMessage("dealDamage", gameObject);
		}
	}
	
}
