using UnityEngine;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

	public GUITexture startButton;
	public GUITexture creditsButton;
	public GUITexture leaderboardButton;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector2 mousePos = Input.mousePosition;
			if( startButton.GetScreenRect().Contains(mousePos))
			{
				Debug.Log("Start Button");
				Application.LoadLevel("Arcade");
			}
			else if(creditsButton.GetScreenRect().Contains(mousePos))
			{
				Application.LoadLevel("Credits");
				Debug.Log("Credits Button");
				
			}
			else if(leaderboardButton.GetScreenRect().Contains(mousePos))
			{
				Application.LoadLevel("Highscores");
				Debug.Log("Leaderboard Button");
			}
		}
	}
}
