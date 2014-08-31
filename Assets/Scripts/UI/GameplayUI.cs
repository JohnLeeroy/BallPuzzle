﻿using UnityEngine;
using System.Collections;

public class GameplayUI : MonoBehaviour {
	
	public GUIText gtScore;
	public GUIText gtLives;
	public GUIText gtLevel;

	public GameObject winMenu;
	public GameObject loseMenu;

	
	//public PauseModal pauseModal;
	// Use this for initialization
	int fontSize = 24;
	LevelManager levelManager;

	bool isEndGame = false;

	void Start () {
		NotificationCenter.DefaultCenter.AddObserver (this, "UpdatedScore");
		NotificationCenter.DefaultCenter.AddObserver (this, "UpdatedLevel");
		NotificationCenter.DefaultCenter.AddObserver (this, "OnSpawnPlayerBubble");

		//NotificationCenter.DefaultCenter.AddObserver (this, "ShowWinMenu");
		//NotificationCenter.DefaultCenter.AddObserver (this, "ShowLoseMenu");

		gtScore.fontSize = Mathf.Min(Screen.height,Screen.width)/fontSize;
		gtLives.fontSize = Mathf.Min(Screen.height,Screen.width)/fontSize;
		gtLevel.fontSize = Mathf.Min(Screen.height,Screen.width)/fontSize;
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		//GameManager.

		gtLives.text = "Lives " + levelManager.LivesLeft;
		gtLevel.text = "Level " + GameManager.getInstance ().Level;
		gtScore.text = "Score " + GameManager.getInstance ().Score;
	}

	void ShowWinMenu()
	{
		winMenu.SetActive (true);
		winMenu.gameObject.GetComponentInChildren<TextMesh> ().text = gtScore.text;
		isEndGame = true;
	}

	void ShowLoseMenu()
	{
		loseMenu.SetActive (true);
		loseMenu.gameObject.GetComponentInChildren<TextMesh>().text = gtScore.text;
		isEndGame = true;
	}

	void UpdatedScore(NotificationCenter.Notification notif)
	{
		gtScore.text = notif.data ["score"].ToString(); 
	}

	void OnUpdatedLevel()
	{
		gtLevel.text = "Level " + GameManager.getInstance().Level.ToString();
	}
	
	void OnSpawnPlayerBubble()
	{
		gtLives.text = "Lives " + levelManager.LivesLeft;
	}


	void Update()
	{
		if (!isEndGame)
			return;

		Ray ray;
		RaycastHit2D hit;
		if(Physics2D.Raycast(Input.mousePosition, Vector2.up, 100))
		{
			Debug.Log(hit.transform.name);
		}
		//Physics.Raycast
	}

}

