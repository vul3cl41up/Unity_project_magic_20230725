using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace magic
{
    public class Skill_Buy_Panel : MonoBehaviour
    {
        [SerializeField, Header("生成池")]
        private GameObject skill_buy_pool;
        [SerializeField, Header("技能格")]
        private GameObject skill_buy_slot;
        [SerializeField, Header("技能描述面板")]
        private GameObject skill_describe_panel;
        [SerializeField, Header("技能池資料")]
        private Skill_Pool_Data skill_pool_data;
        [SerializeField, Header("狀態資料")]
        State_Data state_data;
        [SerializeField, Header("返回")]
        GameObject return_object;
        [SerializeField, Header("確認面板")]
        private GameObject check_panel;
        [SerializeField, Header("獎勵道具數量文字")]
        TextMeshProUGUI bonus_text;

        EventSystem eventSystem;
        private GameObject selected;
        bool first = true;
        Vector3 origin_pos;
        RectTransform skill_describe_panel_transform;

        TextMeshProUGUI title;
        TextMeshProUGUI describe;
        TextMeshProUGUI price;


        private void Start()
        {
            eventSystem = EventSystem.current;
            skill_describe_panel_transform = skill_describe_panel.GetComponent<RectTransform>();
            origin_pos = new Vector3(600, 600, 0);
            title = skill_describe_panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            describe = skill_describe_panel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            price = skill_describe_panel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

            bonus_text.text = state_data.bouns_number.ToString();
            for (int i = 1; i < skill_pool_data.skill_list.Count; i++)
            {
                if (!skill_pool_data.have[i])
                {
                    GameObject new_skill_slot = Instantiate(skill_buy_slot, Vector3.zero, Quaternion.identity, skill_buy_pool.transform);
                    new_skill_slot.GetComponent<Skill_Buy_Slot>().skill_data = skill_pool_data.skill_list[i];
                    new_skill_slot.GetComponent<Skill_Buy_Slot>().skill_pool_index = i;
                    new_skill_slot.GetComponent<Button>().onClick.AddListener(Choose);
                    if (first) 
                    {
                        eventSystem.firstSelectedGameObject = new_skill_slot; 
                        first = false;
                    }
                }
            }
        }
        private void Update()
        {
            bonus_text.text = state_data.bouns_number.ToString();
            selected = eventSystem.currentSelectedGameObject;
            if(selected == null)
            {
                selected = return_object;
                eventSystem.SetSelectedGameObject(selected);
            }
            DisplayDescription();
        }

        public void Refresh_Pool()
        {
            bool first_one = true;
            for(int i = 0; i < skill_buy_pool.transform.childCount; i++)
            {
                Destroy(skill_buy_pool.transform.GetChild(i).gameObject);
            }
            for (int i = 1; i < skill_pool_data.skill_list.Count; i++)
            {
                if (!skill_pool_data.have[i])
                {
                    GameObject new_skill_slot = Instantiate(skill_buy_slot, Vector3.zero, Quaternion.identity, skill_buy_pool.transform);
                    new_skill_slot.GetComponent<Skill_Buy_Slot>().skill_data = skill_pool_data.skill_list[i];
                    new_skill_slot.GetComponent<Skill_Buy_Slot>().skill_pool_index = i;
                    new_skill_slot.GetComponent<Button>().onClick.AddListener(Choose);
                    if (first_one)
                    {
                        eventSystem.SetSelectedGameObject(new_skill_slot);
                        first_one = false;
                    }
                }
            }
        }
        void DisplayDescription()
        {
            if(selected.CompareTag("Skill_buy_button"))
            {
                skill_describe_panel.SetActive(true);
                switch(selected.transform.GetSiblingIndex()%4)
                {
                    case 0:
                        skill_describe_panel_transform.position = origin_pos;
                        break;
                    case 1:
                    case 3:
                        skill_describe_panel_transform.position = origin_pos + new Vector3(350, 0, 0) ;
                        break;
                    case 2:
                        skill_describe_panel_transform.position = origin_pos + new Vector3(700, 0, 0);
                        break;
                }
                switch (selected.transform.GetSiblingIndex() / 4)
                {
                    case 1:
                        skill_describe_panel_transform.position += new Vector3(0, -240, 0);
                        break;
                    case 2:
                        skill_describe_panel_transform.position += new Vector3(0, -480, 0);
                        break;
                }

                title.text = selected.GetComponent<Skill_Buy_Slot>().skill_data.skill_name.ToString();
                describe.text = selected.GetComponent<Skill_Buy_Slot>().skill_data.skill_description.ToString();
                price.text =  selected.GetComponent<Skill_Buy_Slot>().skill_data.price.ToString();
            }
            else
            {
                skill_describe_panel.SetActive(false);
            }
        }
        public void Choose()
        {
            check_panel.GetComponent<Buy_Check_Panel>().skill_data = selected.GetComponent<Skill_Buy_Slot>().skill_data;
            check_panel.GetComponent<Buy_Check_Panel>().skill_pool_index = selected.GetComponent<Skill_Buy_Slot>().skill_pool_index;
            check_panel.GetComponent<Buy_Check_Panel>().siblingIndex = selected.transform.GetSiblingIndex();
            eventSystem.SetSelectedGameObject(check_panel.transform.GetChild(0).gameObject);
            check_panel.SetActive(true);
        }
    }
}


