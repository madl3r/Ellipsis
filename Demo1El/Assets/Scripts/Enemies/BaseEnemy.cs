using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour {

	protected int hp;
	protected int dmg;
	protected bool worldSpawned = false;
	protected GameObject dahWorld;

	protected Animator anim;

	// Use this for initialization
	protected virtual void Start () {

		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	protected virtual void OnTriggerEnter2D(Collider2D other)
//	{
//		if (other.gameObject.tag == "bullet")
//		{
//			other.gameObject.SendMessage("dealDamage", gameObject);
//		}
//	}

	protected virtual void attack()
	{

	}
	
	protected virtual void takeDamage(int dmg)
	{
		anim.SetTrigger("TakeDamage");
		hp -= dmg;
		
		if (hp <= 0)
		{
			//Tell the world that you died.
			dahWorld.GetComponent<World>().enemyKilled();
			Destroy(gameObject);
		}
	}


	//by default do nothing when leaving the screen
	protected virtual void OffCameraRight()
	{
		;
	}
	
	protected virtual void OffCameraLeft()
	{
		;
	}

	public void isWorldSpawned(bool b)
	{
		worldSpawned = b;
	}

	public void setTheWorld(GameObject w)
	{
		dahWorld = w;
	}
}
