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
		NotificationCenter.DefaultCenter.AddObserver (this, "OnPlayerBubblePop");
	}
	void OnBubblePop(NotificationCenter.Notification notify)
	{
		GameObject newExplosion = (GameObject) Instantiate(particleEffects[levelMang.randomizedNum]);
		newExplosion.transform.position = notify.sender.transform.position;
		AudioManager.getInstance().Play(3);
	}
	void OnPlayerBubblePop(NotificationCenter.Notification notify)
	{
		GameObject newExplosion = (GameObject) Instantiate(particleEffects[levelMang.randomizedNum]);
		newExplosion.transform.position = notify.sender.transform.position;
		AudioManager.getInstance().Play(3);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
