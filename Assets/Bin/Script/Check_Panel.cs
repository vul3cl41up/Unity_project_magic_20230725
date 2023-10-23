using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace magic
{
    public class Check_Panel : MonoBehaviour
    {
        public Skill_Data skill_data;
        [SerializeField, Header("±Ô­z¤å¦rª«¥ó")]
        private TextMeshProUGUI object_caption;
        [SerializeField, Header("¤É¯Å­±ªO")]
        private GameObject skill_upgrade_panel;

        private EventSystem eventSystem;
        private GameObject selected;

        private void OnEnable()
        {
            skill_upgrade_panel.GetComponent<CanvasGroup>().interactable = false;
            eventSystem = EventSystem.current;
            print("object_caption.name:" + object_caption.name);
            print("skill_data:" + skill_data.name);
            print("skill_data.skill_name:" + skill_data.skill_name);
            print("½T©w¤É¯Å" + skill_data.skill_name.ToString() + "?");
            object_caption.text = "½T©w¤É¯Å" + skill_data.skill_name.ToString()+"?";
        }

        private void Update()
        {
            selected = eventSystem.currentSelectedGameObject;
            if (selected == null)
            {
                selected = transform.GetChild(0).gameObject;
                eventSystem.SetSelectedGameObject(selected);
            }
            if(Input.GetButtonDown("Cancel"))
            {
                Cancel();
            }
        }

        public void Confirm()
        {
            skill_upgrade_panel.GetComponent<CanvasGroup>().interactable = true;
            skill_upgrade_panel.SetActive(false);

            skill_data.skill_level++;
            skill_data.cool_time = skill_data.cool_time_List[skill_data.skill_level];
            skill_data.skill_damage = skill_data.skill_damage_List[skill_data.skill_level];
            gameObject.SetActive(false);

        }
        public void Cancel()
        {
            skill_upgrade_panel.GetComponent<CanvasGroup>().interactable = true;
            eventSystem.SetSelectedGameObject(skill_upgrade_panel.transform.GetChild(0).gameObject); 
            gameObject.SetActive(false);
        }
    }

}
