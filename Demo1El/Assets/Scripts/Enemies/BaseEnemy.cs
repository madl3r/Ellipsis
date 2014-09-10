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

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "bullet")
		{
			Debug.Log("HIT BY A BULLET");
			other.gameObject.SendMessage("dealDamage", gameObject);
		}
	}

	
	
	protected virtual void takeDamage(int dmg)
	{
		hp -= dmg;
		
		if (hp <= 0)
		{
			//Tell the world that you died.
			GameObject.FindWithTag("theWorld").SendMessage("enemyKilled");
			Destroy(gameObject);
		}
	}



}
