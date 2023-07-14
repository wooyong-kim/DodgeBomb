using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TMPro.TMP_Text myScoreLabel = null;
    int myScore = 0;
    public int Score
    {
        get => myScore;
        set
        {
            myScore = value;
            myScoreLabel.text = myScore.ToString();
        }
    }
}
