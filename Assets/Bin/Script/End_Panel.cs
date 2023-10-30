using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace magic
{
    public class End_Panel : MonoBehaviour
    {
        private TextMeshProUGUI result_text;

        private void Awake()
        {
            result_text = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            Time.timeScale = 0f;

            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
        }
        private void OnDisable()
        {
            Time.timeScale = 1f;
        }

        public void Change_Result_Text(string result)
        {
            result_text.text = result;
        }
    }
}

