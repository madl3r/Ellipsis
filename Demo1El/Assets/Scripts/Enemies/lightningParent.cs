using UnityEngine;
using System.Collections;

public class lightningParent : BaseEnemy {

	// Use this for initialization
	void Start () {
		transform.position = new Vector2(10, 0);
		rigidbody2D.velocity = new Vector2 (-5.0f, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected virtual void OffCameraLeft()
	{
		//Should maybe also choose a new line to go onto.
		transform.position = new Vector2 (11.0f, transform.position.y);
		Debug.Log("Went off the end");
//		Destroy(gameObject);
	}

	public void lightningDie()
	{
		Debug.Log("LIGHTNING DIEING!");
		//Tell the world that you died.
		dahWorld.GetComponent<World>().enemyKilled();
		Destroy(gameObject);
	}

	protected override void OnTriggerEnter2D(Collider2D other)
	{
		//Do nothing... I guess parents get hit too when their 
		;
	}

}
