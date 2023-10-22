using System.Collections;
using UnityEngine;

namespace magic
{
    public class Skill_Base : MonoBehaviour
    {
        [SerializeField, Header("技能資料")]
        protected Skill_Data skill_data;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
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
        }
    }

}
