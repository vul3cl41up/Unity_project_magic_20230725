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

        /// <summary>
        /// 根據技能不同的情況調整技能行為
        /// </summary>
        public virtual void Judge_Action()
        { }    

        /// <summary>
        /// 處理技能的結束
        /// </summary>
        protected virtual void End()
        {
            Destroy(gameObject);
        }
        /// <summary>
        /// 處理技能施放過久
        /// </summary>
        protected virtual void Long_Time()
        {
            Destroy(gameObject, 3f);
        }
        /// <summary>
        /// 執行攻擊
        /// </summary>
        /// <param name="collision">技能物件所碰觸到的物件</param>
        /// <returns></returns>
        protected virtual IEnumerator Start_Attack(Collider2D collision)
        {
            yield return new WaitForSeconds(0.1f);
            if(collision)collision.GetComponent<Enemy_Base>().Take_Damage(skill_data.skill_damage);
        }
    }

}
