using UnityEngine;
using System.Collections;

public class MoveToMouse : MonoBehaviour {

    SpriteRenderer crosshair;
    Vector3 mousePosition;
    float mouseSpeed = 5f;
    GameObject chainStart;
    GameObject chainEnd;
    GameObject player;
    Vector3 playerPos;
    Vector3 crosshairPos;


	void Start () {
        crosshair = GetComponent<SpriteRenderer>();
        chainStart = GameObject.Find("chainStart");
        chainEnd = GameObject.Find("chainEnd");
        player = GameObject.Find("Player");
        
    }
	
	void Update () {
        playerPos = player.transform.position;
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        crosshairPos = mousePosition - playerPos;
        float maxClamp = Vector3.Distance(chainEnd.transform.position, player.transform.position);
        crosshair.transform.position = playerPos + Vector3.Normalize(crosshairPos) * Mathf.Clamp(crosshairPos.magnitude, 2, maxClamp);
	}
}
