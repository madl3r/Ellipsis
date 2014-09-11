using UnityEngine;
using System.Collections;

public class greenBounceScript : attackTypeScript{
	private float startTime;
	private float lifeTime;
	public GameObject greenReflection;
	
	// Use this for initialization
	void Start () {
		lifeTime = 0.2f;
		startTime = Time.time;
		transform.position = new Vector2 (transform.position.x + 1, transform.position.y);
		dmg = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > lifeTime)
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
