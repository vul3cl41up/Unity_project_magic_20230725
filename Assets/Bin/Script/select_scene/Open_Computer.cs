using Cinemachine;
using UnityEngine;

namespace magic
{
    public class Open_Computer : MonoBehaviour
    {
        [SerializeField, Header("提示UI")]
        GameObject hint_canvas;
        [SerializeField, Header("互動UI")]
        GameObject interact_UI;
        [SerializeField, Header("UI攝影機")]
        CinemachineVirtualCamera UI_camera;

        bool touch = false;
        GameObject player;
        private void Update()
        {
            if(touch && Input.GetButtonDown("Submit"))
            {
                interact_UI.SetActive(true);
                player.GetComponent<Role_Control>().enabled = false;
                player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                UI_camera.Priority = 11;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                player = collision.gameObject;
                touch = true;
                hint_canvas.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                touch = false;
                hint_canvas.SetActive(false);
            }
        }
    }
}

