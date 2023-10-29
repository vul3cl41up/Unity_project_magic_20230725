using System.Collections;
using UnityEngine;
namespace magic
{
    public class Skill_Thunder : Skill_Base
    {
        [SerializeField, Header("®ÄªG")]
        GameObject effect_prefab;
        protected override IEnumerator Start_Attack(Collider2D collision)
        {
            yield return base.Start_Attack(collision);
            GameObject effect = Instantiate(effect_prefab, Vector3.zero, Quaternion.identity,collision.transform);
            effect.GetComponent<Thunder_Effect>().stop_time = skill_data.stop_time;
        }
    }
}


