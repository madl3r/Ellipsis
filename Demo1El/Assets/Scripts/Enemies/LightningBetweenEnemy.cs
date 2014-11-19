using UnityEngine;
using System.Collections;

public class LightningBetweenEnemy : BaseEnemy {

	private bool recentlyDamaged;
	private float attackTime;
	private float timeBtwnAttacks;


	void Start () {
		recentlyDamaged = false;
		timeBtwnAttacks = 1.5f;
		hp = 50;
		dmg = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (recentlyDamaged && (Time.time - attackTime) > timeBtwnAttacks)
			recentlyDamaged = false;
		
	}

	protected void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("DIS SHIT IS GETTIN HIT");
		//if we're hitting a player, and we haven't recently damaged them then deal damage.
		if (other.gameObject.tag == "Player" && !recentlyDamaged)
		{
			Debug.Log("lightning hit");
			Debug.Log("Hitting player");
			recentlyDamaged = true;
			other.gameObject.SendMessage("takeDamage", dmg);
			//wait half a second before being able to deal damage again
			
			attackTime = Time.time;
			//Make noise and some effect
		}
//		if (other.gameObject.tag == "bullet")
//		{
//			//Debug.Log("HIT BY A BULLET");
//			other.gameObject.SendMessage("dealDamage", gameObject);
//		}
	}

	protected override void OffCameraLeft()
	{
		transform.position = new Vector2 (10.9f, transform.position.y);
	}

	//Doesn't take damage, so do nothing to HP.
	protected override void takeDamage(int dmg)
	{
		;
	}

}
