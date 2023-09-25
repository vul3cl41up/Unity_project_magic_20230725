using System.Collections;
using UnityEngine;

namespace Model_scene_3
{
    public class Animation_Control : MonoBehaviour
    {
        protected Animator animator;
        public Character_Data character_data;
        [SerializeField]
        protected Skill_Type skill_type;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemy"))
            {
                animator.SetTrigger("touch_trigger");
                StartCoroutine(Start_Attack(collision));
            }
            
        }

        protected virtual void End()
        {
            Destroy(gameObject);
        }

        protected virtual void Long_Time()
        {
            Destroy(gameObject, 3f);
        }

        protected virtual IEnumerator Start_Attack(Collider2D collision)
        {
            yield return new WaitForSeconds(0.2f);
            if(collision)collision.gameObject.GetComponent<Enemy_Base_Control>().Take_Damage(character_data, skill_type);
        }
    }

}
