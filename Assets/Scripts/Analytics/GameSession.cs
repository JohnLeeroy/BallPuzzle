using Splyt;

public class GameSession {

	Transaction gameSession;

	// Use this for initialization
	public void start (string gameMode = "Arcade") {
		gameSession = Splyt.Instrumentation.Transaction("Game");
		gameSession.begin ();
		gameSession.setProperty ("Mode", gameMode);
	}

	public void end(bool isWin, int round, int score, int tries){
		if (gameSession == null)
			return;
		gameSession.setProperty ("Result", (isWin) ? 1 : 0);
		gameSession.setProperty ("Round", round);
		gameSession.setProperty ("Score", score);
		gameSession.setProperty ("Tries", tries);
		gameSession.end ();
	}
}
