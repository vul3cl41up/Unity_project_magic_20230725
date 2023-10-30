using UnityEngine;
namespace magic
{
    public class Skill_Horn : Skill_Base
    {
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
        }
        private void Start()
        {
            GetComponentInParent<Enemy_Base>().Take_Damage(skill_data.skill_damage);
        }

    }
}


