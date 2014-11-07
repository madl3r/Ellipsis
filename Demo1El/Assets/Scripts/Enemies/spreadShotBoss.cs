using UnityEngine;
using System.Collections;

public class spreadShotBoss : BaseEnemy {

	public GameObject attackType;
	private float timeBetweenShot;
	private float prevShotTime;
	private float timeBetweenMove;
	private float prevMoveTime;
	//	private bool worldSpawned = true;
	private float theYPos;
	private Vector2 startingPos;


	// Use this for initialization
	void Start () {
	
		startingPos = transform.position;
		theYPos = transform.position.y;
		hp = 20;
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
	
	protected override void attack()
	{
		prevShotTime = Time.time;
		timeBetweenShot = Random.Range(0.75f, 1.25f);
		Instantiate(attackType, transform.position, transform.rotation);
	}
	
	protected virtual void move()
	{
		prevMoveTime = Time.time;
		timeBetweenMove = Random.Range(1.5f, 2.0f);
		
		if (Random.Range(0, 2) == 0)
		{
			//move up
			theYPos += 2.0f;
			//If going too high, then just move down
			if (theYPos > 2.0f + startingPos.y)
				theYPos = 2.0f + startingPos.y;
		}
		else
		{
			//move down
			theYPos -= 2.0f;
			//If we're going too low, then just move up
			if (theYPos < startingPos.y - 2.0f)
				theYPos = startingPos.y - 2.0f;
		}
	}
	
	//	public void isWorldSpawned(bool b)
	//	{
	//		worldSpawned = b;
	//	}

	protected override void takeDamage(int dmg)
	{
		hp -= dmg;
		
		if (hp <= 0)
		{
			//Tell the world that you died.
			dahWorld.GetComponent<World>().finishBossRound();
			Destroy(gameObject);
		}
	}


	public void setPos(Vector2 thePos)
	{
		transform.position = thePos;
	}
}
