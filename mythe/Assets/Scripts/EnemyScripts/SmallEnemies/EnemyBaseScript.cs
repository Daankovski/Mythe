using UnityEngine;
using System.Collections;

public class EnemyBaseScript : MonoBehaviour
{
    public GameObject EnimPos;
    public float BaseSpeed = 5;
    public bool PlayerInSight = false;
    public float TimeLeft = 10;
    public int EnimBaseHealth = 2;

    //for test purposes
    public bool Attacked = false;
    public TestCamScript foe;

    //-----------------
	
	void Start ()
    {
	
	}
	
	
	void Update ()
    {
        if (EnimBaseHealth == 0)
        {
            Destroy(this);
        }

        if (foe.Attacks == true)
        {
            EnimBaseHealth = EnimBaseHealth - 1;
        }

        if (PlayerInSight != true)
        {
            EnimPos.transform.Translate(Vector3.left * BaseSpeed);
        }
        if (PlayerInSight == true)
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft < 0)
            {
                Debug.Log("i attack the enemy!");
                Attacked = true;
                TimeLeft = 10;
            }

            if(TimeLeft < 10)
            {
                Attacked = false;
            }
        }
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("a foe!");
        
        if (other.gameObject.tag == "Player")
        {
            PlayerInSight = true;
            
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("the foe has flead or has been vanqished");
        if (other.gameObject.tag == "Player")
        {
            PlayerInSight = false;
        }
    }
}
