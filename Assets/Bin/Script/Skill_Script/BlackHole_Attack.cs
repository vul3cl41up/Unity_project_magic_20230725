using UnityEngine;

namespace magic
{
    public class BlackHole_Attack : Skill_Base
    {
        bool can_attack = true;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemy")&&can_attack)
            {
                StartCoroutine(Start_Attack(collision));
                can_attack = false;
            }
        }
    }
}

