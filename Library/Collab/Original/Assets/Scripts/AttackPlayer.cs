using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    public Rigidbody2D enemy;
    public Rigidbody2D player;

    public float toxicity;
    private int intervalsPassed = 0;
    private int firing = 1;
    private int initFiring = 1;

    public GameObject bulletPrefab;

    public float bulletForce = 0.1f;

    private float nextActionTime = 0.0f;
    public float period = 0.0f;
    private Animator animator;

    // Start is called before the first frame update
    private void Create(float tox)
    {
        toxicity = tox;
        Debug.Log("The toxicity is: " + toxicity.ToString());
    }

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Rigidbody2D>();
        Vector2 tv_pos = new Vector2(-4.0f, 6.75f);
        Vector2 tv_dir = tv_pos - enemy.position;
        float angle = Mathf.Atan2(tv_dir.y, tv_dir.x) * Mathf.Rad2Deg;
        enemy.rotation = angle;
        animator = GetComponent<Animator>();
    }

    private void PlayReloadAudio()
    {
        GameObject.FindGameObjectWithTag("AudioPlayer").GetComponent<AudioSource>().Play();
    }

    private void MayShoot()
    {
        // shoots a burst of bullets == ceil of toxicity score, then rests for an equal period before firing again
        Vector2 Dir = player.position - enemy.position;
        if (player.position[1] > -6)
        {
            if (firing > 0)
            {
                animator.enabled = true;
                Shoot(Dir);
            }
            else
            {
                animator.enabled = false;
            }
            intervalsPassed += 1;
            if (intervalsPassed % Mathf.Ceil(toxicity) == 0)
            {
                firing = -firing;  // flips between 1 and -1
                if (firing == -1)
                {
                    Invoke("PlayReloadAudio", 0.5f);
                }
            }
        }
        else
        {
            animator.enabled = false;
        }
        Invoke("MayShoot", 0.5f);  // recursively call after fixed intervals
    }

    // Update is called once per frame
    private void Update()

    {
    }

    private void FixedUpdate()
    {
        if (player.position[1] > -6)
        {
            Vector2 Dir = player.position - enemy.position;
            float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
            enemy.rotation = angle;
            if (initFiring > 0){
                Invoke("MayShoot", Random.Range(1.0f, 2.0f));  // start firing 1-2s after player enters the room
                initFiring = 0;
            }
        }
    }

    private void Shoot(Vector2 Dir)
    {
        Dir.Normalize();
        Vector2 spawnPos = enemy.position + (1.0f * Dir);//+ new Vector2(0.003f, 0.003f);

        GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(Dir * bulletForce, ForceMode2D.Impulse);
        Debug.Log(Dir);
    }
}