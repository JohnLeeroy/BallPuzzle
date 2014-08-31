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

