using UnityEngine;
using System.Collections;

public class attackTypeScript : BaseBulletScript {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	

	protected void setDamageBonus (int bonusDmg)
	{
		dmg += bonusDmg;
	}
}
