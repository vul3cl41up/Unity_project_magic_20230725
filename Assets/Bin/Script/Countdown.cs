using System;
using TMPro;
using UnityEngine;

namespace magic
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField, Header("�������O")]
        GameObject end_panel;
        [SerializeField, Header("�s�����")]
        float live_time = 480;
        [SerializeField, Header("���A���")]
        State_Data state_data;

        TextMeshProUGUI countdown_text;


        private void Start()
        {
            countdown_text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            live_time -= Time.deltaTime;
            if(live_time >0)
            {
                countdown_text.text = string.Format("{0:00}", Mathf.Floor(live_time / 60)) + ":" + string.Format("{0:00}", Mathf.Floor(live_time % 60));
            }
            else
            {
                end_panel.SetActive(true);
                end_panel.GetComponent<End_Panel>().Change_Result_Text("�ӧQ!");
                state_data.pass_stage[state_data.map] = true;
            }
        }
    }

}
