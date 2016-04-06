using UnityEngine;
using System.Collections;

public class ObjectFloating : MonoBehaviour {

    [SerializeField]
    private float amplitude;
    [SerializeField]
    private float speed;
    private float tempVal;
    private Vector3 tempPos;

	void Start () {
        tempVal = transform.position.y;
	}

	void Update () {
        tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = tempPos;
	}
}
