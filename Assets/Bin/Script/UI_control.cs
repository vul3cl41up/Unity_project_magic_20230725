using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_control
    : MonoBehaviour
{
    public GameObject BagPanel;
    public GameObject menu;

    void Start()
    {
        BagPanel.SetActive(false);
        menu.SetActive(false);
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
            BagPanel.SetActive(!BagPanel.activeSelf);
        if (Input.GetKeyDown(KeyCode.Escape))
            menu.SetActive(!menu.activeSelf);
    }
}
