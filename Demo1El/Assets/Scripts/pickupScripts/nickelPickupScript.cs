using UnityEngine;
using System.Collections;

public class nickelPickupScript : basePickupScript {
	
	// Use this for initialization
	void Start () {
		pickupSpeed = -7.0f;
		rigidbody2D.velocity = new Vector2 (pickupSpeed, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OffCameraLeft()
	{
		transform.position = new Vector2 (10.0f, transform.position.y);
	}
	
	public override void giveUpgradeToPlayer (GameObject player)
	{
		player.GetComponent<playerStats>().addCoins(5);
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