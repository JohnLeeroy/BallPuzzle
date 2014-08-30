using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour 
{
	public static LevelManager Instance;
	public int levelNum = 0;
	public int ballsCount = 5;
	public int playerLives = 3;

	List<Transform> bubbles;

	private int ballsAlive = 0;
	private Factory	factory;


	public int LivesLeft
	{
		get{return playerLives;}
		set{playerLives = value;}
	}
	// Use this for initialization
	void Awake()
	{
		Instance = this;
		bubbles = new List<Transform> (ballsCount);
	}
	void Start () 
	{
		factory = Factory.getInstance ();
		GameObject newBubble;
		for (int i = 0; i < ballsCount; i++) {
			newBubble =factory.createBubble(); 
			newBubble.name = "Bubble";
			newBubble.transform.parent = transform;
			bubbles.Add(newBubble.transform);
			//TODO Randomly position bubbles

		}
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
