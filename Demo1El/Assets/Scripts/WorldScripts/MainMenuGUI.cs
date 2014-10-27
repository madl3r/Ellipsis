using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {

	//TODO is there a way to rotate this?

	public GUISkin menuSkin;
	public int guiDepth = 0;
	public Rect menuArea;
	public Rect playButton;
	public Rect optionsButton;
	public Rect quitButton;
	public Rect soundFXButton;
	public Rect musicButton;
	public Rect backButton;
	Rect menuAreaNormalized;
	private bool optionsMenu;
	
	public bool networking = false;
	
	// Use this for initialization
	void Start () {
		Time.timeScale = 0.0f;
		optionsMenu = false;
		menuAreaNormalized = new Rect(menuArea.x * Screen.width * 0.5f - (menuArea.width * 0.5f),
		                              menuArea.y * Screen.height * 0.5f - (menuArea.height * 0.5f), 
		                              menuArea.width, menuArea.height);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI() {
		GUI.skin = menuSkin;
		GUI.depth = guiDepth;
		if(!optionsMenu) {
			GUI.BeginGroup(menuAreaNormalized);
			if (GUI.Button(new Rect(playButton), "Play")) {
				Time.timeScale = 1.0f;
				Application.LoadLevel("DemoScene1");
			} if (GUI.Button(new Rect(optionsButton), "Options")) {
				optionsMenu = true;
			} if(GUI.Button(new Rect(quitButton), "Quit")) {
				Application.Quit();
				Debug.Log("Quit!");
			}
			GUI.EndGroup();
		} else if (optionsMenu) {
			GUI.BeginGroup(menuAreaNormalized);
			
			if (GUI.Button(new Rect(soundFXButton), "Sound Fx")) {
				//TODO
			} if (GUI.Button(new Rect(musicButton), "Music")) {
				//TODO
			} if(GUI.Button(new Rect(backButton), "Back")) {
				optionsMenu = false;
			}
			GUI.EndGroup();
		}
		
	}
	
}
