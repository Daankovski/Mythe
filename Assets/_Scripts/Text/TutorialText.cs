using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TutorialText : MonoBehaviour {

    //These are the text colliders
    [SerializeField]private GameObject textbox;
    [SerializeField]private GameObject walkTextInit;
    [SerializeField]private GameObject jumpCollider;
    [SerializeField]private GameObject chainSwingCollider;
    [SerializeField]private GameObject endCollider;
    [SerializeField]private GameObject player;

    //These are the text objects
    private Text textBox;

    private List<string> tutorialText;

    private string walkTextString = "You can use D to walk forward and A to walk backwards.";
    private string jumpTextString = "You can use Spacebar to jump.";
    private string chainSwingTextString = "You can use your Chain to grab the hooks hanging from the ceiling or scaffoldings. Use them to reach places you can't get to with a jump.";
    private string endTextString = "Hmm, Let's see why Zeus requested me to come so urgently.";

    //These are the text strings

    void Awake()
    {
        //Find all colliders for which activate text
        textbox = GameObject.Find("textbox");
        walkTextInit = GameObject.Find("walkText");
        jumpCollider = GameObject.Find("jumpText");
        chainSwingCollider = GameObject.Find("chainSwingText");
        endCollider = GameObject.Find("endText");

        tutorialText.Add(walkTextString);
        tutorialText.Add(jumpTextString);
        tutorialText.Add(chainSwingTextString);
        tutorialText.Add(endTextString);

        /*
        walkText.text = walkTextString;
        jumpText.text = jumpTextString;
        chainSwingText.text = chainSwingTextString;
        endText.text = endTextString;   
        */
    }

    //This function immediately tells the player how to move.
    void Start()
    {
        ShowWalkText();
    }

    void ShowWalkText() {
        textbox.gameObject.SetActive(true);
        textBox.gameObject.SetActive(true);
        textBox.text = tutorialText[0];

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            textBox.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hoi");
        if (jumpCollider.tag == "jumpText" && other.tag == "Player")
        {
            Debug.Log("jump");
            textBox.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                textBox.gameObject.SetActive(false);
            }
        }
        if (chainSwingCollider.tag == "chainSwingText" && other.tag == "Player")
        {
            Debug.Log("chainSwing");
            textBox.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                textBox.gameObject.SetActive(false);
            }
        }
        if (endCollider.tag == "EndText" && other.tag == "Player")
        {
            Debug.Log("end");
            textBox.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                textBox.gameObject.SetActive(false);
            }
        }
    }
}
