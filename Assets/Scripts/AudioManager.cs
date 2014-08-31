using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public AudioClip[] clips;

	//RENAME ME TO ACTUAL SOUNDS
	public const int LEVEL_START = 0;
	public const int BUTTON_DOWN = 1;
	public const int BOUNCE = 2;
	public const int EXPLOSION = 3;
	public const int DRIP = 4;
	public const int END_GAME = 5;
	
	private static AudioManager instance;
	
	public static AudioManager getInstance()
	{
		return instance;	
	}
	
	void Awake()
	{
		if(instance != null)
			Destroy(gameObject);
		
		instance = this;
	}
	
	public void Play(int clipIndex)
	{
		if(clipIndex < clips.Length)	
			audio.PlayOneShot(clips[clipIndex], .5f);
		
	}
}