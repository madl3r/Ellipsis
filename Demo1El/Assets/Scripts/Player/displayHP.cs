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

	//hpAmt is how much current HP we have, and maxHP is what it sounds like
	public void showHearts(int hpAmt, int maxHP)
	{
		//Kill all the hearts that are there
		for (int i = 0; i < hearts.Length; i++)
		{
			if (hearts[i] != null)
				Destroy(hearts[i]);
		}

		//Put the new hearts in
		if (hpAmt > 0){
			//Make a list of hearts of the size of our max hp
			hearts = new GameObject[maxHP];

			//For each heart in our max HP make a heart object
			for (int i = 0; i < hearts.Length; i++)
			{
				hearts[i] = Instantiate(heartSprite, new Vector2(transform.position.x + i, transform.position.y), transform.rotation) as GameObject;
				hearts[i].transform.parent = gameObject.transform.parent;
				//color it red if it is within our current HP
				if (i < hpAmt)
				{
					hearts[i].GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);
				}
				//Color it black otherwise to show our current MAX HP
				else
				{
					hearts[i].GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
				}
			}
		}
	}
}
