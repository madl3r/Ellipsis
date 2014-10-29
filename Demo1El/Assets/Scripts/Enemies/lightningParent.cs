using UnityEngine;
using System.Collections;

public class lightningParent : BaseEnemy {


	public GameObject enemySpawn;

	// Use this for initialization
	void Start () {
		transform.position = new Vector2(Random.Range(8.0f, 11.0f), 0);
		rigidbody2D.velocity = new Vector2 (Random.Range(-3.0f, -5.0f), 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected override void OffCameraLeft()
	{
		//Should maybe also choose a new line to go onto.
		transform.position = new Vector2 (Random.Range(10.0f, 12.0f), transform.position.y);
	}

	public void lightningDie(float deadYPos)
	{
		//Don't count the death, because this enemy spawns another one after its death.
		if (deadYPos > 0)
		{
			//spawn an basic shooty enemy at -4y
			GameObject e = Instantiate(enemySpawn, new Vector2(transform.position.x, -4.0f), Quaternion.identity) as GameObject;
			e.GetComponent<BasicShootyEnemy>().isWorldSpawned(false);
			e.GetComponent<BaseEnemy>().setTheWorld(dahWorld);
		}
		else
		{
			//spawn a basic shooty at +4y
			GameObject e = Instantiate(enemySpawn, new Vector2(transform.position.x, 4.0f), Quaternion.identity) as GameObject;
			e.GetComponent<BasicShootyEnemy>().isWorldSpawned(false);
			e.GetComponent<BaseEnemy>().setTheWorld(dahWorld);
		}


		Destroy(gameObject);
	}

	protected override void OnTriggerEnter2D(Collider2D other)
	{
		//Do nothing... I guess parents get hit too when their kids are hit 
		;
	}

}
