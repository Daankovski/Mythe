using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{

    public GameObject EnimPos;
    public float BaseSpeed = 5;
    public bool PlayerInSight = false;
    public delegate void Sight(bool PlayerInSight);


    //for test purposes


    //-----------------

    public Sight SightChanged;

    //and maybe more states later.


    void Update()
    {
        

        if (PlayerInSight != true)
        {
            EnimPos.transform.Translate(Vector3.left * BaseSpeed);
        }
        if (PlayerInSight == true)
        {
            
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("a foe!");

        if (other.gameObject.tag == "Player")
        {
            PlayerInSight = true;
            DispatchChange();

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("the foe has flead or has been vanqished");
        if (other.gameObject.tag == "Player")
        {
            PlayerInSight = false;
            DispatchChange();
        }
    }

    void DispatchChange(){
        if(SightChanged != null)
            SightChanged(PlayerInSight);
    }
}
