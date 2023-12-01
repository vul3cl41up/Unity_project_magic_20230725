using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace magic
{
    public class Map_Toggle : MonoBehaviour
    {
        [SerializeField, Header("狀態資料")]
        State_Data state_data;
        [SerializeField, Header("下一步")]
        GameObject next;
        [SerializeField, Header("關卡描述")]
        TextMeshProUGUI text_describe;

        GameObject seleted;
        EventSystem eventSystem;

        Toggle[] map_toggle;
        GameObject[] map;


        private void OnEnable()
        {
            map_toggle = new Toggle[transform.childCount];
            map = new GameObject[transform.childCount];
            eventSystem = EventSystem.current; 
            seleted = next;
            eventSystem.SetSelectedGameObject(seleted);
            for(int i = 0;i < state_data.pass_stage.Count-1;i++)
            {
                if (state_data.pass_stage[i])
                {
                    transform.GetChild(i+1).GetComponent<Toggle>().interactable = true;
                    transform.GetChild(i+1).GetChild(0).gameObject.SetActive(false);
                }
            }
            for(int i = 0; i < transform.childCount;i++)
            {
                map_toggle[i] = transform.GetChild(i).GetComponent<Toggle>();
                map[i] = transform.GetChild(i).gameObject;
            }

        }
        private void Update()
        {
            seleted = eventSystem.currentSelectedGameObject;
            if (seleted == map[0])
            {
                text_describe.text = "關卡一";
            }
            else if(seleted == map[1])
            {
                text_describe.text = "關卡二";
            }
            else if (seleted == map[2])
            {
                text_describe.text = "關卡三";
            }
        }
        public void Change_Stage()
        {
            if (map_toggle[0].isOn)
            {
                state_data.map = 0;
            }
            if (map_toggle[1].isOn)
            {
                state_data.map = 1;
            }
            if (map_toggle[2].isOn)
            {
                state_data.map = 2;
            }
        }
    }
}

