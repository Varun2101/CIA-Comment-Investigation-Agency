using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class GameOverDisplay: MonoBehaviour
{
    public Text pointsText;

    public void Start(){
        pointsText.text = "SCORE: "+ PlayerStats.Score.ToString();
    }

}