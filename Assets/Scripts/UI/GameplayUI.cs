using UnityEngine;
using System.Collections;

public class GameplayUI : MonoBehaviour {
	
	public GUIText gtScore;
	public GUIText gtLives;
	public GUIText gtLevel;
	
	//public PauseModal pauseModal;
	// Use this for initialization
	void Start () {
		NotificationCenter.DefaultCenter.AddObserver (this, "UpdatedScore");
		NotificationCenter.DefaultCenter.AddObserver (this, "UpdatedLevel");
		NotificationCenter.DefaultCenter.AddObserver (this, "OnSpawnPlayerBubble");

		gtScore.fontSize = Mathf.Min(Screen.height,Screen.width)/20;
		gtLives.fontSize = Mathf.Min(Screen.height,Screen.width)/20;
		gtLevel.fontSize = Mathf.Min(Screen.height,Screen.width)/20;
	}

	void UpdatedScore(NotificationCenter.Notification notif)
	{
		gtScore.text = notif.data ["score"].ToString(); 
	}

	void OnUpdatedLevel()
	{
		gtLevel.text = GameManager.Level.ToString();
	}
	
	void OnSpawnPlayerBubble()
	{
		gtLives.text = GameObject.Find("LevelManager").GetComponent<LevelManager>().LivesLeft.ToString();
	}
}

