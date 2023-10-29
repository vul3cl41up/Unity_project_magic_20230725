using TMPro;
using UnityEngine;

namespace magic
{
    public class Pick_Up : MonoBehaviour
    {
        [SerializeField, Header("數量文字")]
        TextMeshProUGUI number_text;
        [SerializeField, Header("狀態資料")]
        State_Data state_data;

        int bonus_number;

        private void Start()
        {
            bonus_number = 0;
        }

        public void get_bonus(int amount)
        {
            bonus_number += amount;
            number_text.text = bonus_number.ToString();
        }
        private void OnDisable()
        {
            state_data.bouns_number += bonus_number;
        }
    }

}
