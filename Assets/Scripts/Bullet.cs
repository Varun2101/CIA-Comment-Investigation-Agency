using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;

    void OnCollisionEnter2D(Collision2D collision){
        // Debug.Log("Destroy bullet");
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            player.GetComponent<HeartSystem>().TakeDamage();
        }
        else{
            Destroy(gameObject, 0.5f);
        }
    }

}