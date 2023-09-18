using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
        public Character_Data character_data;
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
                Skill_Data selected_skill_data = selected.GetComponent<Skill_Data_Control>().this_skill_data;
                title.text = selected_skill_data.skill_name;
                description.text = selected_skill_data.skill_description;
            }

        }

        public void Choose()
        {
            if (Input.GetButtonDown("Submit") && selected.CompareTag("Skill_button") && choose_count <= 4)
            {
                GameObject new_skill_selected_slot = Instantiate(skill_selected_slot, Vector3.zero, Quaternion.identity, skill_grid.transform);
                new_skill_selected_slot.GetComponent<Skill_Data_Control>().this_skill_data = selected.GetComponent<Skill_Data_Control>().this_skill_data;
                selected.SetActive(false);
                choose_count++;
            }
            
        }

        public void Cancel()
        {
            if (Input.GetButtonDown("Submit") && selected.CompareTag("Skill_selected_button") && choose_count > 0)
            {
                int index = selected.GetComponent<Skill_Data_Control>().this_skill_data.skill_index;
                skill_pool.transform.GetChild(index).gameObject.SetActive(true);
                Destroy(selected);
                choose_count--;
            }
            if(Input.GetButtonDown("Cancel") && choose_count > 0)
            {
                selected = skill_grid.transform.GetChild(choose_count).gameObject;
                int index = selected.GetComponent<Skill_Data_Control>().this_skill_data.skill_index;
                skill_pool.transform.GetChild(index).gameObject.SetActive(true);
                Destroy(selected);
                choose_count--;
            }
        }

        public void StartGame()
        {
            if(choose_count == 4)
            {
                character_data.skill_1 = skill_grid.transform.GetChild(1).gameObject.GetComponent<Skill_Data_Control>().this_skill_data;
                character_data.skill_2 = skill_grid.transform.GetChild(2).gameObject.GetComponent<Skill_Data_Control>().this_skill_data;
                character_data.skill_3 = skill_grid.transform.GetChild(3).gameObject.GetComponent<Skill_Data_Control>().this_skill_data;
                character_data.skill_4 = skill_grid.transform.GetChild(4).gameObject.GetComponent<Skill_Data_Control>().this_skill_data;
                SceneManager.LoadScene(2);
            }
        }    
    }
}