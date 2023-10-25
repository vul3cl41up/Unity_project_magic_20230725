using UnityEngine;

public class Game_UI : MonoBehaviour
{
    [SerializeField, Header("選單")]
    private GameObject menu;

    void Update()
    {
        if(menu)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                menu.SetActive(!menu.activeSelf);
            }
        }
    }
}
