using UnityEngine;
using System.Collections;

public class backgroundColorScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Color theColor = new Color(55, 255, 255);//Random.Range(0.0f, 255.0f), Random.Range(0.0f, 255.0f), 255.0f);
		camera.backgroundColor = theColor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
