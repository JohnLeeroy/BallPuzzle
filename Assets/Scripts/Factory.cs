using UnityEngine;
using System.Collections;

public class Factory : MonoBehaviour {

	public GameObject bubblePrefab;

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

	// Use this for initialization
	void Start () {
	
	}
	

}
