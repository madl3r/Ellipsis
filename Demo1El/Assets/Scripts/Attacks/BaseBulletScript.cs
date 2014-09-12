using UnityEngine;
using System.Collections;

public class BaseBulletScript : attackTypeScript{



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected virtual void dealDamage(GameObject theHit)
	{
		if (theHit.tag == "player" || theHit.tag == "enemy")
		{
			theHit.SendMessage("takeDamage", dmg);
		}
		Destroy(gameObject);
	}
	
}
