using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSensor : MonoBehaviour
{
    private GameObject attackTarget;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
            attackTarget = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        attackTarget = null;
    }

    public GameObject AttackTarget()
    {
        return attackTarget;
    }
}
