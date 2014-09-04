using UnityEngine;
using System.Collections;

public class BaseBulletScript : MonoBehaviour {

	protected int dmg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	protected virtual void dealDamage(GameObject theHit)
	{
		theHit.SendMessage("takeDamage", dmg);
		Destroy(gameObject);
	}
	
}
