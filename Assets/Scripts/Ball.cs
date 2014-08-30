using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public float fGrowRate;												// Speed at which the ball grows
	public float fMaxSize;												// Maximum size the ball can be

	public bool bGrowing;												// Is the ball currently growing

	// Use this for initialization
	public virtual void Start () 
	{
		fGrowRate 	= 1.0f;
		fMaxSize	= 5.0f;

		bGrowing	= false;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public virtual IEnumerator StartGrowing()
	{
		// If the ball hasn't yet started growing
		if( bGrowing == false )
		{
			bGrowing = true;

			// Increase the scale of the ball
			while( transform.localScale.x < fMaxSize )
			{
				transform.localScale += new Vector3( fGrowRate, fGrowRate, fGrowRate ) * Time.deltaTime;
				yield return null;
			}

			// Clamp the ball size to the max size
			if( transform.localScale.x > fMaxSize || transform.localScale.y > fMaxSize || transform.localScale.z > fMaxSize )
				transform.localScale = new Vector3( fMaxSize, fMaxSize, fMaxSize );
		}
	}

	public virtual void OnTriggerEnter( Collider other )
	{
		// Make other ball start growing
		if( other.gameObject.tag == "Ball" )
		{
			Debug.Log( this.gameObject.name + " HIT BALL " + other.gameObject.name );
			StartCoroutine( other.gameObject.GetComponent<Ball>().StartGrowing() );
		}
	}
}
