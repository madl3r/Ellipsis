using UnityEngine;
using System.Collections;

public class blueBulletScript : BaseBulletScript {
	
	// Use this for initialization
	void Start () 
	{
		dmg = 1 + bnsDmg;
		bulletSpeed = 8.0f + bnsBulletSpeed;
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
	}

	void OffCameraRight()
	{
		Destroy(gameObject);
	}

	//Need to override this if you want the bullet to destroy bullets that come against you
	protected virtual void dealDamage(GameObject theHit)
	{
		if (theHit.tag == "player" || theHit.tag == "enemy")
		{
			//Debug.Log("DEALING " + dmg + " DAMAGE");
			theHit.SendMessage("takeDamage", dmg);
		}
		Destroy(gameObject);
	}
	
}
