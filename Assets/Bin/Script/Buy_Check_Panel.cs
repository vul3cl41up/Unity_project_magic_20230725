using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace magic
{
    public class Buy_Check_Panel : MonoBehaviour
    {
        public Skill_Data skill_data;
        public int skill_pool_index;
        public int siblingIndex;
        [SerializeField, Header("敘述文字物件")]
        private TextMeshProUGUI object_caption;
        [SerializeField, Header("技能購買面板")]
        private GameObject skill_buy_panel;
        [SerializeField, Header("狀態資料")]
        State_Data state_data;
        [SerializeField, Header("技能池資料")]
        private Skill_Pool_Data skill_pool_data;
        [SerializeField, Header("結果面板")]
        GameObject result_panel;

        EventSystem eventSystem;

        GameObject selected;

        private void OnEnable()
        {
            skill_buy_panel.GetComponent<CanvasGroup>().interactable = false;
            eventSystem = EventSystem.current;
            object_caption.text = "確定購買" + skill_data.skill_name.ToString() + "?";
        }
        private void Update()
        {
            selected = eventSystem.currentSelectedGameObject;
            if (selected == null)
            {
                selected = transform.GetChild(0).gameObject;
                eventSystem.SetSelectedGameObject(selected);
            }
            if (Input.GetButtonDown("Cancel"))
            {
                Cancel();
            }
        }

        public void Confirm()
        {
            if(state_data.bouns_number >= skill_data.price)
            {
                skill_pool_data.have[skill_pool_index] = true;
                state_data.bouns_number -= skill_data.price;
                result_panel.SetActive(true);
                eventSystem.SetSelectedGameObject(result_panel.transform.GetChild(0).gameObject);
                result_panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "購買成功";
                enabled = false;

            }
            else
            {
                result_panel.SetActive(true);
                result_panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "餘額不足，無法購買";
                eventSystem.SetSelectedGameObject(result_panel.transform.GetChild(0).gameObject);
                enabled = false;
            }
        }

        public void Result_Confirm()
        {
            result_panel.SetActive(false);
            enabled = true;
            gameObject.SetActive(false);
            skill_buy_panel.GetComponent<CanvasGroup>().interactable = true;
            skill_buy_panel.GetComponent<Skill_Buy_Panel>().Refresh_Pool();
        }

        public void Cancel()
        {
            skill_buy_panel.GetComponent<CanvasGroup>().interactable = true;
            eventSystem.SetSelectedGameObject(skill_buy_panel.transform.GetChild(2).GetChild(siblingIndex).gameObject);
            gameObject.SetActive(false);
        }
    }

}
