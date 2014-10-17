using UnityEngine;
using System.Collections;

public class yellowBounceScript : greenBounceScript {

	// Use this for initialization
	void Start () {
		dmg = 1 + bnsDmg;
		duration = 0.2f + bnsDuration;
		startTime = Time.time;
		transform.position = new Vector2 (transform.position.x + 1, transform.position.y);
	}

}