using UnityEngine;
using System.Collections;

public class blueBulletScript : BaseBulletScript {

	private GameObject theCamGameObj;
	private Camera theCam;
	Vector3 posOnScreen;
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
		//duration = 0.5f;
		startTime = Time.time;
		//defaultAttackSpeed = 4.0f;
		rigidbody2D.velocity = new Vector2(bulletSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.position.x > 30)
			Destroy(gameObject);

		posOnScreen = theCam.WorldToViewportPoint(gameObject.transform.position);

		if (posOnScreen.x > 0.99f)
		{
			Debug.Log("TIME TO BOUNCE BACK");
			rigidbody2D.velocity = new Vector2 (-bulletSpeed * 1.25, 0);
			gameObject.GetComponent<Collider2D>().isTrigger = true;
		}
	}

	void FixedUpdate()
	{
		transform.RotateAround(transform.position, new Vector3 (0, 0, 1), 15f);
	}

	void OffCameraRight()
	{
		Destroy(gameObject);
	}

	void OffCameraLeft()
	{
		Destroy(gameObject);
	}

	//Need to override this if you want the bullet to destroy bullets that come against you
	protected override void dealDamage(GameObject theHit)
	{
		if (theHit.tag == "player" || theHit.tag == "enemy")
		{
			//Debug.Log("DEALING " + dmg + " DAMAGE");
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
		//if we collided with a bullet, have the bullet deal damage to us.
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
