using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public static LevelManager Instance;
	public int levelNum = 0;
	public int ballsCount = 0;
	public int playerLives = 0;


	private int ballsAlive = 0;
	private Factory	gameFactory;


	public int LivesLeft
	{
		get{return playerLives;}
		set{playerLives = value;}
	}
	// Use this for initialization
	void Awake()
	{
		Instance = this;
	}
	void Start () 
	{
		gameFactory = GameObject.Find("Factory").GetComponent<Factory>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		while(ballsAlive < ballsCount)
		{

		}

		if(playerLives == 0)
		{

		}
	}
}
