using UnityEngine;
using System.Collections.Generic;

public class Factory : MonoBehaviour {

	//public GameObject bubblePrefab;
	//public GameObject playerBubblePrefab;
	public List<GameObject> bubblePrefab;
	public List<GameObject> playerBubblePrefab;
	private static Factory instance;
	private int ballColor = 0;
	
	public static Factory getInstance()
	{
		return instance;	
	}
	
	void Awake()
	{
		if(instance != null)
			Destroy(gameObject);
		
		instance = this;
	}
	void Start()
	{
		ballColor = LevelManager.getInstance().randomizedNum;
		Debug.Log(ballColor);
	}

	public GameObject createBubble()
	{
		return (GameObject)Instantiate(bubblePrefab[ballColor]);
	}

	public GameObject createPlayerBubble()
	{
		return (GameObject)Instantiate(playerBubblePrefab[ballColor]);
	}
	

}
