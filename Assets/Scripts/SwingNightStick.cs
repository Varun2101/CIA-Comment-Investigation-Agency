using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingNightStick : MonoBehaviour
{
    public bool swing = false;
    public Transform nightStickMiddle;
    public float attackRange = 0.5f;
    public bool animationOver = false;

    private Animator animator;
    private List<GameObject> enemiesToDestroy = new List<GameObject>();
    private float animationLength;
    private float lastAtk = 0.0f;

    private float toxicThresh = 0.4f;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        animationLength = animator.runtimeAnimatorController.animationClips[1].length;
    }

    private void checkCollision()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(nightStickMiddle.position, attackRange);
        foreach (Collider2D hitObject in hitObjects)
        {
            Debug.Log("Collided with " + hitObject.gameObject.tag);
            if (hitObject.gameObject.tag == "Enemy" && !hitObject.GetComponent<AttackPlayer>().isDead)
            {
                Debug.Log("Smacked Enemy!");
                hitObject.GetComponent<AttackPlayer>().isDead = true;
                enemiesToDestroy.Add(hitObject.gameObject);
                //Destroy(hitObject.gameObject);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > lastAtk + animationLength)
        {
            swing = true;
            lastAtk = Time.time;
            /*collision = Physics2D.Linecast(nightStickStart.position, nightStickEnd.position);
            Debug.DrawLine(nightStickStart.position, nightStickEnd.position);
            if (collision.collider != null)
            {
                GameObject gameObject = collision.collider.gameObject;
                Debug.Log("Collided with " + gameObject.tag);
                if (gameObject.tag == "Enemy")
                {
                    Debug.Log("Smacked Enemy!");
                    Destroy(collision.collider.gameObject);
                }
            }*/
            Invoke("checkCollision", 0.3f);
        }
    }

    private void FixedUpdate()
    {
        if (swing)
            animator.SetInteger("AnimState", 1);
        if (animationOver)
        {
            float toxicity;
            foreach (GameObject go in enemiesToDestroy)
            {
                Destroy(go);
                enemiesToDestroy.Remove(go);
                toxicity = go.GetComponent<AttackPlayer>().toxicity;
                if (toxicity < 0.4){
                    gameObject.GetComponent<HeartSystem>().TakeDamage();
                    PlayerStats.Score -= 10;
                }
                PlayerStats.Score += 20;
            }
            animationOver = false;
        }
    }
}