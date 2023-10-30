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
        [SerializeField, Header("�ԭz��r����")]
        private TextMeshProUGUI object_caption;
        [SerializeField, Header("�ޯ��ʶR���O")]
        private GameObject skill_buy_panel;
        [SerializeField, Header("���A���")]
        State_Data state_data;
        [SerializeField, Header("�ޯ�����")]
        private Skill_Pool_Data skill_pool_data;
        [SerializeField, Header("���G���O")]
        GameObject result_panel;

        EventSystem eventSystem;

        GameObject selected;

        private void OnEnable()
        {
            skill_buy_panel.GetComponent<CanvasGroup>().interactable = false;
            eventSystem = EventSystem.current;
            object_caption.text = "�T�w�ʶR" + skill_data.skill_name.ToString() + "?";
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
                result_panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "�ʶR���\";
                enabled = false;

            }
            else
            {
                result_panel.SetActive(true);
                result_panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "�l�B�����A�L�k�ʶR";
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
