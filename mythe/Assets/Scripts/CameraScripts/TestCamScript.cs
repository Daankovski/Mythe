using UnityEngine;
using System.Collections;

public class TestCamScript : MonoBehaviour
{
    public float speed = 6;
    public GameObject Box;
    public bool Attacks = false;
	
	void Start ()
    {
	
	}
	
	
	void Update ()
    {
        if (Input.GetKeyDown("right"))
        {
            Box.transform.Translate(Vector3.right * speed);
        }

        if (Input.GetKeyDown("up"))
        {
            Box.transform.Translate(Vector3.up * Time.deltaTime);
        }

        if (Input.GetKeyDown("down"))
        {
            Attacks = true;
        }

        if (Input.GetKeyUp("down"))
        {
            Attacks = false;
        }


    }
}
