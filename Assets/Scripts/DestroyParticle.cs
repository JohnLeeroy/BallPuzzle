using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		StartCoroutine("WaitToDestroy");
	}
	
	IEnumerator WaitToDestroy()
	{
		yield return new WaitForSeconds(1.5f);
		Destroy(this.gameObject);
	}
}
