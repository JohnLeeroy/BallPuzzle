using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class FileScript : MonoBehaviour 
{
	#region public static void SaveScore( string name, int points )
	public static void SaveScore( string name, int points )
	{
		string defaultText = "142029026187102151075253247010187202235071023235163210213253185157241056206036201120235139177081109104069125225148163057036058178011033180008170149224135124132091078083010066135207001179081016024071051143117191031037106038012138080099050183152014044118097152121110210068038202082036227010132202056061021070229034038013117097067233000140212004169175049065232165034087216153058164044179207062064073038234253165254068000103158251200220001019163052120034031199247087122177009207007126074177169037180061074118119104052243082135171217203197184226250242016153062123048112237127168214249169225211229239157027176069171133015119170149000217162007169070176058057211213248094097085062248115237096184210217253118172091164120153118198129062208080229034235019193114077050000063140239238159204124091195017088110075031126184226141036038205058062028051110145041150187049033119082051173074039220086167138086150051063062163093057253217157167229155137222166228135013007098058192146";
		string path = Application.persistentDataPath + "/SaveData/";
		string filePath = path + "highscores.json";
		if( !Directory.Exists( path ) )
		{
			Directory.CreateDirectory( path );
			if( !File.Exists( filePath ) )
			{
				File.Create( filePath );
				File.WriteAllText( filePath, defaultText );
			}
		}

		SimpleAES aes = new SimpleAES();

		// READING STUFF
		StreamReader reader = new StreamReader( filePath );
		
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
		StreamWriter writer = new StreamWriter( filePath );
		
		string serial = MiniJSON.Json.Serialize(text);
		
		//print(serial);

		serial = aes.EncryptToString(serial);
		writer.WriteLine(serial);
		writer.Flush();
		writer.Close();
	}
	#endregion

	public static void SaveLeaderboard( )
	{
		string defaultText = "142029026187102151075253247010187202235071023235163210213253185157241056206036201120235139177081109104069125225148163057036058178011033180008170149224135124132091078083010066135207001179081016024071051143117191031037106038012138080099050183152014044118097152121110210068038202082036227010132202056061021070229034038013117097067233000140212004169175049065232165034087216153058164044179207062064073038234253165254068000103158251200220001019163052120034031199247087122177009207007126074177169037180061074118119104052243082135171217203197184226250242016153062123048112237127168214249169225211229239157027176069171133015119170149000217162007169070176058057211213248094097085062248115237096184210217253118172091164120153118198129062208080229034235019193114077050000063140239238159204124091195017088110075031126184226141036038205058062028051110145041150187049033119082051173074039220086167138086150051063062163093057253217157167229155137222166228135013007098058192146";
		string path = Application.persistentDataPath + "/SaveData/";
		string filePath = path + "highscores.json";
		if( !Directory.Exists( path ) )
		{
			Directory.CreateDirectory( path );
			if( !File.Exists( filePath ) )
			{
				File.Create( filePath );
				File.WriteAllText( filePath, defaultText );
			}
		}
		
		SimpleAES aes = new SimpleAES();
		
		// READING STUFF
		StreamReader reader = new StreamReader( filePath );
		
		string savedFile = reader.ReadLine();
		reader.Close();
		Debug.Log( "Before Encryption: " + savedFile );
		savedFile = aes.DecryptString( savedFile );
		Debug.Log( "After Encryption: " + savedFile );
		IList data = (IList)MiniJSON.Json.Deserialize( savedFile );
		
		int index = 0;
		int score;

		Leaderboard leaderboard = Leaderboard.Instance;
		foreach(IDictionary scoreData in data)
		{
			scoreData["Score"] = leaderboard.records[index].score;
			scoreData["Initials"] = leaderboard.records[index].name;
			index++;
		}
		// WRITING STUFF
		StreamWriter writer = new StreamWriter( filePath );
		
		string serial = MiniJSON.Json.Serialize(data);
		
		//print(serial);
		
		serial = aes.EncryptToString(serial);
		writer.WriteLine(serial);
		writer.Flush();
		writer.Close();
	}
	
	#region public static IList GetHighscores()
	public static IList GetHighscores()
	{
		string defaultText = "142029026187102151075253247010187202235071023235163210213253185157241056206036201120235139177081109104069125225148163057036058178011033180008170149224135124132091078083010066135207001179081016024071051143117191031037106038012138080099050183152014044118097152121110210068038202082036227010132202056061021070229034038013117097067233000140212004169175049065232165034087216153058164044179207062064073038234253165254068000103158251200220001019163052120034031199247087122177009207007126074177169037180061074118119104052243082135171217203197184226250242016153062123048112237127168214249169225211229239157027176069171133015119170149000217162007169070176058057211213248094097085062248115237096184210217253118172091164120153118198129062208080229034235019193114077050000063140239238159204124091195017088110075031126184226141036038205058062028051110145041150187049033119082051173074039220086167138086150051063062163093057253217157167229155137222166228135013007098058192146";
		string path = Application.persistentDataPath + "/SaveData/";
		string filePath = path + "highscores.json";
		if( !Directory.Exists( path ) )
		{
			Directory.CreateDirectory( path );
			if( !File.Exists( filePath ) )
			{
				FileStream fs = File.Create( filePath );
				fs.Close();
				File.WriteAllText( filePath, defaultText );

			}
		}

		SimpleAES aes = new SimpleAES();
		// READING STUFF
		StreamReader reader = new StreamReader( filePath );
		
		string data = reader.ReadLine();
		reader.Close();

		data = aes.DecryptString( data );
		
		IList text = (IList)MiniJSON.Json.Deserialize( data );
		return text;
	}
	#endregion
}
