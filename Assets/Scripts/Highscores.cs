using UnityEngine;
using System.Collections;

public class Highscores : MonoBehaviour
{
	void Start()
	{
		IList data = File.GetHighscores();
		
		string names = "";
		string scores = "";
		int i = 0;
		
		foreach(IDictionary scoreData in data)
		{
			if( i < 9 )
			{
				names += scoreData["Initials"] + "\n";
				scores += scoreData["Score"] + "\n";
			}
			else
			{
				names += scoreData["Initials"];
				scores += scoreData["Score"];
			}
			i++;
		}

		GameObject.Find( "Names" ).GetComponent<GUIText>().guiText.text = names;
		GameObject.Find( "Scores" ).GetComponent<GUIText>().guiText.text = scores;
	}
	
	void Update()
	{
		if( Input.GetKeyDown( KeyCode.Return ) )
			File.SaveScore( "JJJ", 54321 );
	}
}
