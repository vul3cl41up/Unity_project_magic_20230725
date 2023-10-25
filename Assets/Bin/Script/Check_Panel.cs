using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace magic
{
    public class Check_Panel : MonoBehaviour
    {
        public Skill_Data skill_data;
        [SerializeField, Header("敘述文字物件")]
        private TextMeshProUGUI object_caption;
        [SerializeField, Header("升級面板")]
        private GameObject skill_upgrade_panel;

        private EventSystem eventSystem;
        private GameObject selected;

        private void OnEnable()
        {
            skill_upgrade_panel.GetComponent<CanvasGroup>().interactable = false;
            eventSystem = EventSystem.current;
            object_caption.text = "確定升級" + skill_data.skill_name.ToString()+"?";
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
