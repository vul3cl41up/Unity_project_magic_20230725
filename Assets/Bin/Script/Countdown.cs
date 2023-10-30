using System;
using TMPro;
using UnityEngine;

namespace magic
{
    public class Countdown : MonoBehaviour
    {
        [SerializeField, Header("結束面板")]
        GameObject end_panel;
        [SerializeField, Header("存活秒數")]
        float live_time = 480;
        [SerializeField, Header("狀態資料")]
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
                end_panel.GetComponent<End_Panel>().Change_Result_Text("勝利!");
                state_data.pass_stage[state_data.map] = true;
            }
        }
    }

}
