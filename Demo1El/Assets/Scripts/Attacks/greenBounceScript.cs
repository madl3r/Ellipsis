using UnityEngine;
using System.Collections;

public class greenBounceScript : BaseBulletScript{

	public GameObject greenReflection;
	
	// Use this for initialization
	void Start () {
		dmg = 2 + bnsDmg;
		duration = 0.2f + bnsDuration;
		startTime = Time.time;
		transform.position = new Vector2 (transform.position.x + 1, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > duration)
			Destroy(gameObject);
	}

	protected override void dealDamage(GameObject theHit)
	{
		if (theHit.tag == "enemy")
			theHit.SendMessage("takeDamage", dmg);
		else if (theHit.tag == "enemyBullet")
		{
			//reflect bullet
			//Debug.Log("REFLECT BULLET");
			Instantiate(greenReflection, transform.position, transform.rotation);
		}

		Destroy(gameObject);
	}

}
