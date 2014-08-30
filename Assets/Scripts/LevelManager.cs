using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public int levelNum = 0;
	public int ballsCount = 0;
	public int playerLives = 0;


	private int ballsAlive = 0;
	private Factory	gameFactory;
	// Use this for initialization
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
	}
}
