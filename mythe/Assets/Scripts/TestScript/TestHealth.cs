using UnityEngine;
using System.Collections;

public class TestHealth : MonoBehaviour
{

    public int Health = 5;
    public EnemyAttack EnimProperty;

	void Update ()
    {

        if (EnimProperty.Attacked == true)
        {
            Health = Health - 1;
        }
        if (Health < 0 | Health == 0)
        {
            Debug.Log("i have fallen");
        }
	}
}
