using UnityEngine;
using System.Collections;

public class GameplayUI : MonoBehaviour {
	
	public GUIText gtScore;
	public GUIText gtLives;
	public GUIText gtLevel;
	public GUIText gtScoreText;

	TextMesh editScoreText;
	
	public GameObject winMenu;
	public GameObject loseMenu;
	public GameObject leaderboardMenu;

	private bool restarting = false;
	//public PauseModal pauseModal;
	// Use this for initialization
	int fontSize = 24;
	LevelManager levelManager;

	bool isEndGame = false;

	void Start () {
		NotificationCenter.DefaultCenter.AddObserver (this, "UpdatedScore");
		NotificationCenter.DefaultCenter.AddObserver (this, "UpdatedLevel");
		NotificationCenter.DefaultCenter.AddObserver (this, "OnSpawnPlayerBubble");

		NotificationCenter.DefaultCenter.AddObserver (this, "ShowWinMenu");
		NotificationCenter.DefaultCenter.AddObserver (this, "ShowLoseMenu");
		NotificationCenter.DefaultCenter.AddObserver (this, "ShowLeaderboard");

		gtScore.fontSize = Mathf.Min(Screen.height,Screen.width)/fontSize;
		gtScoreText.fontSize = Mathf.Min(Screen.height,Screen.width)/fontSize;
		gtLives.fontSize = Mathf.Min(Screen.height,Screen.width)/fontSize;
		gtLevel.fontSize = Mathf.Min(Screen.height,Screen.width)/fontSize;
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();

		gtLives.text = "Lives " + levelManager.LivesLeft;
		gtLevel.text = "Level " + GameManager.getInstance ().Level;
		gtScore.text =  GameManager.getInstance ().Score.ToString();
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


#if !UNITY_WEBPLAYER
	void ShowLeaderboard(NotificationCenter.Notification notif)
	{
		leaderboardMenu.SetActive (true);
		isEndGame = true;

		int rank = (int)notif.data ["rank"];
		editScoreText = leaderboardMenu.GetComponent<LeaderboardModal> ().rows [rank].name;
		StartCoroutine (HandleLeaderboard (rank));
	}
#endif

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


	IEnumerator HandleLeaderboard(int rank)
	{
#if UNITY_EDITOR ||UNITY_EDITOR_OSX
		/*while (editScoreText.text.Length < 3) {
			if(Input.anyKeyDown)
				editScoreText.text = Input.inputString;
			yield return 0;
		}*/
		yield return 0;
		editScoreText.text = Random.Range(100, 999).ToString();
#elif !UNITY_WEBPLAYER
		TouchScreenKeyboard keyboard = TouchScreenKeyboard.Open ("");
		while (keyboard.active) {
			if(keyboard.text.Length > 3)
			{
				keyboard.text = keyboard.text.Substring(0, 3);
			}
			editScoreText.text = keyboard.text;
			yield return 0;
		}
		editScoreText.text = keyboard.text;
		
		Leaderboard.Instance.records [rank].name = editScoreText.text;
		//Debug.Log ("Rank " + rank + "  |   " + name);
		FileScript.SaveLeaderboard ();
#endif
		
	}

	void Update()
	{
		if (!isEndGame)
			return;

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				Debug.Log (hit.transform.name);
				if(hit.transform.name == "HomeButton")
					Application.LoadLevel(0);
				else if(hit.transform.name == "RetryButton")
				{
					//GameManager.getInstance().Score = 0;
				    restarting = true;
				}
				else if(hit.transform.name == "NextButton")
				{
					restarting = true;
				}
			}
		}
		
		if(restarting && Input.touchCount == 0)
		{
			restarting = false;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

}

