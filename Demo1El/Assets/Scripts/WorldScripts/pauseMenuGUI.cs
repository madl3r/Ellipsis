using UnityEngine;
using System.Collections;

public class pauseMenuGUI : MonoBehaviour {
	
	public GUISkin menuSkin;
	public int guiDepth = 0;
	public Rect menuArea;
	public Rect resumeButton;
	public Rect optionsButton;
	public Rect quitButton;
	public Rect soundFXButton;
	public Rect musicButton;
	public Rect backButton;
	Rect menuAreaNormalized;
	public static bool isPaused = false;
	public static bool gameOver = false;
	private bool optionsMenu;
	
	public bool networking = false;
	
	// Use this for initialization
	void Start () {
		optionsMenu = false;
		menuAreaNormalized = new Rect(menuArea.x * Screen.width * 0.5f - (menuArea.width * 0.5f),
		                              menuArea.y * Screen.height * 0.5f - (menuArea.height * 0.5f), 
		                              menuArea.width, menuArea.height);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) {
			if (gameOver) {
				restart ();
			} else {
				if(!isPaused) {
					Time.timeScale = 0.0f;
					isPaused = true;
				} else {
					Time.timeScale = 1.0f;
					isPaused = false;
				}
			}
		}
	}
	
	
	void restart() {
		Application.LoadLevel("MainMenu");
		isPaused = false;
		gameOver = false;
		//		Time.timeScale = 1.0f;
//		ClockGUI.resetStartTime();
	}
	
	void OnGUI() {
		GUI.skin = menuSkin;
		GUI.depth = guiDepth;
		if(isPaused && !optionsMenu)
		{
			GUI.BeginGroup(menuAreaNormalized);
			if (GUI.Button(new Rect(resumeButton), "Resume"))
			{
				Time.timeScale = 1.0f;
				isPaused = false;
			}
			if (GUI.Button(new Rect(optionsButton), "Options"))
			{
				optionsMenu = true;
			}
			if(GUI.Button(new Rect(quitButton), "Quit"))
			{
				Application.Quit();
				Debug.Log("Quit!");
			}
			GUI.EndGroup();
		}
		else if (gameOver && !optionsMenu)
		{
			GUI.BeginGroup(menuAreaNormalized);
			if (GUI.Button(new Rect(resumeButton), "Restart"))
			{	
				restart ();
			}
			if (GUI.Button(new Rect(optionsButton), "Options"))
			{
				optionsMenu = true;
			}
			if(GUI.Button(new Rect(quitButton), "Quit"))
			{
				Application.Quit();
				Debug.Log("Quit!");
			}
			GUI.EndGroup();
		}
		else if (optionsMenu)
		{
			GUI.BeginGroup(menuAreaNormalized);
			
			if (GUI.Button(new Rect(soundFXButton), "Sound Fx"))
			{
				//TODO
			}
			if (GUI.Button(new Rect(musicButton), "Music"))
			{
				//TODO
			}
			if(GUI.Button(new Rect(backButton), "Back"))
			{
				optionsMenu = false;
			}
			GUI.EndGroup();
			
		}
		
	}
}