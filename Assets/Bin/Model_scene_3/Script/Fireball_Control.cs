using System.Collections;
using UnityEngine;
namespace Model_scene_3
{
    public class Fireball_Control : Animation_Control
    {
        bool is_exploit = false;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemy"))
            {
                if(!is_exploit)
                {
                    animator.SetTrigger("touch_trigger");
                    is_exploit = true;
                }
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position - new Vector3(0, 0.25f, 0), 1f);
        }
        public void Start_Attack()
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, 0.25f, 0), 1f);
            if(cols != null)
            for(int i = 0; i < cols.Length; i++)
            {
                if(cols[i].CompareTag("Enemy")) //cols[i].gameObject.GetComponent<Enemy_Base_Control>().Take_Damage(role_control, skill_type);
                    StartCoroutine(Start_Attack(cols[i]));
            }
            
        }

        


        protected override IEnumerator Start_Attack(Collider2D collision)
        {
            yield return new WaitForSeconds(0.02f);
            if(collision) collision.gameObject.GetComponent<Enemy_Base_Control>().Take_Damage(character_data, skill_type);
        }

    }

}
