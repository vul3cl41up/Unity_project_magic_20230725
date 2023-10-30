using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace magic
{
    public class Interact_UI : MonoBehaviour
    {
        [SerializeField, Header("UIÄá¼v¾÷")]
        CinemachineVirtualCamera UI_camera;
        [SerializeField, Header("Stage")]
        GameObject stage;

        GameObject player;
        GameObject seleted;

        EventSystem eventSystem;

        private void OnEnable()
        {
            eventSystem = EventSystem.current;
            seleted = stage;
            eventSystem.SetSelectedGameObject(seleted);
        }

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }
        private void Update()
        {
            if(Input.GetButtonDown("Cancel"))
            {
                gameObject.SetActive(false);

            }
            if(seleted == null)
            {
                seleted = stage;
                eventSystem.SetSelectedGameObject(seleted);
            }
        }
        private void OnDisable()
        {
            UI_camera.Priority = 9;
            if(player)player.GetComponent<Role_Control>().enabled = true;
        }
    }
}

