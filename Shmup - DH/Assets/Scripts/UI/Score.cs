using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreboard;

    int score = 0;

    public void ScoreUpdate()
    {
        score += 10;
        scoreboard.text = string.Format("{0}", score);
    } 
}
