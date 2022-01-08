using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawnScript : MonoBehaviour
{
    public float numEnemies;
    public GameObject enemy;
    public LayerMask collidableLayers;
    public string number;

    // Start is called before the first frame update
    private void Start()
    {
        float randX, randY;
        int tries;
        Vector3 position;
        Vector2 tempPos;
        string[] comments = {
            "Whoever bullied this dude in high school please just apologize. He's off the rails.",
            "Now China has a sun as well",
            "you want your 3 kicked",
            "No matter what you say about the Metaverse (scary as name as well) you will never, NEVER get the same feeling of climbing a mountain in real life or meeting with friends in a restaurant.",
            "Fucking Bezos has managed to turn space travel into a vanity/waste of money for most people. And man I am pissed at him for doing that. Why cant corporate lunatic billionaires leave us alone and stop spoiling everything we have?",
            "Look at the damage social media has done to society, literally destroying families and relationships.. I hate to think the same effect this will have on our species in the years to come.",
            "yall use it, they make it. who�s really the problem�",
            "@Jess Wilson he knows what he's doing. Look at the movie Wallie I believe it was.",
            "People already prefer fake appreciations (Likes) over someone telling them in real life that they appreciate them and it will be the same when living in a VR world.",
            "REALLY fucked up that the local governments around the area of Myanmar and Ethiopia are letting this happen.",
            "I hate your work, I think you should just give up...",
            "Wow, I am a huge fannnnnnn <3 ",
            "Idk why you show your fat face online, just get a job man!",
            "Lets get this party started...",
            "this is shit lol, how do they get views?",
            "I feel you should jump off a cliff",
            "Don't ruin my day, shut up now"
        };
        for (int i = 0; i < numEnemies; i++)
        {
            tries = 0;
            while (true)
            {
                randX = Random.Range(-8.0f, 8.0f);
                randY = Random.Range(-5.0f, 5.0f);
                tempPos = new Vector2(randX, randY);
                Collider2D[] hitObjects = Physics2D.OverlapCircleAll(tempPos, 0.5f, collidableLayers);
                if (hitObjects.Length == 0)
                {
                    break;  // new position did not collide with anything
                }
                else
                {
                    tries += 1;
                    if (tries == 50)
                    {
                        //failed to place enemy after 50 tries, give up on adding enemy in this layout
                        tries = -1;  // flag to stop
                        break;
                    }
                }
            }
            if (tries == -1)
            {
                Debug.Log("Could not find optimal enemy placement, quit early.");
            }
            position = new Vector3(randX, randY, 0);
            GameObject go = Instantiate(enemy, position, Quaternion.identity) as GameObject;
            int randomIndex = Random.Range(0, comments.Length);
            string randomComment = comments[randomIndex];
            go.SendMessage("InitComment", randomComment);
            StartCoroutine(getToxicity(randomComment, go));
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost:3000/getCommentToxicity", CityId, API_KEY));
            go.SendMessage("Create", Random.Range(0.0f, 5.0f));
        }

        IEnumerator getToxicity(string randomComment, GameObject go)
        {
            WWWForm form = new WWWForm();
            form.AddField("comment", randomComment);

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/getCommentToxicity", form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Debug.Log("POST successful!");
                    StringBuilder sb = new StringBuilder();
                    float toxicity = 0.0f;
                    foreach (System.Collections.Generic.KeyValuePair<string, string> dict in www.GetResponseHeaders())
                    {
                        sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
                    }

                    // Print Headers
                    //Debug.Log(sb.ToString());

                    // Print Body
                    Debug.Log(www.downloadHandler.text);
                    string body = www.downloadHandler.text;
                    number = body.Substring(12, body.Length - 13);
                    Debug.Log(number);
                    //Debug.Log(float.Parse("0.1009563", CultureInfo.InvariantCulture.NumberFormat));
                    toxicity = float.Parse(number, CultureInfo.InvariantCulture.NumberFormat);
                    go.SendMessage("Create", toxicity);
                }
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}