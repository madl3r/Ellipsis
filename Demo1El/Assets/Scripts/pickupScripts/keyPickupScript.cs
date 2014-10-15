using UnityEngine;
using System.Collections;

public class keyPickupScript : basePickupScript {
	
	// Use this for initialization
	void Start () {
		pickupSpeed = -6.0f;
		rigidbody2D.velocity = new Vector2 (pickupSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OffCameraLeft()
	{
		//Should maybe also choose a new line to go onto.
		transform.position = new Vector2 (10.0f, transform.position.y);
	}
	
	public override void giveUpgradeToPlayer (GameObject player)
	{
		player.GetComponent<playerStats>().addKey(1);
		Destroy(gameObject);
	}
	
	public override void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			giveUpgradeToPlayer (other.gameObject);
		}
	}
}