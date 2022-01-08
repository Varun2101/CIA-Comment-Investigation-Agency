using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayComment : MonoBehaviour
{
    public GameObject speechBubble;
    public bool firstHover = true;
    private GameObject player;
    public bool display = true;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    private void Update()
    {
        if (player.transform.position[1] > -5.5 || display == false)
        {
            display = false;
        }
    }

    public void OnMouseEnter()
    {
        //get mouse position
        string comment = gameObject.GetComponent<AttackPlayer>().comment;
        if(firstHover)
        {
            Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            Vector3 screenPosDepth = Input.mousePosition;
            screenPosDepth.z = 19.39f; // Give it a depth. Maybe a raycast depth, maybe a clipping plane...
            Vector3 mouse = Camera.main.ScreenToWorldPoint(screenPosDepth);

            //If your mouse hovers over the GameObject with the script attached, output this message
            Debug.Log("Mouse is over enemy.");
            Vector3 position = new Vector3(mouse[0] - 3f, mouse[1] + 3f, mouse[2]);
            speechBubble = Instantiate(speechBubble, position , Quaternion.identity);
            speechBubble.SendMessage("InitComment", comment);
            firstHover = false;
        }
        else
        {
            if (display == true) speechBubble.SetActive(true);
        }


    }

    public void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
        speechBubble.SetActive(false);
    }
    //need to disappear bubble after few minutes
}



