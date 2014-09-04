using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

	protected int hp;
	protected int dmg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	
	protected virtual void takeDamage(int dmg)
	{
		hp -= dmg;
		
		if (hp <= 0)
			Destroy(gameObject);
	}

}
