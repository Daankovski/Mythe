using UnityEngine;
using System.Collections;

public class TestCamScript : MonoBehaviour
{
    public float speed = 6;
    public GameObject Box;
	
	void Start ()
    {
	
	}
	
	
	void Update ()
    {
        if (Input.GetKeyDown("space"))
        {
            Box.transform.Translate(Vector3.right * speed);
        }
	}
}
