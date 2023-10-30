using UnityEngine.UI;
using UnityEngine;

namespace magic
{
    public class cool_down_slot : MonoBehaviour
    {
        [SerializeField, Header("攻擊系統")]
        Attack_System attack_system;
        [SerializeField, Header("角色資料")]
        Role_Data role_data;
        [SerializeField, Header("技能索引")]
        Skill_Index skill_index;

        Image cool_down_mask;
        private void Start()
        {
            switch (skill_index)
            {
                case Skill_Index.Common_Attack:
                    GetComponent<Image>().sprite = role_data.common_attack.skill_image;
                    break;
                case Skill_Index.Skill_1:
                    GetComponent<Image>().sprite = role_data.skill_1.skill_image;
                    break;
                case Skill_Index.Skill_2:
                    GetComponent<Image>().sprite = role_data.skill_2.skill_image;
                    break;
                case Skill_Index.Skill_3:
                    GetComponent<Image>().sprite = role_data.skill_3.skill_image;
                    break;
            }
            cool_down_mask = transform.GetChild(0).GetComponent<Image>();
        }

        private void Update()
        {
            switch(skill_index)
            {
                case Skill_Index.Common_Attack:
                    cool_down_mask.fillAmount = attack_system.common_attack_timer / role_data.common_attack.cool_time;
                    break;
                case Skill_Index.Skill_1:
                    cool_down_mask.fillAmount = attack_system.skill_1_timer / role_data.skill_1.cool_time;
                    break;
                case Skill_Index.Skill_2:
                    cool_down_mask.fillAmount = attack_system.skill_2_timer / role_data.skill_2.cool_time;
                    break;
                case Skill_Index.Skill_3:
                    cool_down_mask.fillAmount = attack_system.skill_3_timer / role_data.skill_3.cool_time;
                    break;
            }
        }

    }

}
