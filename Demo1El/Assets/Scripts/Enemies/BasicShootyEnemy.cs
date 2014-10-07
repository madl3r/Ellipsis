using UnityEngine;
using System.Collections;

public class BasicShootyEnemy : BaseEnemy {

	public GameObject attackType;
	private float timeBetweenShot;
	private float prevShotTime;
	private float timeBetweenMove;
	private float prevMoveTime;

	private float theYPos;


	// Use this for initialization
	void Start () {
		theYPos = transform.position.y;
		hp = 1;
		dmg = 1;
		prevMoveTime = Time.time;
		prevShotTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		//Every now and again move
		if (Time.time - prevMoveTime > timeBetweenMove)
		{
			move ();
		}

		//Every now and again attack
		if (Time.time - prevShotTime > timeBetweenShot)
		{
			attack ();
		}

		//Locking onto a line
		if(Mathf.Abs(transform.position.y - theYPos) < 0.05f)
			transform.position = new Vector2 (transform.position.x, theYPos);

	}

	void FixedUpdate()
	{
		//TODO Get enemy movement and player movement to be velocity based. NOT fixed update
		if (transform.position.y < theYPos)
			transform.position = new Vector2 (transform.position.x, transform.position.y + 0.2f);
		else if (transform.position.y > theYPos)
			transform.position = new Vector2 (transform.position.x, transform.position.y - 0.2f);
	}
	
	protected virtual void attack()
	{
		prevShotTime = Time.time;
		timeBetweenShot = Random.Range(0.5f, 1.0f);
		Instantiate(attackType, transform.position, transform.rotation);
	}

	protected virtual void move()
	{
		prevMoveTime = Time.time;
		timeBetweenMove = Random.Range(0.75f, 1.0f);

		if (Random.Range(0, 2) == 0)
		{
			//move up
			theYPos += 2.0f;
			if (theYPos > 4.0f)
				theYPos = 4.0f;
		}
		else
		{
			//move down
			theYPos -= 2.0f;
			if (theYPos < -4.0f)
				theYPos = -4.0f;
		}


	}
	
}
