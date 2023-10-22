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
            yield return base.Start_Attack(collision);
            if (collision.CompareTag("Enemy"))
            {
                yield return new WaitForSeconds(0.1f);
                Vector3 touch_point = collision.transform.position;
                touch_point.y = (collision.transform.position.y + transform.position.y) / 2;
                Instantiate(hit_prefab, touch_point, Quaternion.identity);
                print("hit");
            }
        }
    }
}

