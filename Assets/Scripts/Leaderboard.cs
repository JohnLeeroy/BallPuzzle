using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Leaderboard {

	public List<ScoreRecord> records;

	private static Leaderboard instance;
	public static Leaderboard Instance { 
		get { 
			if(instance == null)
				instance = new Leaderboard(); 
			return instance;
		} 
	}

	private Leaderboard()
	{
		instance = this;
		init ();
	}

	void init() {
		IList data = FileScript.GetHighscores();
		int i = 0;
		records = new List<ScoreRecord> ();
		
		ScoreRecord record;
		foreach(IDictionary scoreData in data)
		{
			record = new ScoreRecord();
			record.rank = i + 1;
			record.score = int.Parse(scoreData["Score"].ToString());
			record.name = scoreData["Initials"].ToString();
			records.Add(record);
			i++;
		}
	}

	public int getScoreRank(int score)
	{
		int scoreCount = records.Count;
		for (int i = 0; i < scoreCount; i++) {
			Debug.Log("Compare " + score + " | " + records[i].score);
			if(score > records[i].score){
				return i;
			}
		}
		return -1;
	}

	public void postScore(int score)
	{
		int rank = getScoreRank (score);
		if (rank == -1)
			return;

		int scoreCount = records.Count;

		for (int i = (scoreCount-1); i > rank; i--) {
			Debug.Log("INDEX " + i);
			records[i].score = records[i-1].score;
		}
		records [rank].score = score;
	}

	public void saveScore(string name, int score)
	{
		int rank = getScoreRank (score);
		if (rank != -1)
			FileScript.SaveScore (name, score);
	}
}
