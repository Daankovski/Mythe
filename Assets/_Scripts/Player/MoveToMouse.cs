using UnityEngine;
using System.Collections;

public class MoveToMouse : MonoBehaviour {

    SpriteRenderer crosshair;
    Vector3 mousePosition;
    float mouseSpeed = 5f;
    Vector3 crosshairPos;


	void Start () {
        crosshair = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
        
    }
	
	void Update () {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = Vector2.Lerp(transform.position, mousePosition, mouseSpeed);
    }

    void LockMousePosition() {

    }
}
