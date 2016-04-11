using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    Animator anim;


    public int attackHash = Animator.StringToHash("Attack");

    public delegate void Attack(bool Attacked);

    public GameObject EnimPos;

    //public bool isSeeing = false;
    public bool isStartTimer = false;
    

    //public bool PlayerInSight = false;
    public float TimeLeft = 10;

    public float AnimTimer = 5;
    

    //for test purposes
    public bool Attacked = false;

    public Attack isAttacking;

    public EnemyMovement EnimMove;

    //-----------------
    

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Attack", Attacked);
        EnimMove = gameObject.GetComponent<EnemyMovement>();
        EnimMove.SightChanged += SightChanged;
    }

    void SightChanged(bool isSeeing)
    {
       
        if (isSeeing == true)
        {
            isStartTimer = true;
        }

    }


    //and maybe more states later.


    void Update()
    {

        if (isStartTimer == true)
        {
            TimeLeft -= Time.deltaTime;
            if (TimeLeft < 0)
            {
               
                    anim.SetTrigger(attackHash);
                
                Debug.Log("i attack the enemy!");
                Attacked = true;
                
                DispatchHit();
                TimeLeft = 10;
               

            }
            if (Attacked == true)
            {
                AnimTimer = AnimTimer - 2;

            }

            if (TimeLeft < 10)
            {


                DispatchHit();
                Attacked = false;
                
                if (AnimTimer < 0)
                {
                    anim.SetBool("Attack", Attacked);
                    AnimTimer = 5;
                }
                
            }
        }

       
    }

    void DispatchHit()
    {
        if (isAttacking != null)
            isAttacking(Attacked);
    }


}
