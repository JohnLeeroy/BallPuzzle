using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour 
{
	private float fScrollSpeed = 2.0f;

	void Start()
	{
		StartCoroutine( RollCredits() );
	}

	IEnumerator RollCredits()
	{
		while(true)
		{
			while( transform.position.y < 20.0f )
			{
				transform.position += new Vector3( 0.0f, fScrollSpeed * Time.deltaTime, 0.0f );
				yield return null;
			}

			transform.position = new Vector3(0, -17, 0);
		}
	}
}
