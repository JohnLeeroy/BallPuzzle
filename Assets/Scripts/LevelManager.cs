using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour 
{
	public static LevelManager Instance;
	public int levelNum    = 0;
	public int ballsCount  = 5;
	public List<Material>  	backgroundMaterials;
	public GameObject		background;
	public List<Material>   playbarMaterials;
	public GameObject		playbar;
	public int playerLives = 3;

	List<Transform> bubbles;

	public int ballsAlive = 0;
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
		int tempInt = Random.Range(0,4);
		background.renderer.material = backgroundMaterials[tempInt];
		playbar.renderer.material = playbarMaterials[tempInt];
	}

	void Start () 
	{
		factory = Factory.getInstance ();
		GameObject newBubble;
		ballsAlive = ballsCount;

		for (int i = 0; i < ballsCount; i++) {
			newBubble =factory.createBubble(); 
			newBubble.name = "Bubble";
			newBubble.transform.parent = transform;
			bubbles.Add(newBubble.transform);
			//TODO Randomly position bubbles
		}

	}

}
