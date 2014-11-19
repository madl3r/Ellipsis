using UnityEngine;
using System.Collections;

public class BaseUpgrade : MonoBehaviour {

	//the line that this upgrade is on, and then the sprite for it.
	public GameObject upgradeLine;
	public Sprite upgradeSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void giveUpgradeToPlayer (GameObject player, bool shopCalled)
	{

	}
}
