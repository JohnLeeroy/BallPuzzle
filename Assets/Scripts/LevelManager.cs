using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour 
{
	public int levelNum    = 0;
	public int ballsCount  = 5;
	public int randomizedNum = 0;

	public List<Material>  	backgroundMaterials;
	public GameObject		background;
	public List<Texture2D>   playbarTextures;
	public GUITexture		playbar;
	public int playerLives = 3;
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
		background.renderer.material = backgroundMaterials[randomizedNum];
		playbar.texture = playbarTextures [randomizedNum];
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
			float tempX = Random.Range(-7.2f, 7.2f);
			float tempY = Random.Range(-4.2f,4f);
			newBubble.transform.position = new Vector3(tempX, tempY, 0);
			bubbles.Add(newBubble.transform);

		}

	}

}
