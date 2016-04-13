using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour {
    [SerializeField]
    DistanceJoint2D joint;
    Vector3 targetPos;
    RaycastHit2D hit;
    [SerializeField]
    private float maxDistance;
    private float step = .05f;
    public LayerMask mask;
    [SerializeField]
    LineRenderer line;
    GameObject hookStart;
    GameObject player;
    PlayerBehaviour playerBehaviour;
    [SerializeField]
    private Material chainMat;
    public float xScaleFactor;
    private float adjustedXSize;

    // Use this for initialization
    void Start () {
        hookStart = GameObject.Find("chainStart");
        player = GameObject.Find("Player");
        playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        joint = GameObject.Find("Player").GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line = GameObject.Find("grapplinghook").GetComponent<LineRenderer>();
        line.enabled = false;        
        maxDistance = 20f;
    }
	
	// Update is called once per frame
	void Update () {
        line.transform.position = hookStart.transform.position;

        if (Input.GetKeyDown(KeyCode.R)) {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            
            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, maxDistance, mask);

            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                joint.distance = Vector2.Distance(line.transform.position, hit.point);

                line.enabled = true;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, hit.point);
                line.GetComponent<RopeRatio>().GrabPos = hit.point;
            }

            adjustedXSize = xScaleFactor * (Mathf.Abs(Vector2.Distance(transform.position, hit.point)));
            chainMat.mainTextureScale = new Vector2(adjustedXSize, chainMat.mainTextureScale.y);
        }

        if (Input.GetKey(KeyCode.R)) {
            line.SetPosition(0, transform.position);
            playerBehaviour.MaxSpeed = 20f;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            joint.enabled = false;
            line.enabled = false;
            playerBehaviour.MaxSpeed = 7f;
        }

        if (Input.GetKey(KeyCode.R) && Input.GetMouseButton(1))
        {
            joint.distance -= step * maxDistance;
        }

        if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.W))
        {
            if (joint.distance != 0)
            {
                joint.distance -= step;
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + step);
            }

        }
        else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.S))
        {
            if (joint.distance < maxDistance)
            {
                joint.distance += step;
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - step);
            }
        }

        else {
            if (joint.distance >= maxDistance) {
                joint.distance = maxDistance;
            }
        }

    }
}
