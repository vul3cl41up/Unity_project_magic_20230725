using System.Collections;
using UnityEngine;

namespace Model_scene_3
{
    public class Animation_Control : MonoBehaviour
    {
        public Role_Control role_control;
        [SerializeField]
        protected Skill_Type skill_type;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemy"))
                StartCoroutine(Start_Attack(collision));
        }

        protected virtual void End()
        {
            Destroy(gameObject);
        }

        protected virtual IEnumerator Start_Attack(Collider2D collision)
        {
            yield return new WaitForSeconds(0.2f);
            collision.gameObject.GetComponent<Enemy_Base_Control>().Take_Damage(role_control, skill_type);
        }
    }

}
