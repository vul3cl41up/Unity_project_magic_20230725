using System.Collections;
using UnityEngine;

namespace magic
{
    public class Skill_boomWave : Skill_Base
    {
        [SerializeField, Header("Ä²µo¹w¸mª«")]
        GameObject hit_prefab;

        protected override IEnumerator Start_Attack(Collider2D collision)
        {
            if (collision)
            {
                yield return new WaitForSeconds(0.2f);
                Vector3 touch_point = collision.transform.position;
                touch_point.y = (collision.transform.position.y + transform.position.y) / 2;
                Instantiate(hit_prefab, touch_point, Quaternion.identity);
                collision.GetComponent<Enemy_Base>().Take_Damage(skill_data.skill_damage);
            }
                
        }
    }
}

