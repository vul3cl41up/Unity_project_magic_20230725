using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using System;

namespace Model_scene_3
{

    public class Skill_Panel_Control : MonoBehaviour
    {
        public GameObject skill_pool;
        public GameObject skill_slot;
        public GameObject skill_grid;
        public GameObject skill_selected_slot;

        public TMP_Text title;
        public TMP_Text description;

        public State_Data init_state_data;
        public State_Data current_state_data;

        EventSystem eventSystem;
        public GameObject selected;

        int choose_count = 0;

        private void Start()
        {
            eventSystem = EventSystem.current;

            State_Data_Control.Copy_State_Data(current_state_data, init_state_data);

            for(int i = 1;i < current_state_data.skill_pool.skill_list.Count;i++)
            {
                GameObject new_skill_slot= Instantiate(skill_slot, Vector3.zero, Quaternion.identity, skill_pool.transform);
                new_skill_slot.GetComponent<Skill_Slot_Control>().skill_data = current_state_data.skill_pool.skill_list[i];
                new_skill_slot.GetComponent<Image>().sprite = current_state_data.skill_pool.skill_list[i].skill_image;
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
                Skill_Data selected_skill_data = selected.GetComponent<Skill_Slot_Control>().skill_data;
                title.text = selected_skill_data.skill_name;
                description.text = selected_skill_data.skill_description;
            }

        }

        public void Choose()
        {
            if (Input.GetButtonDown("Submit") && selected.CompareTag("Skill_button") && choose_count <= 4)
            {
                GameObject new_skill_selected_slot = Instantiate(skill_selected_slot, Vector3.zero, Quaternion.identity, skill_grid.transform);
                new_skill_selected_slot.GetComponent<Skill_Slot_Control>().skill_data = selected.GetComponent<Skill_Slot_Control>().skill_data;
                new_skill_selected_slot.GetComponent<Image>().sprite = selected.GetComponent<Skill_Slot_Control>().skill_data.skill_image;
                selected.SetActive(false);
                choose_count++;
            }
            
        }

        public void Cancel()
        {
            if (Input.GetButtonDown("Submit") && selected.CompareTag("Skill_selected_button") && choose_count > 0)
            {
                int index = (int)selected.GetComponent<Skill_Slot_Control>().skill_data.skill_type;
                skill_pool.transform.GetChild(index-1).gameObject.SetActive(true);
                Destroy(selected);
                choose_count--;
            }
            if(Input.GetButtonDown("Cancel") && choose_count > 0)
            {
                selected = skill_grid.transform.GetChild(choose_count).gameObject;
                int index = (int)selected.GetComponent<Skill_Slot_Control>().skill_data.skill_type;
                skill_pool.transform.GetChild(index-1).gameObject.SetActive(true);
                Destroy(selected);
                choose_count--;
            }
        }

        public void StartGame()
        {
            if(choose_count == 4)
            {
                current_state_data.character_data.skill_1 = skill_grid.transform.GetChild(1).gameObject.GetComponent<Skill_Slot_Control>().skill_data;
                current_state_data.character_data.skill_2 = skill_grid.transform.GetChild(2).gameObject.GetComponent<Skill_Slot_Control>().skill_data;
                current_state_data.character_data.skill_3 = skill_grid.transform.GetChild(3).gameObject.GetComponent<Skill_Slot_Control>().skill_data;
                current_state_data.character_data.skill_4 = skill_grid.transform.GetChild(4).gameObject.GetComponent<Skill_Slot_Control>().skill_data;
                Scene_control.Model_3_Scene();
            }
        }    
    }
}