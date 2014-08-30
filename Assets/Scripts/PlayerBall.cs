using UnityEngine;
using System.Collections;

public class PlayerBall : MonoBehaviour 
{
	public float fGrowRate;
	public float fMaxSize;

	public bool bGrowing;
	
	// Use this for initialization
	void Start () 
	{
		fGrowRate 	= 0.01f;
		fMaxSize	= 5.0f;

		bGrowing	= false;

		StartCoroutine( "StartGrowing" );
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( Input.GetKeyDown( KeyCode.Return ) )
			StartCoroutine( "StartGrowing" );
	}
	
	public IEnumerator StartGrowing()
	{
		if( bGrowing == false )
		{
			bGrowing = true;

			while( transform.localScale.x < fMaxSize )
			{
				transform.localScale += new Vector3( fGrowRate, fGrowRate, fGrowRate );
				yield return null;
			}
		}
	}

	void OnTriggerEnter( Collider other )
	{
		if( other.gameObject.tag == "Ball" )
		{
			other.gameObject.GetComponent<Ball>().StartCoroutine( "StartGrowing" );
		}
	}
}
