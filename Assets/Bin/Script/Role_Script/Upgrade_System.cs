using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace magic
{
    public class Upgrade_System : MonoBehaviour
    {
        [SerializeField, Header("需要經驗值")]
        private List<float> need_exp;
        [SerializeField, Header("等級文字")]
        private TMP_Text text_level;
        [SerializeField, Header("經驗值物件")]
        private GameObject object_exp;
        [SerializeField, Header("技能升級UI")]
        private GameObject object_upgrade_UI;
        [SerializeField, Header("技能升級卡")]
        private GameObject object_upgrade_slot;
        [SerializeField, Header("角色資料")]
        Role_Data role_data;
        [SerializeField, Header("事件系統")]
        EventSystem event_system;

        private int role_level;

        private float current_exp;
        private int current_object_exp;
        private void Start()
        {

            role_data.common_attack.skill_level = 0;
            role_data.common_attack.skill_damage = role_data.common_attack.skill_damage_List[0];
            role_data.common_attack.cool_time = role_data.common_attack.cool_time_List[0];

            role_data.skill_1.skill_level = 0;
            role_data.skill_1.skill_damage = role_data.skill_1.skill_damage_List[0];
            role_data.skill_1.cool_time = role_data.skill_1.cool_time_List[0];

            role_data.skill_2.skill_level = 0;
            role_data.skill_2.skill_damage = role_data.skill_2.skill_damage_List[0];
            role_data.skill_2.cool_time = role_data.skill_2.cool_time_List[0];

            role_data.skill_3.skill_level = 0;
            role_data.skill_3.skill_damage = role_data.skill_3.skill_damage_List[0];
            role_data.skill_3.cool_time = role_data.skill_3.cool_time_List[0];

            role_level = 1;
            current_exp = 0;
            current_object_exp = 0;
            text_level.text = role_level.ToString();
            for (int i = 0; i < object_exp.transform.childCount; i++)
            {
                object_exp.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        public void Get_Exp(float exp)
        {
            current_exp += exp;
            print(role_level);
            while (current_exp >= need_exp[role_level])
            {
                Level_Up();
            }
            while ((current_exp / need_exp[role_level] * 50) > (current_object_exp + 1))
            {
                object_exp.transform.GetChild(current_object_exp).gameObject.SetActive(true);
                current_object_exp++;
            }
        }
        private void Level_Up()
        {
            current_exp -= need_exp[role_level];
            role_level++;
            for (int i = 0; i <= current_object_exp; i++)
            {
                object_exp.transform.GetChild(i).gameObject.SetActive(false);
            }
            current_object_exp = 0;
            text_level.text = role_level.ToString();
            object_upgrade_UI.SetActive(true);
            Init_Upgrade_UI();
        }
        void Init_Upgrade_UI()
        {
            if(role_data.common_attack != null)
            {
                GameObject new_object = Instantiate(object_upgrade_slot,Vector3.zero, Quaternion.identity,object_upgrade_UI.transform);
                event_system.firstSelectedGameObject = new_object;
                new_object.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = role_data.common_attack.skill_name;
                new_object.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = 
                    role_data.common_attack.skill_upgrade_description[role_data.common_attack.skill_level];
                new_object.GetComponent<Button>().onClick.AddListener(Common_Attack_Upgrade);
            }
            if (role_data.skill_1 != null)
            {
                GameObject new_object = Instantiate(object_upgrade_slot, Vector3.zero, Quaternion.identity, object_upgrade_UI.transform);
                new_object.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = role_data.skill_1.skill_name;
                new_object.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    role_data.skill_1.skill_upgrade_description[role_data.skill_1.skill_level];
                new_object.GetComponent<Button>().onClick.AddListener(Skill_1_Upgrade);
            }
            if (role_data.skill_2 != null)
            {
                GameObject new_object = Instantiate(object_upgrade_slot, Vector3.zero, Quaternion.identity, object_upgrade_UI.transform);
                new_object.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = role_data.skill_2.skill_name;
                new_object.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    role_data.skill_2.skill_upgrade_description[role_data.skill_2.skill_level];
                new_object.GetComponent<Button>().onClick.AddListener(Skill_2_Upgrade);
            }
            if (role_data.skill_3 != null)
            {
                GameObject new_object = Instantiate(object_upgrade_slot, Vector3.zero, Quaternion.identity, object_upgrade_UI.transform);
                new_object.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = role_data.skill_3.skill_name;
                new_object.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                    role_data.skill_3.skill_upgrade_description[role_data.skill_3.skill_level];
                new_object.GetComponent<Button>().onClick.AddListener(Skill_3_Upgrade);
            }
            
            Time.timeScale = 0;
        }
        void Common_Attack_Upgrade()
        {
            role_data.common_attack.skill_level++;
            role_data.common_attack.skill_damage =
                role_data.common_attack.skill_damage_List[role_data.common_attack.skill_level];
            role_data.common_attack.cool_time =
                role_data.common_attack.cool_time_List[role_data.common_attack.skill_level];

        }
        void Skill_1_Upgrade()
        {
            role_data.skill_1.skill_level++;
            role_data.skill_1.skill_damage =
                role_data.skill_1.skill_damage_List[role_data.skill_1.skill_level];
            role_data.skill_1.cool_time =
                role_data.skill_1.cool_time_List[role_data.skill_1.skill_level];
        }
        void Skill_2_Upgrade()
        {
            role_data.skill_2.skill_level++;
            role_data.skill_2.skill_damage =
                role_data.skill_2.skill_damage_List[role_data.skill_2.skill_level];
            role_data.skill_2.cool_time =
                role_data.skill_2.cool_time_List[role_data.skill_2.skill_level];
        }
        void Skill_3_Upgrade()
        {
            role_data.skill_3.skill_level++;
            role_data.skill_3.skill_damage =
                role_data.skill_3.skill_damage_List[role_data.skill_3.skill_level];
            role_data.skill_3.cool_time =
                role_data.skill_3.cool_time_List[role_data.skill_3.skill_level];
        }
    }
}

