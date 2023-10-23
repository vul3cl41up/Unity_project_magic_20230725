using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace magic
{
    public class Skill_Select_Panel : MonoBehaviour
    {
        [SerializeField, Header("生成父物件")]
        private GameObject skill_pool;
        [SerializeField, Header("技能格")]
        private GameObject skill_slot;
        [SerializeField, Header("選取父物件")]
        private GameObject skill_grid;
        [SerializeField, Header("選取技能格")]
        private GameObject skill_selected_slot;
        [SerializeField, Header("技能標題")]
        private TMP_Text title;
        [SerializeField, Header("技能描述")]
        private TMP_Text description;
        [SerializeField, Header("技能池資料")]
        private Skill_Pool_Data skill_pool_data;
        [SerializeField, Header("角色資料")]
        private Role_Data role_data;

        EventSystem eventSystem;
        private GameObject selected;

        int choose_count;

        private void Start()
        {
            choose_count = 0;
            eventSystem = EventSystem.current;

            for (int i = 1; i < skill_pool_data.skill_list.Count; i++)
            {
                GameObject new_skill_slot = Instantiate(skill_slot, Vector3.zero, Quaternion.identity, skill_pool.transform);
                new_skill_slot.GetComponent<Skill_Slot>().skill_data = skill_pool_data.skill_list[i];
                if (i == 1) eventSystem.firstSelectedGameObject = new_skill_slot;
            }
        }
        private void Update()
        {
            selected = eventSystem.currentSelectedGameObject;
            DisplayDescription();
            Choose();
            Cancel();
        }
        private void DisplayDescription()
        {
            if (selected == null)
            {
                selected = skill_grid.transform.GetChild(choose_count).gameObject;
                eventSystem.SetSelectedGameObject(selected);
            }
            if (selected.activeSelf == false)
            {
                selected = skill_grid.transform.GetChild(choose_count).gameObject;
                eventSystem.SetSelectedGameObject(selected);
            }
            if (selected.CompareTag("Skill_button") || selected.CompareTag("Skill_selected_button") || selected.CompareTag("Common_attack"))
            {
                Skill_Data selected_skill_data = selected.GetComponent<Skill_Slot>().skill_data;
                title.text = selected_skill_data.skill_name;
                description.text = selected_skill_data.skill_description;
            }
        }

        private void Choose()
        {
            if (Input.GetButtonDown("Submit") && selected.CompareTag("Skill_button") && choose_count <= 2)
            {
                GameObject new_skill_selected_slot = Instantiate(skill_selected_slot, Vector3.zero, Quaternion.identity, skill_grid.transform);
                new_skill_selected_slot.GetComponent<Skill_Slot>().skill_data = selected.GetComponent<Skill_Slot>().skill_data;
                selected.SetActive(false);
                choose_count++;
            }
        }
        private void Cancel()
        {
            if (Input.GetButtonDown("Submit") && selected.CompareTag("Skill_selected_button") && choose_count > 0)
            {
                int index = (int)selected.GetComponent<Skill_Slot>().skill_data.skill_type;
                skill_pool.transform.GetChild(index - 1).gameObject.SetActive(true);
                Destroy(selected);
                choose_count--;
            }
            if (Input.GetButtonDown("Cancel") && choose_count > 0)
            {
                selected = skill_grid.transform.GetChild(choose_count).gameObject;
                int index = (int)selected.GetComponent<Skill_Slot>().skill_data.skill_type;
                skill_pool.transform.GetChild(index - 1).gameObject.SetActive(true);
                Destroy(selected);
                choose_count--;
            }
        }

        public void StartGame()
        {
            if (choose_count == 3)
            {
                role_data.skill_1 = skill_grid.transform.GetChild(1).gameObject.GetComponent<Skill_Slot>().skill_data;
                role_data.skill_2 = skill_grid.transform.GetChild(2).gameObject.GetComponent<Skill_Slot>().skill_data;
                role_data.skill_3 = skill_grid.transform.GetChild(3).gameObject.GetComponent<Skill_Slot>().skill_data;
                Scene_control.Model_3_Scene();
            }
        }
    }

}
