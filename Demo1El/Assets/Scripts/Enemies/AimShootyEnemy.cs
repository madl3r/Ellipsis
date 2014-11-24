using UnityEngine;
using System.Collections;

public class AimShootyEnemy : BaseEnemy {

	public GameObject attackType;
	private float timeBetweenShot;
	private float prevShotTime;

	private GameObject playerToLookAt;
	private Vector3 eulerAngleOffset;

	// Use this for initialization
	void Start () {
		//Spawn position start
		if (worldSpawned)
			transform.position = new Vector2(Random.Range(7.0f,8.5f), Random.Range(-2, 3)*2);

		hp = 4;
		dmg = 1;
		prevShotTime = Time.time;

		//Making so that we're looking at the -x direction instead of the Z direction.
		eulerAngleOffset = new Vector3(0, 90, 0);

		playerToLookAt = GameObject.FindGameObjectWithTag("Player");

		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		//Look at the player
		if (playerToLookAt != null)
		{
			//transform.LookAt(playerToLookAt.transform.position);
			transform.LookAt(new Vector2 (-6.0f, playerToLookAt.transform.position.y));
			transform.Rotate(eulerAngleOffset, Space.Self);
		}

		//Every now and again move
		if (Time.time - prevShotTime > timeBetweenShot)
		{
			attack ();
		}
	}

	//attacks the player and semi random intervals
	protected override void attack()
	{
		prevShotTime = Time.time;
		timeBetweenShot = Random.Range(0.75f, 1.0f);
		Instantiate(attackType, transform.position, transform.rotation);
	}
}
