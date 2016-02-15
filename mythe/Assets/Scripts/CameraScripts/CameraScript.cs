using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public GameObject target;

    private Vector3 CamPos;
	
	void Start ()
    {
        CamPos = transform.position - target.transform.position;
	}
	

	void LateUpdate ()
    {
        transform.position = target.transform.position + CamPos;
	}
}
