using System.Collections;
using UnityEngine;

namespace Model_scene_3
{
    public class Animation_Control : MonoBehaviour
    {
        [SerializeField]
        private Character_Data character_data;
        [SerializeField]
        private Skill_Data skill_data;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            StartCoroutine(Start_Attack(collision));
        }

        void End()
        {
            gameObject.GetComponentInParent<Role_Control>().can_attack = true;
            gameObject.GetComponentInParent<Role_Control>().is_attack = false;
            gameObject.SetActive(false);
        }

        IEnumerator Start_Attack(Collider2D collision)
        {
            yield return new WaitForSeconds(0.2f);
            collision.gameObject.GetComponent<Enemy_Base_Control>().Take_Damage(character_data, skill_data);
        }
    }

}
