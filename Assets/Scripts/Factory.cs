using UnityEngine;
using System.Collections;

public class Factory : MonoBehaviour {

	public GameObject bubblePrefab;
	public GameObject playerBubblePrefab;

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

	public GameObject createBubble()
	{
		return (GameObject)Instantiate(bubblePrefab);
	}

	public GameObject createPlayerBubble()
	{
		return (GameObject)Instantiate(playerBubblePrefab);
	}
	

}
