using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ParticleManager : MonoBehaviour
{
	public List<GameObject> particleEffects;
	private LevelManager	levelMang;
	// Use this for initialization
	void Start () 
	{
		levelMang = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		NotificationCenter.DefaultCenter.AddObserver (this, "OnBubblePop");
	}
	void OnBubblePop(NotificationCenter.Notification notify)
	{
		GameObject newExplosion = (GameObject) Instantiate(particleEffects[levelMang.randomizedNum]);
		newExplosion.transform.position = notify.sender.transform.position;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
