using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using System.Text;

public class TopScore
{
    public string Name;
    public int Score;
}

public delegate void HighScoreEvent(WebServerHighScores sender);

// Web Server High Score Module
// Usage:
// - Attach as a component to an object
// - Set the public "GameName" property to the name of your game
// - To submit a high score call SubmitHighScore
// - To force a refresh of the high score list call RefreshTopScores
// - You can subscribe to HighScoresUpdated event to be notified whenever GetTopScores has finished refreshing
//
// BTW, the regex expression to accept only letters for entering a name is:
// ^[a-zA-Z]*$
// You're welcome

public class WebServerHighScores : MonoBehaviour {

    public string GameName = "Vanilla";

    public List<TopScore> TopScores = new List<TopScore>();
    public List<TopScore> TopScoresDaily = new List<TopScore>();

    public event HighScoreEvent HighScoresUpdated;
    public event HighScoreEvent HighScoreSubmitted;

    // If you are ever having trouble - try reading this forum post:
    // https://forum.unity.com/threads/unknown-error.425412/

    private string privateKey = "WYATT_CHENG_GAMEJAM_PRIVATE_KEY";
    private string AddScoreURL = "https://wyattcheng.com/games/LeaderboardWebserver/AddScore.php?";
    private string TopScoresURL = "https://wyattcheng.com/games/LeaderboardWebserver/TopScores.php";
    private string TopScoresDailyURL = "https://wyattcheng.com/games/LeaderboardWebserver/TopScoresDaily.php";

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

    IEnumerator AddScore(string name, int score)
    {
        string hash = Md5Sum(name + score + privateKey);

        string query = AddScoreURL + "name=" + UnityWebRequest.EscapeURL(name) + "&score=" + score + "&hash=" + hash + "&game=" + GameName;

//        Debug.Log(query);

        UnityWebRequest ScorePost = UnityWebRequest.Get(query);
        ScorePost.useHttpContinue = false;
        ScorePost.SetRequestHeader("Cache-Control", "max-age=0, no-cache, no-store");
        ScorePost.SetRequestHeader("Pragma", "no-cache");
        yield return ScorePost.SendWebRequest();

        if (ScorePost.error == null)
        {
            HighScoreSubmitted?.Invoke(this);
        }
        else
        {
            Error(ScorePost.error, "AddScore");
            HighScoreSubmitted?.Invoke(this);
        }
    }

    public void SubmitHighScore(string username, int score)
    {
        StartCoroutine(AddScore(username, score));
    }

    public void RefreshTopScores()
    {
        StartCoroutine(GetTopScores());
    }

    IEnumerator GetTopScores()
    {
        // Get Lifetime high scores
        string query = TopScoresURL + "?game=" + GameName;
        UnityWebRequest GetScoresAttempt = UnityWebRequest.Get(query);
        GetScoresAttempt.useHttpContinue = false;
        GetScoresAttempt.SetRequestHeader("Cache-Control", "max-age=0, no-cache, no-store");
        GetScoresAttempt.SetRequestHeader("Pragma", "no-cache");
        yield return GetScoresAttempt.SendWebRequest();

        if (GetScoresAttempt.error != null)
        {
            Error(GetScoresAttempt.error, "GetTopScores");
        }
        else
        {
            //Collect up all our data
            string[] textlist = GetScoresAttempt.downloadHandler.text.Split(new string[] { "\n", "-" }, System.StringSplitOptions.RemoveEmptyEntries);

            int HighScoreListLength = Mathf.FloorToInt(textlist.Length / 2);

            TopScores.Clear();

            for (int i = 0; i < HighScoreListLength; i++)
            {
                TopScore ts = new TopScore();
                ts.Name = textlist[i*2];
                int.TryParse(textlist[i*2+1], out ts.Score);

                TopScores.Add(ts);
            }
        }

        // Get Daily scores
        query = TopScoresDailyURL + "?game=" + GameName;
        UnityWebRequest GetDailyScoresAttempt = UnityWebRequest.Get(query);
        GetDailyScoresAttempt.useHttpContinue = false;
        GetDailyScoresAttempt.SetRequestHeader("Cache-Control", "max-age=0, no-cache, no-store");
        GetDailyScoresAttempt.SetRequestHeader("Pragma", "no-cache");
        yield return GetDailyScoresAttempt.SendWebRequest();

        if (GetDailyScoresAttempt.error != null)
        {
            Error(GetDailyScoresAttempt.error, "GetTopScores");
        }
        else
        {
            //Collect up all our data
            string[] textlist = GetDailyScoresAttempt.downloadHandler.text.Split(new string[] { "\n", "-" }, System.StringSplitOptions.RemoveEmptyEntries);

            int HighScoreListLength = Mathf.FloorToInt(textlist.Length / 2);

            TopScoresDaily.Clear();

            for (int i = 0; i < HighScoreListLength; i++)
            {
                TopScore ts = new TopScore();
                ts.Name = textlist[i * 2];
                int.TryParse(textlist[i * 2 + 1], out ts.Score);

                TopScoresDaily.Add(ts);
            }
        }

        // Done getting scores
        HighScoresUpdated(this);
    }

    public void ClearErrorMessages()
    {
        ErrorMessages = "";
        ErrorMessageCount = 0;
    }

    public int ErrorMessageCount = 0;
    public string ErrorMessages = "";
    void Error(string msg, string caller="")
    {
        ErrorMessages += msg + "(" + caller + ")";
        print("Error trying to post score:" + msg);
    }

    // Use this for initialization
    void Start () {
    }

}
