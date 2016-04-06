using UnityEngine;
using System.Collections;

public class BlackBackgroundAppear : MonoBehaviour {
    private GameObject[] blackBacks;
    private GameObject blackBackTop;
    private GameObject blackBackBottom;
    private GameObject indoor;

    // Use this for initialization
    void Start()
    {
        Debug.Log(blackBacks.Length + "Length");
        blackBacks = GameObject.FindGameObjectsWithTag("BlackBack");
        blackBackTop = blackBacks[0];
        blackBackBottom = blackBacks[1];
        indoor = blackBacks[2];
        Debug.Log(blackBackTop + "top");
        Debug.Log(blackBackBottom + "bottom");
        Debug.Log(indoor + "Indoor");
    }

    //Enables the black background when the player enters the building
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            blackBackTop.SetActive(true);
            blackBackBottom.SetActive(true);
            indoor.SetActive(false);
        }
    }
}
