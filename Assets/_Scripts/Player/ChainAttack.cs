using UnityEngine;
using System.Collections;

public class ChainAttack : MonoBehaviour {

    [SerializeField]
    private LineRenderer chainLineRenderer;
    [SerializeField]
    private bool isClickedOnce;
    private float DCTimer;
    private GameObject chainStart;
    private bool isAttacking = false;
    private RaycastHit2D hit;
    Vector3 targetPos;
    public LayerMask mask;
    GameObject chainEnd;
    float maxChainLength;

    void Awake()
    { 
        chainLineRenderer = GameObject.Find("chain").GetComponent<LineRenderer>();
        chainStart = GameObject.Find("chainStart");
        chainEnd = GameObject.Find("chainEnd");
        maxChainLength = chainEnd.transform.position.x - chainStart.transform.position.x;

    }

    void Start () {
        chainLineRenderer.enabled = false;
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            chainCast();

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            chainLineRenderer.enabled = false;
        }
    }

    void chainCast() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            chainLineRenderer.enabled = true;
            chainLineRenderer.SetPosition(0, chainStart.transform.position);
            chainLineRenderer.SetPosition(1, chainEnd.transform.position);

            hit = Physics2D.Raycast(chainStart.transform.position, targetPos - chainStart.transform.position, maxChainLength, mask);

            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null) {
                chainLineRenderer.enabled = true;
                chainLineRenderer.SetPosition(0, chainStart.transform.position);
                chainLineRenderer.SetPosition(1, hit.point);
                chainLineRenderer.GetComponent<RopeRatio>().GrabPos = hit.point;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            chainLineRenderer.SetPosition(0, chainStart.transform.position);
        }

    }
}
