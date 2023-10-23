using Model_scene_3;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace magic
{
    public class Skill_Upgrade : MonoBehaviour
    {
        [SerializeField, Header("玩家資料")]
        private Role_Data role_data;
        [SerializeField, Header("生成預置物")]
        private GameObject skill_upgrade_slot;
        [SerializeField, Header("攻擊系統")]
        private Attack_System attack_system;
        [SerializeField, Header("技能等級上限")]
        private int skill_level_max = 5;
        [SerializeField, Header("確認面板")]
        private GameObject check_panel;

        private EventSystem eventSystem;
        private GameObject selected;

        private void OnEnable()
        {
            eventSystem = EventSystem.current;
            Time.timeScale = 0;
            Generate_Slot();
            attack_system.enabled = false;
            eventSystem.SetSelectedGameObject(transform.GetChild(0).gameObject);
        }

        private void Update()
        {
            selected = eventSystem.currentSelectedGameObject;
            if (selected == null)
            {
                selected = transform.GetChild(0).gameObject;
                eventSystem.SetSelectedGameObject(selected);
            }
            
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
            if(attack_system)attack_system.enabled = true;
            Destroy_Slot();
        }

        void Generate_Slot()
        {
            if(role_data.common_attack.skill_level < skill_level_max-1)
            {
                GameObject new_slot = Instantiate(skill_upgrade_slot, Vector3.zero, Quaternion.identity, transform);
                Skill_Data skill_data = new_slot.GetComponent<Skill_Upgrade_Slot>().skill_data = role_data.common_attack;
                new_slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skill_data.skill_name;
                int skill_level = skill_data.skill_level;
                new_slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = skill_data.skill_upgrade_description[skill_level];
                new_slot.GetComponent<Button>().onClick.AddListener(Choose);
            }


            if (role_data.skill_1.skill_level < skill_level_max - 1)
            {
                GameObject new_slot = Instantiate(skill_upgrade_slot, Vector3.zero, Quaternion.identity, transform);
                Skill_Data skill_data = new_slot.GetComponent<Skill_Upgrade_Slot>().skill_data = role_data.skill_1;
                new_slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skill_data.skill_name;
                int skill_level = skill_data.skill_level;
                new_slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = skill_data.skill_upgrade_description[skill_level];
                new_slot.GetComponent<Button>().onClick.AddListener(Choose);
            }

            if (role_data.skill_2.skill_level < skill_level_max - 1)
            {
                GameObject new_slot = Instantiate(skill_upgrade_slot, Vector3.zero, Quaternion.identity, transform);
                Skill_Data skill_data = new_slot.GetComponent<Skill_Upgrade_Slot>().skill_data = role_data.skill_2;
                new_slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skill_data.skill_name;
                int skill_level = skill_data.skill_level;
                new_slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = skill_data.skill_upgrade_description[skill_level];
                new_slot.GetComponent<Button>().onClick.AddListener(Choose);
            }
            if (role_data.skill_3.skill_level < skill_level_max - 1)
            {
                GameObject new_slot = Instantiate(skill_upgrade_slot, Vector3.zero, Quaternion.identity, transform);
                Skill_Data skill_data = new_slot.GetComponent<Skill_Upgrade_Slot>().skill_data = role_data.skill_3;
                new_slot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = skill_data.skill_name;
                int skill_level = skill_data.skill_level;
                new_slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = skill_data.skill_upgrade_description[skill_level];
                new_slot.GetComponent<Button>().onClick.AddListener(Choose);
            }
            eventSystem.firstSelectedGameObject = transform.GetChild(0).gameObject;
        }

        void Destroy_Slot()
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        public void Choose()
        {
            check_panel.GetComponent<Check_Panel>().skill_data = selected.GetComponent<Skill_Upgrade_Slot>().skill_data;
            eventSystem.SetSelectedGameObject(check_panel.transform.GetChild(0).gameObject);
            check_panel.SetActive(true);
        }
    }

}
