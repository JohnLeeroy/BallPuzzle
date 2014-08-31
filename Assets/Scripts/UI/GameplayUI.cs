using UnityEngine;
using System.Collections;

public class GameplayUI : MonoBehaviour {
	
	public GUIText gtScore;
	public GUIText gtLives;
	public GUIText gtLevel;
	
	//public PauseModal pauseModal;
	// Use this for initialization
	int fontSize = 24;
	LevelManager levelManager;
	void Start () {
		NotificationCenter.DefaultCenter.AddObserver (this, "UpdatedScore");
		NotificationCenter.DefaultCenter.AddObserver (this, "UpdatedLevel");
		NotificationCenter.DefaultCenter.AddObserver (this, "OnSpawnPlayerBubble");

		gtScore.fontSize = Mathf.Min(Screen.height,Screen.width)/fontSize;
		gtLives.fontSize = Mathf.Min(Screen.height,Screen.width)/fontSize;
		gtLevel.fontSize = Mathf.Min(Screen.height,Screen.width)/fontSize;
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		//GameManager.

		gtLives.text = "Lives " + levelManager.LivesLeft;
	}

	void UpdatedScore(NotificationCenter.Notification notif)
	{
		gtScore.text = notif.data ["score"].ToString(); 
	}

	void OnUpdatedLevel()
	{
		gtLevel.text = "Level " + GameManager.Level.ToString();
	}
	
	void OnSpawnPlayerBubble()
	{
		gtLives.text = "Lives " + levelManager.LivesLeft;
	}
}

