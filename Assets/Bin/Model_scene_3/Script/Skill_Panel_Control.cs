using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace Model_scene_3
{

    public class Skill_Panel_Control : MonoBehaviour
    {
        public GameObject skill_pool;
        public Skill_Pool_Data skill_pool_data;
        public GameObject skill_slot;
        public GameObject skill_grid;
        public GameObject skill_selected_slot;
        public TMP_Text title;
        public TMP_Text description;
        public GameObject selected;
        EventSystem eventSystem;

        int choose_count = 0;

        private void Start()
        {
            eventSystem = EventSystem.current;
            for(int i = 0;i < skill_pool_data.skill_list.Count;i++)
            {
                GameObject new_skill_slot= Instantiate(skill_slot, Vector3.zero, Quaternion.identity, skill_pool.transform);
                new_skill_slot.GetComponent<Skill_Data_Control>().this_skill_data = skill_pool_data.skill_list[i];
                if(i == 0) eventSystem.firstSelectedGameObject = new_skill_slot;
            }
        }

        private void Update()
        {
            selected = eventSystem.currentSelectedGameObject;
            if (selected == null)
            {
                selected = skill_grid.transform.GetChild(0).gameObject;
                eventSystem.SetSelectedGameObject(selected);
            }
            if(selected.active == false)
            {
                selected = skill_grid.transform.GetChild(choose_count).gameObject;
                eventSystem.SetSelectedGameObject(selected);
            }
            if (selected.CompareTag("Skill_button"))
            {
                DisplayDescription();
                Choose();
            }
            if (selected.CompareTag("Skill_selected_button"))
            {
                DisplayDescription();
            }

        }

        private void DisplayDescription()
        {
            Skill_Data selected_skill_data = selected.GetComponent<Skill_Data_Control>().this_skill_data;
            title.text = selected_skill_data.skill_name;
            description.text = selected_skill_data.skill_description;
        }

        public void Choose()
        {
            if (Input.GetKeyDown(KeyCode.Z) && choose_count <= 4)
            {
                GameObject new_skill_selected_slot = Instantiate(skill_selected_slot, Vector3.zero, Quaternion.identity, skill_grid.transform);
                new_skill_selected_slot.GetComponent<Skill_Data_Control>().this_skill_data = selected.GetComponent<Skill_Data_Control>().this_skill_data;
                selected.SetActive(false);
                choose_count++;
            }
            
        }


    }
}