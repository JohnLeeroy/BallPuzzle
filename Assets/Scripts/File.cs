using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class File : MonoBehaviour 
{
	#region public static void SaveScore( string name, int points )
	public static void SaveScore( string name, int points )
	{
		SimpleAES aes = new SimpleAES();

		// READING STUFF
		StreamReader reader = new StreamReader( Application.dataPath + "/SaveData/Highscores.json" );
		
		string data = reader.ReadLine();
		reader.Close();
		Debug.Log( "Before Encryption: " + data );
		data = aes.DecryptString( data );
		Debug.Log( "After Encryption: " + data );
		IList text = (IList)MiniJSON.Json.Deserialize( data );
		
		int index = 0;
		int score;
		
		foreach(IDictionary scoreData in text)
		{
			score = Convert.ToInt32( scoreData["Score"] );
			if( points > score )
				break;
			else
				index++;
		}
		
		// If the player made the top ten
		if( index < text.Count )
		{
			// Delete the last score
			text.RemoveAt( 9 );
			// Insert the new score at index
			Dictionary<string,string> highscore = new Dictionary<string,string>();
			highscore["Initials"] 				= name;
			highscore["Score"] 					= points.ToString();
			text.Insert( index, highscore );
		}
		
		Debug.Log( "List length: " + text.Count );
		
		// WRITING STUFF
		StreamWriter writer = new StreamWriter( Application.dataPath + "/SaveData/Highscores.json" );
		
		string serial = MiniJSON.Json.Serialize(text);
		
		//print(serial);

		serial = aes.EncryptToString(serial);
		writer.WriteLine(serial);
		writer.Flush();
		writer.Close();
	}
	#endregion
	
	#region public static IList GetHighscores()
	public static IList GetHighscores()
	{
		SimpleAES aes = new SimpleAES();
		// READING STUFF
		StreamReader reader = new StreamReader( Application.dataPath + "/SaveData/Highscores.json" );
		
		string data = reader.ReadLine();
		reader.Close();

		data = aes.DecryptString( data );
		
		IList text = (IList)MiniJSON.Json.Deserialize( data );
		return text;
	}
	#endregion
}
