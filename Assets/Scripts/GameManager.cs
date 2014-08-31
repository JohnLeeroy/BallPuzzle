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
	private GameStates prevState;

	// Use this for initialization
	void Start () 
	{
		levelMang = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		NotificationCenter.DefaultCenter.AddObserver (this, "Pause");
		NotificationCenter.DefaultCenter.AddObserver (this, "Resume");

		currentState = GameStates.PLAYING;
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
		if (currentState == GameStates.PLAYING && InputController.instance.isTouched) 
		{
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


}
