using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreUtility : MonoBehaviour {

    // Helper Utility functions for use with WebServerHighScores

    // wshs - WebServerHighScores object
    // namesControl - Unity Text object to receive 20 names, separated by newlines
    // scoresControl - Unity Text object to receive 20 scores, separated by newlines
    // username - an optional username to highlight if that user is found
    // start - What position number to start the dump at

    public static void Dump20ScoresToTextControls(WebServerHighScores wshs, TextMeshProUGUI namesControl, TextMeshProUGUI scoresControl, string username, int start)
    {
        string sNames = "";
        string sScores = "";
        int MyPosition = FindMyPosition(wshs, username);

        for (int Place = start; Place < start + 20; Place++)
        {
            if (Place < wshs.TopScores.Count)
            {
                sNames += CreateNameString(Place, wshs, MyPosition) + "\n";
                sScores += CreateScoreString(Place, wshs, MyPosition) + "\n";
            }
        }

        namesControl.text = sNames;
        scoresControl.text = sScores;
    }

    public static int RecommendedStartIndexForMoreScores(WebServerHighScores wshs, string username, int showingTop = 20)
    {
        int MyPosition = FindMyPosition(wshs, username);
        if (MyPosition < showingTop + showingTop / 2)
        {
            // Just show the next 20
            return showingTop;
        }
        else if (MyPosition < wshs.TopScores.Count - (showingTop /2 + 1))
        {
            // Show the 20 around me
            return MyPosition - showingTop / 2;
        }
        else
        {
            // Dump the bottom 20
            return wshs.TopScores.Count - showingTop;
        }
    }

    public static string CreateNameString(int rank, WebServerHighScores wshs, int highlightPosition)
    {
        string result = "";

        if (rank == highlightPosition)
        {
            result += "<color=yellow>";
        }

        result += (rank + 1).ToString() + " - " + wshs.TopScores[rank].Name;

        if (rank == highlightPosition)
        {
            result += "</color>";
        }

        return result;
    }

    public static string CreateScoreString(int rank, WebServerHighScores wshs, int highlightPosition)
    {
        string result = "";

        if (rank == highlightPosition)
        {
            result += "<color=yellow>";
        }

        result += wshs.TopScores[rank].Score.ToString("##,##0");

        if (rank == highlightPosition)
        {
            result += "</color>";
        }

        return result;
    }

    public static int FindMyPosition(WebServerHighScores wshs, string username)
    {
        for (int i = 0; i < wshs.TopScores.Count; i++)
        {
            if (wshs.TopScores[i].Name == username)
            {
                return i;
            }
        }

        return -1;
    }

}
