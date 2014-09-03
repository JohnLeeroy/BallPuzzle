using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour 
{
	public int levelNum    = 0;
	public int ballsCount  = 5;
	public int randomizedNum = 0;

	public List<Material>  	backgroundMaterials;
	public Renderer[]		backgrounds;
	public List<Texture2D>   playbarTextures;
	public GUITexture		playbar;

	public int playerStartLives = 3;
	private int playerLives = 3;
	public int ballsAlive = 0;
	private static LevelManager instance;
	private Factory	factory;
	List<Transform> bubbles;


	public int LivesLeft
	{
		get{return playerLives;}
		set{playerLives = value;}
	}
	public static LevelManager getInstance()
	{
		return instance;	
	}
	
	void Awake()
	{
		if(instance != null)
			Destroy(gameObject);
		
		instance = this;

		bubbles = new List<Transform> (ballsCount);

		randomizedNum = Random.Range(0,4);
		foreach (Renderer rend in backgrounds) {
			rend.material = backgroundMaterials[randomizedNum];
		}
		playbar.texture = playbarTextures [randomizedNum];
	}

	void Start () 
	{
		factory = Factory.getInstance ();
		GameObject newBubble;
		
		if (Analytics.isTuningEnabled) {
			ballsCount = Splyt.Tuning.getVar("Stage_Start_Bubbles", 15);
			playerStartLives = Splyt.Tuning.getVar("Stage_Start_Lives", 5);
		}
	
		playerLives = playerStartLives;

		ballsAlive = ballsCount;

		for (int i = 0; i < ballsCount; i++) {
			newBubble = factory.createBubble(randomizedNum); 
			newBubble.transform.name = "Bubble";
			newBubble.transform.parent = transform;
			float tempX = Random.Range(-7.2f, 7.2f);
			float tempY = Random.Range(-4.2f,4f);
			newBubble.transform.position = new Vector3(tempX, tempY, 0);
			bubbles.Add(newBubble.transform);
		}
	}

}
