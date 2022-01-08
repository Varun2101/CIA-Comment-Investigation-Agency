using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
   public Text pointsText;

    public void Update(){
        pointsText.text = "SCORE: "+ PlayerStats.Score.ToString();
    }

}
