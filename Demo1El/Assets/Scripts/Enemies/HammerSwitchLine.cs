using UnityEngine;
using System.Collections;

public class HammerSwitchLine : HammerEnemy {


	//Same exact as hammer... but chooses a new line whenver it goes off of the screen.
	protected override void OffCameraLeft()
	{
		//Should maybe also choose a new line to go onto.
		transform.position = new Vector2 (11.0f, Random.Range(-2, 3)*2);
	}

}
