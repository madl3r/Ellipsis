using UnityEngine;
using System.Collections;

public class attackTypeScript : MonoBehaviour
{
	//To be used for setting the default attack speed of the player that is currently using this attackType
	protected float defaultAttackSpeed;
	protected float knockBackPos;
	protected float knockBackSpeed;
	public GameObject bullet;
	public GameObject myPlayer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void setBaseAttackSpeed(GameObject attacker)
	{
		attacker.GetComponent<playerStats>().setBaseAttackSpd(defaultAttackSpeed);
	}

	public void givePlayerBullet(GameObject player)
	{
		player.GetComponent<playerStats>().setBullet(bullet);
	}
}
