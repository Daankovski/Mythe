using UnityEngine;
using System.Collections;
using System;

public class PlayerHealth : MonoBehaviour
{
    public float Health;

    public Boolean isLosingHP = false;

    public EnemyAttack EnimAtt;
    public GameObject EnemyScript;

	void Start ()
    {

        EnemyScript = GameObject.FindGameObjectWithTag("Enemy");
        EnimAtt = EnemyScript.GetComponent<EnemyAttack>();
        EnimAtt.isAttacking += isAttacking;
    }

    void isAttacking (Boolean isAttacked)
    {
        

        if (isAttacked == true)
        {
            Debug.Log("i have been struck");
            isLosingHP = true;
            isAttacked = false;
        }
        

    }


    void Update ()
    {
        if (isLosingHP == true)
        {
            
            Health = Health - 1;
            isLosingHP = false;
        }
    }
}
