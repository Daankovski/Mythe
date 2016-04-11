using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{

    public int EHealth = 2;
    public bool isELosingHP = false;

    //for test purposes


    //-----------------
    public TestCamScript foe;
    public PlayerAttack PlayerAtt;
    public GameObject PlayerScript;

    //and maybe more states later.

    void Start()
    {
        PlayerScript = GameObject.FindGameObjectWithTag("Player");
        PlayerAtt = PlayerScript.GetComponent<PlayerAttack>();
        Debug.Log("health");
        PlayerAtt.isPAttacking += isPAttacking;
    }

  

    void isPAttacking(bool isPAttacked)
    {
        Debug.Log("i work");

        if (isPAttacked == true)
        {
            Debug.Log("au");
            isELosingHP = true;
            isPAttacked = false;
        }
        Debug.Log(isPAttacked);
        Debug.Log(EHealth);

    }

    void Update()
    {
        if (isELosingHP == true)
        {
           
            EHealth = EHealth - 1;
            isELosingHP = false;
        }
    }

}
