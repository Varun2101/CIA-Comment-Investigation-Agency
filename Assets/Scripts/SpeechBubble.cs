using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SpeechBubble : MonoBehaviour
{
    public string comment;
    public Text textDisplay;
    // Start is called before the first frame update
    void Start()
    {
         textDisplay.text = comment;
    }

    private void InitComment(string comm)
    {
        comment = comm;
        Debug.Log(comment);
    }

}
