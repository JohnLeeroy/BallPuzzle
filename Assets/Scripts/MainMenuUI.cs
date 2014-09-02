using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

	public GUITexture startButton;
	public GUITexture creditsButton;
	public GUITexture leaderboardButton;
	
	private bool starting = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector2 mousePos = Input.mousePosition;
			if( startButton.GetScreenRect().Contains(mousePos))
			{
				AudioManager.getInstance().Play(1);
				starting = true;
				Debug.Log("Start Button");
				
			}
			else if(creditsButton.GetScreenRect().Contains(mousePos))
			{
				AudioManager.getInstance().Play(1);
				Application.LoadLevel("Credits");
				Debug.Log("Credits Button");
				
			}
			else if(leaderboardButton.GetScreenRect().Contains(mousePos))
			{
				AudioManager.getInstance().Play(1);
				Application.LoadLevel("Highscores");
				Debug.Log("Leaderboard Button");
			}
		}
		
		if(starting && Input.touchCount == 0)
		{
			Application.LoadLevel("Arcade");
		}
	}
}
