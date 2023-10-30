using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace magic
{
    public class Map_Toggle : MonoBehaviour
    {
        [SerializeField, Header("ª¬ºA¸ê®Æ")]
        State_Data state_data;

        GameObject seleted;
        EventSystem eventSystem;

        Toggle[] map_toggle;


        private void OnEnable()
        {
            map_toggle = new Toggle[transform.childCount];
            eventSystem = EventSystem.current; 
            seleted = transform.GetChild(0).gameObject;
            eventSystem.SetSelectedGameObject(seleted);
            for(int i = 1;i < state_data.pass_stage.Count;i++)
            {
                if (state_data.pass_stage[i])
                {
                    transform.GetChild(i).GetComponent<Toggle>().interactable = true;
                    transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                }
            }
            for(int i = 0; i < transform.childCount;i++)
            {
                map_toggle[i] = transform.GetChild(i).GetComponent<Toggle>();
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

