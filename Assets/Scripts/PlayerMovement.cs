using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float trueMoveSpeed = 8.0f;
    private float moveSpeed = 8.0f;

    public Rigidbody2D rb;
    public Camera cam;
    private Animator animator;

    private Vector2 move;
    private Vector2 mouse;
    private bool moving = false;
    private bool nextStage = false;

    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");
        if (move.x == 0 && move.y == 0)
        {
            moving = false;
        }
        else
        {
            moving = true;
        }
        if (move.x != 0 && move.y != 0)
        {
            moveSpeed = trueMoveSpeed / Mathf.Sqrt(2.0f);
        }
        else
        {
            moveSpeed = trueMoveSpeed;
        }

        mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 screenPosDepth = Input.mousePosition;
        screenPosDepth.z = 19.39f; // Give it a depth. Maybe a raycast depth, maybe a clipping plane...
        mouse = Camera.main.ScreenToWorldPoint(screenPosDepth);

        if (Input.GetKeyDown(KeyCode.E))
        {
            nextStage = true;
            Invoke("resetKeyPress", 0.2f);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * (moveSpeed) * Time.fixedDeltaTime);
        Vector2 Dir = mouse - rb.position;
        float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        if (moving)
        {
            animator.speed = 1;
        }
        else if (animator.GetInteger("AnimState") == 0)
        {
            animator.speed = 0;
        }
        else
        {
            animator.speed = 1;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if ( col.tag == "Finish" && nextStage )
        {
            Debug.Log("Next level");
            StartCoroutine(FindObjectOfType<GameMgr>().Restart());
        }
    }

    private void resetKeyPress()
    {
        nextStage = false;
    }
}