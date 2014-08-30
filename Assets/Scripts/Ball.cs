using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public float fGrowRate;
	public float fMaxSize;

	public bool bGrowing;

	// Use this for initialization
	public virtual void Start () 
	{
		fGrowRate 	= 0.01f;
		fMaxSize	= 5.0f;

		bGrowing	= false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public virtual IEnumerator StartGrowing()
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

	public virtual void OnTriggerEnter( Collider other )
	{
		if( other.gameObject.tag == "Ball" )
		{
			other.gameObject.GetComponent<Ball>().StartCoroutine( "StartGrowing" );
		}
	}
}
