using UnityEngine;
namespace magic
{
    public class Skill_Horn : Skill_Base
    {
        bool can_attack = true;
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy") && can_attack)
            {
                can_attack = false;
                StartCoroutine(Start_Attack(collision));
            }
        }

    }
}


