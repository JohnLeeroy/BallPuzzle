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

	static private GameManager instance;
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
	public int Score { get { return score; } }

	void Awake()
	{
		if (instance != null) {
			Destroy (gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad (gameObject);
	}
	// Use this for initialization
	void Start () 
	{
		levelMang = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		NotificationCenter.DefaultCenter.AddObserver (this, "Pause");
		NotificationCenter.DefaultCenter.AddObserver (this, "Resume");
		NotificationCenter.DefaultCenter.AddObserver (this, "GameOver");
		NotificationCenter.DefaultCenter.AddObserver (this, "OnBubblePop");
		currentState = GameStates.PLAYING;
		AudioManager.getInstance().Play(0);
		isGameOver = false;
		isWin = false;
	}

	// Update is called once per frame
	void Update () 
	{
		CatchState();
		HandleInput ();

		/*
		 * if(levelMang.LivesLeft > 0)
		{
			StateHandle = GameStates.RESTARTING;
		}
		else
		{
			StateHandle = GameStates.DEAD;
		}
		*/
	}

	void CatchState()
	{
		switch(currentState)
		{
		case GameStates.INTRO:{break;}
		case GameStates.ALIVE:{break;}
		case GameStates.PLAYING:{break;}
		case GameStates.DEAD:{break;}
		case GameStates.RESTARTING:{break;}
		}
	}

	void HandleInput()
	{
		if (InputController.instance.isTouched && levelMang.playerLives > 0) 
		{
			AudioManager.getInstance().Play(4);
			levelMang.LivesLeft--;
			GameObject bubble = Factory.getInstance().createPlayerBubble();
			//bubble.transform.position = Camera.main.
			Ray ray;
			RaycastHit hit;
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100))
			{
				bubble.transform.position = new Vector3(hit.point.x, hit.point.y, 0);
				NotificationCenter.DefaultCenter.PostNotification(this, "OnSpawnPlayerBubble");

				lastBubblePopTime = Time.time; 		//reset the pop timer when the player spawns a bubble

				if(levelMang.playerLives == 0)
					StartCoroutine ("CR_OnLastPlayerBubbleSpawn");
			}
		}
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

	void GameOver()
	{
		if (isWin) {
			Debug.Log ("VICTORY");
			score = GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>().score;
		}
		else
		{
			Debug.Log ("GAME OVER");
			AudioManager.getInstance().Play(5);
		}
		NotificationCenter.DefaultCenter.PostNotification (this, "Pause");
		isGameOver = true;
	}

	void OnBubblePop(NotificationCenter.Notification notif)
	{
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
	}

	void OnLevelWasLoaded()
	{
		if(GetInstanceID() != instance.GetInstanceID()){
			Destroy(gameObject);
			return;
		}
		StopAllCoroutines ();
		Time.timeScale = 1;
		
		levelMang = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		NotificationCenter.DefaultCenter.AddObserver (this, "Pause");
		NotificationCenter.DefaultCenter.AddObserver (this, "Resume");
		NotificationCenter.DefaultCenter.AddObserver (this, "GameOver");
		NotificationCenter.DefaultCenter.AddObserver (this, "OnBubblePop");
		currentState = GameStates.PLAYING;
		
		isGameOver = false;
		isWin = false;

		GameObject.Find("ScoreSystem").GetComponent<ScoreSystem>().score = score;
		currentLevel++;
		//setNextLevelDifficulty ();
	}

	const int baseBubbleCount = 5;
	const int baseTryCount = 1;

	void setNextLevelDifficulty()
	{
		
		//levelMang.ballsCount = baseBubbleCount + Formula(currentLevel);
		//Debug.Log("Ball Start Count " + levelMang.ballsCount);
	}

	int Formula(int n)
	{
		int sum = 0;
		int current = n;
		for (int i = n; n > 0; n--) {
			current = n;
			while(current > 0)
			{
				sum += n;
				current--;
			}
		}
		return sum;
	}
}
