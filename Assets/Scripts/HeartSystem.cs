using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoBehaviour

{
    public static int max_life = 6;
    public GameObject[] hearts = new GameObject[max_life];

    public GameObject heartPrefab;
    public static int init_life = 5;
    public int current_life = init_life;

    public int current_pos = 0;
    public float lastDmg = 0.0f;

    public void Start()
    {
        InitHearts();
    }

    public void Update()
    {
        
    }

    public void FlashPlayer()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void cancelFlashing()
    {
        CancelInvoke("FlashPlayer");
    }

    public void TakeDamage()
    {
        if (current_life > 0 && Time.time > lastDmg + 1.0f)
        {
            Destroy(hearts[current_life - 1]);
            InvokeRepeating("FlashPlayer", 0.0f, 0.1f);
            current_life -= 1;
            Debug.Log("Current Life" + current_life);
            lastDmg = Time.time;
            if (current_life == 2)
            {
                Invoke("spawnHeartRandom", 1.0f);
                cancelFlashing();
            }
            else
            {
                Invoke("cancelFlashing", 1.0f);
            }
        }
        if (current_life == 0){
            Debug.Log("No life");
            FindObjectOfType<GameMgr>().EndGame();
        }
    }

    public void InitHearts()
    {
        Debug.Log("Init life " + init_life);
        Debug.Log("Hearts Array" + hearts.Length);
        for (int i = 0; i < init_life; i++)
        {
            Vector2 position = new Vector2(-11.5f, 7.43f) + (i) * new Vector2(-1f, 0);
            GameObject go = Instantiate(heartPrefab, position, Quaternion.identity) as GameObject;
            hearts[i] = go;
        }
    }

    public void addHeart()
    {
        if (current_life < max_life && current_life > 0)
        {
            current_life += 1;
            Vector2 position = new Vector2(-11.5f, 7.43f) + (current_life - 1) * new Vector2(-1f, 0);
            GameObject go = Instantiate(heartPrefab, position, Quaternion.identity) as GameObject;
            hearts[current_life - 1] = go;
        }
    }

    public void spawnHeartRandom()
    {
        Vector2 position = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-5.0f, 5.0f));
        GameObject go = Instantiate(heartPrefab, position, Quaternion.identity) as GameObject;
    }
}



