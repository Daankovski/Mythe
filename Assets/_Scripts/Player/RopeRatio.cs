using UnityEngine;
using System.Collections;

public class RopeRatio : MonoBehaviour {

    LineRenderer line;
    [SerializeField]
    GameObject player;
    Vector3 grabPos;
    [SerializeField]
    private float ratio = 8;

	// Use this for initialization
	void Start () {
        line = GameObject.Find("chain").GetComponent<LineRenderer>();
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        float scaleX = Vector3.Distance(player.transform.position, grabPos)/ ratio;
        line.material.mainTextureScale = new Vector2(scaleX, 1f);
	}

    public Vector3 GrabPos {
        get { return grabPos; }
        set { grabPos = value; }
    }
}
