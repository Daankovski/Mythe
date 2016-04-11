using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public bool PlayerAttacks = false;
    public delegate void PAttack(bool PlayerAttacks);

    public PAttack isPAttacking;

	void Start ()
    {
	
	}
	
	
	void Update ()
    {
        if (Input.GetKeyDown("space"))
        {
            PlayerAttacks = true;
            DispatchHit();
        }
    }

    void DispatchHit()
    {
        Debug.Log("space");
        if (isPAttacking != null)
            isPAttacking(PlayerAttacks);
    }
}
