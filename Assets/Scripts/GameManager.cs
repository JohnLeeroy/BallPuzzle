using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum GameStates
{
	INTRO,
	ALIVE,
	PLAYING,
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

	// Use this for initialization
	void Start () 
	{
		levelMang = GameObject.Find("LevelManager").GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		CatchState();

		if(levelMang.LivesLeft > 0)
		{
			StateHandle = GameStates.RESTARTING;
		}
		else
		{
			StateHandle = GameStates.DEAD;
		}
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


}
