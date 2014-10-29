using UnityEngine;
using System.Collections;

public class AimChargeEnemy : BaseEnemy {

	//Damage variables
	private bool recentlyDamaged;
	private float timeBtwnDamage;
	private float attackTime;

	//Choosing to attack vars
	private float timeBtwnAttacks;
	private float prevShotTime;
	private bool attacking;
	private Vector2 startingPos;

	//Look at vars
	private GameObject playerToLookAt;
	private Vector3 eulerAngleOffset;

	// Use this for initialization
	void Start () {

		//Spawn position start
		transform.position = new Vector2(Random.Range(7.0f,8.5f), Random.Range(-2, 3)*2);
		startingPos = transform.position;

		//initializing the stuff
		recentlyDamaged = false;
		timeBtwnAttacks = Random.Range(0.5f, 1.5f);
		timeBtwnDamage = 0.5f;
		hp = 2;
		dmg = 3;
		prevShotTime = Time.time;
		attacking = false;

		//Making so that we're looking at the -x direction instead of the Z direction.
		eulerAngleOffset = new Vector3(0, 90, 0);

		//Getting the player to look at... is there a better way to do this than to use find?
		playerToLookAt = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		//Make sure that it doesn't double hit.
		if (recentlyDamaged && (Time.time - attackTime) > timeBtwnDamage)
			recentlyDamaged = false;

		//Look at the player
		if (playerToLookAt != null && !attacking)
		{
			transform.LookAt(playerToLookAt.transform.position);
			transform.Rotate(eulerAngleOffset, Space.Self);
		}
		
		//Checking if we can, and then attacking
		if (Time.time - prevShotTime > timeBtwnAttacks)
		{
			attack ();
		}

	}

	//attacks the player and semi random intervals
	protected override void attack()
	{
		attacking = true;
		rigidbody2D.velocity = transform.right * -15.0f;
	}

	protected override void OffCameraLeft()
	{
		//Should maybe also choose a new line to go onto.
		attacking = false;
		rigidbody2D.velocity = new Vector2(0, 0);
		transform.position = startingPos;
		prevShotTime = Time.time;
		timeBtwnAttacks = Random.Range(0.5f, 2.0f);

	}

	protected override void OnTriggerEnter2D(Collider2D other)
	{
		//if we're hitting a player, and we haven't recently damaged them then deal damage.
		if (other.gameObject.tag == "Player" && !recentlyDamaged)
		{
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
