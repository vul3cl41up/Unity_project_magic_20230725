using System.Collections;
using UnityEngine;
namespace Model_scene_3
{
    public class Fireball_Control : Animation_Control
    {
        Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if(collision.CompareTag("Enemy"))
            {
                animator.SetTrigger("touch_trigger");
                if (collision) StartCoroutine(Start_Attack(collision));
            }
                
        }



        protected override void End()
        {
            Destroy(gameObject);
        }
        protected void Long_Time()
        {
            Destroy(gameObject,3f);
        }

        protected override IEnumerator Start_Attack(Collider2D collision)
        {
            yield return new WaitForSeconds(0.2f);
            if(collision)collision.gameObject.GetComponent<Enemy_Base_Control>().Take_Damage(role_control, skill_type);
        }

    }

}
