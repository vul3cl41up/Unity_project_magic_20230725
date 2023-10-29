
using UnityEngine;

namespace magic
{
    public class Skill_Shine : Skill_Base
    {
        Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();
            animator.SetInteger("scale_level", skill_data.skill_level);
        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                animator.SetTrigger("touch_trigger");
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 1.6f*skill_data.scale);
        }
        public void Judge_Attack()
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 1.6f*skill_data.scale);
            if (cols != null)
                for (int i = 0; i < cols.Length; i++)
                {
                    if (cols[i].CompareTag("Enemy")) //cols[i].gameObject.GetComponent<Enemy_Base_Control>().Take_Damage(role_control, skill_type);
                        StartCoroutine(Start_Attack(cols[i]));
                }

        }


    }
}

