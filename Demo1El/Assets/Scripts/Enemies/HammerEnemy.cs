using UnityEngine;
using System.Collections;

public class HammerEnemy : BaseEnemy {

	private bool recentlyDamaged;
	private float attackTime;
	private float timeBtwnAttacks;
	// Use this for initialization
	void Start () {

		//Spawn position start
		transform.position = new Vector2(Random.Range(7.0f,9.5f), (float)(Random.Range(-2, 3)*2));

		recentlyDamaged = false;
		timeBtwnAttacks = 0.5f;
		hp = 3;
		dmg = 2;
		rigidbody2D.velocity = new Vector2 (Random.Range(-12.0f, -8.99f), 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (recentlyDamaged && (Time.time - attackTime) > timeBtwnAttacks)
			recentlyDamaged = false;

	}

	protected override void OffCameraLeft()
	{
		//Should maybe also choose a new line to go onto.
		transform.position = new Vector2 (11.0f, transform.position.y);
	}

	protected override void OnTriggerEnter2D(Collider2D other)
	{
		//if we're hitting a player, and we haven't recently damaged them then deal damage.
		if (other.gameObject.tag == "Player" && !recentlyDamaged)
		{
			Debug.Log("Hitting player");
			recentlyDamaged = true;
			other.gameObject.SendMessage("takeDamage", dmg);
			//wait half a second before being able to deal damage again

			attackTime = Time.time;
			//Make noise and some effect
		}
		if (other.gameObject.tag == "bullet")
		{
			//Debug.Log("HIT BY A BULLET");
			other.gameObject.SendMessage("dealDamage", gameObject);
		}
	}
	

}
