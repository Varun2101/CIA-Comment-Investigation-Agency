using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public GameObject LoadingScreen;
    public void EndGame()
    {
        Debug.Log("GAME OVER");
        SceneManager.LoadScene("GameOver");
    }

    public IEnumerator Restart()
    {

        Debug.Log("RESTART");
        LoadingScreen.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        LoadingScreen.gameObject.SetActive(false);
        SceneManager.LoadScene("SampleScene");

    }

    public void restartAfterQuit()
    {
        SceneManager.LoadScene("SampleScene");
        PlayerStats.Score = 0;
    }
}