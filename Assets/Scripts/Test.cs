using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
	void Update()
	{
		if( Input.GetKeyDown( KeyCode.Return ) )
			File.SaveScore( "TestPlayer", 54321 );
	}
}
