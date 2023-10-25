using UnityEngine;
using UnityEngine.EventSystems;

namespace magic
{
    public class Menu : MonoBehaviour
    {
        bool is_stop = false;
        private void OnEnable()
        {
            if(Time.timeScale == 0) is_stop = true;
            else is_stop = false;
            Time.timeScale = 0;

            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
        }

        private void OnDisable()
        {
            if(!is_stop)
                Time.timeScale = 1f;
        }

    }
}

