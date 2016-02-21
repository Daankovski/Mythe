using UnityEngine;
using System.Collections;

public class MoveToMouse : MonoBehaviour {

    SpriteRenderer crosshair;
    Vector3 mousePosition;
    float mouseSpeed = 5f;
    GameObject chainStart;
    GameObject chainEnd;
    GameObject player;

	void Start () {
        crosshair = GetComponent<SpriteRenderer>();
        chainStart = GameObject.Find("chainStart");
        chainEnd = GameObject.Find("chainEnd");
        player = GameObject.Find("Player");
    }
	
	void Update () {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float mousePosX =  Mathf.Clamp(mousePosition.x, player.transform.position.x, chainEnd.transform.position.x);
        float mousePosY = Mathf.Clamp(mousePosition.y, -10, 10);
        crosshair.transform.position = new Vector2(mousePosX, mousePosY);


	}
}
