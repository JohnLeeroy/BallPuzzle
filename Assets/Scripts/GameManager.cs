using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameStates
{
	INTRO,
	ALIVE,
	PLAYING,
	PAUSED,
	DEAD,
	RESTARTING
};

public class GameManager : MonoBehaviour 
{
	private GameStates 		currentState = GameStates.INTRO;
	private LevelManager 	levelMang;

	public GameStates  		StateHandle
	{
		get{return currentState;}
		set{currentState = value;}
	}

	ScoreSystem scoreSystem;

	private static GameManager instance;
	public static GameManager getInstance()
	{
		return instance;
	}

	private GameStates prevState;
	private float lastBubblePopTime; 

	bool isWin = false;
	public static bool isGameOver = false;

	static int currentLevel = 1;
	public int Level { get { return currentLevel; } }

	int score;
	public int Score { get { return score; } set {score = value;}}

	//Analytics
	GameSession gameSession;
	static int winCount = 0; 	//tracks wins per session
	static int loseCount = 0; 	//tracks loses per session

	void Awake()
	{
		if (instance != null) {
			Destroy (gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad (gameObject);

		gameSession = new GameSession ();
	}

	void Start()
	{
		scoreSystem = GameObject.Find ("ScoreSystem").GetComponent<ScoreSystem> ();
		levelMang = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}

	void init()
	{
		scoreSystem = GameObject.Find ("ScoreSystem").GetComponent<ScoreSystem> ();
		levelMang = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		NotificationCenter.DefaultCenter.AddObserver (this, "Pause");
		NotificationCenter.DefaultCenter.AddObserver (this, "Resume");
		NotificationCenter.DefaultCenter.AddObserver (this, "GameOver");
		NotificationCenter.DefaultCenter.AddObserver (this, "OnBubblePop");
		currentState = GameStates.PLAYING;
		
		isGameOver = false;
		isWin = false;
		
		AudioManager.getInstance().Play(0);
		
		gameSession.start ();
	}

	void Update () 
	{
		HandleInput ();
	}

	void HandleInput()
	{
		if (InputController.instance.isTouched && levelMang.LivesLeft > 0) 
		{
			AudioManager.getInstance().Play(4);
			levelMang.LivesLeft--;
			GameObject bubble = Factory.getInstance().createPlayerBubble(levelMang.randomizedNum);
			//bubble.transform.position = Camera.main.
			Ray ray;
			RaycastHit hit;
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100))
			{
				bubble.transform.position = new Vector3(hit.point.x, hit.point.y, 0);
				NotificationCenter.DefaultCenter.PostNotification(this, "OnSpawnPlayerBubble");

				lastBubblePopTime = Time.time; 		//reset the pop timer when the player spawns a bubble

				if(levelMang.LivesLeft == 0)
					StartCoroutine ("CR_OnLastPlayerBubbleSpawn");
			}
		}
	}

	void GameOver()
	{
		StopAllCoroutines ();
		Leaderboard leaderboard = Leaderboard.Instance;

		int triesLeft = levelMang.LivesLeft;
		scoreSystem.applyLifeLeftoverBonus (triesLeft);

		score += scoreSystem.score;
		if (isWin) {
			Debug.Log ("VICTORY " + score);
			AudioManager.getInstance().Play(0);
			NotificationCenter.DefaultCenter.PostNotification(this, "ShowWinMenu");
			winCount++;
		}
		else
		{
			Debug.Log ("GAME OVER " + score);
			AudioManager.getInstance().Play(5);

			//Debug.Log("Total Score " + score);
			int rank = leaderboard.getScoreRank (score);

			Hashtable table = new Hashtable ();
			table ["rank"] = rank;

			Debug.Log ("Rank " + rank);
			leaderboard.postScore (score);
			
			if(rank != -1)
				NotificationCenter.DefaultCenter.PostNotification(this, "ShowLeaderboard", table);
			else
				NotificationCenter.DefaultCenter.PostNotification(this, "ShowLoseMenu");
			loseCount++;
		}
		NotificationCenter.DefaultCenter.PostNotification (this, "Pause");
		isGameOver = true;

		Dictionary<string, object> data = new Dictionary<string, object> ();
		data["Result"] = ((isWin) ? 1 : 0);
		data["Round"] = currentLevel;
		data["Score"] = score;
		data["Tries"] = levelMang.playerStartLives - levelMang.LivesLeft;

		data["BubbleCount"] = levelMang.ballsCount;
		data ["LifeCount"] = levelMang.playerStartLives;

		gameSession.end (ref data);
	}

	IEnumerator OnBubblePop(NotificationCenter.Notification notif)
	{
		yield return 0;
		levelMang.ballsAlive--;
		//Debug.Log ("BALLS LEFT " + levelMang.ballsAlive);
		if (levelMang.ballsAlive <= 0) {
			Debug.Log ("No more Balls, we win");
			isWin = true;
			GameOver();
		}
		lastBubblePopTime = Time.time;
	}
		
	//Checks for game end if no bubbles are popped for bubble lifetime
	IEnumerator CR_OnLastPlayerBubbleSpawn()
	{
		while (Time.time - lastBubblePopTime < 2) {
			yield return 0;
		}

		if (levelMang.ballsAlive == 0) 
			isWin = true;

		GameOver();
	}

	void OnApplicationQuit()
	{
		isGameOver = true;
		Splyt.Transaction globalData = Splyt.Instrumentation.Transaction("SessionGlobal");
		globalData.setProperty ("WinCount", winCount);
		globalData.setProperty ("LoseCount", loseCount);
		globalData.setProperty ("HighestCombo", ScoreSystem.highestCombo);

		globalData.beginAndEnd ();
	}

	void OnLevelWasLoaded()
	{
		if(GetInstanceID() != instance.GetInstanceID()){
			Destroy(gameObject);
			return;
		}

		StopAllCoroutines ();
		Time.timeScale = 1;
		
		if (Application.loadedLevelName != "Arcade")
			return;

		if (isWin)
		{
			currentLevel++;
			GameObject.Find ("ScoreSystem").GetComponent<ScoreSystem> ().setScore(score);
			score = 0;
		}
		else
		{
			currentLevel = 1;
			score = 0;
		}

		Debug.Log ("Score at the end of round " + score);
		
		init ();
	}

	const int baseBubbleCount = 5;
	const int baseTryCount = 1;

	void setNextLevelDifficulty()
	{
		//levelMang.ballsCount = baseBubbleCount + Formula(currentLevel);
		//Debug.Log("Ball Start Count " + levelMang.ballsCount);
	}
	
	void Pause()
	{
		prevState = currentState;
		currentState = GameStates.PAUSED;
		Time.timeScale = 0;
	}
	
	void Resume()
	{
		currentState = prevState;
		Debug.Log (currentState.ToString ());
		Time.timeScale = 1;
	}
}
