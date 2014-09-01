using UnityEngine;
using System.Collections.Generic;

public class Factory : MonoBehaviour {


	public List<GameObject> bubblePrefab;
	public List<GameObject> playerBubblePrefab;
	private static Factory instance;

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

	public GameObject createBubble(int num)
	{
		return (GameObject)Instantiate(bubblePrefab[num]);
	}

	public GameObject createPlayerBubble(int num)
	{

		return (GameObject)Instantiate(playerBubblePrefab[num]);
	}
}
