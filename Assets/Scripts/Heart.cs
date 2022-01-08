using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public GameObject player;

    void OnCollisionEnter2D(Collision2D collision){
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            player.GetComponent<HeartSystem>().addHeart();
        }
    }
}
