using UnityEngine;
using System.Collections;

public class displayHP : MonoBehaviour {

	public GameObject heartSprite;
	public GameObject[] hearts;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void showHearts(int hpAmt)
	{
		//Kill all the hearts that are there
		for (int i = 0; i < hearts.Length; i++)
		{
			if (hearts[i] != null)
				Destroy(hearts[i]);
		}

		//Pu the new hearts in
		if (hpAmt > 0){
			hearts = new GameObject[hpAmt];

			for (int i = 0; i < hearts.Length; i++)
			{
				hearts[i] = Instantiate(heartSprite, new Vector2(transform.position.x + i, transform.position.y), transform.rotation) as GameObject;
			}
		}
	}
}
