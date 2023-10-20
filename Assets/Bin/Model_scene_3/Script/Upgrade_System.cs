using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;

public class Upgrade_System : MonoBehaviour
{
    [SerializeField, Header("需要經驗值")]
    private List<float> need_exp;
    [SerializeField, Header("等級文字")]
    private TMP_Text text_level;
    [SerializeField, Header("經驗值物件")]
    private GameObject object_exp;


    private int level;

    private float current_exp;
    private int current_object_exp;

    private void Start()
    {
        level = 1;
        current_exp = 0;
        current_object_exp = 0;
        text_level.text = level.ToString();
        for (int i = 0; i <= 50; i++)
        {
            object_exp.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void Get_exp(float exp)
    {
        current_exp += exp;

        if(current_exp >= need_exp[level-1])
        {
            Level_Up();
        }
        while ( (current_exp / need_exp[level-1] * 50) > (current_object_exp+1) )
        {
            object_exp.transform.GetChild(current_object_exp).gameObject.SetActive(true);
            current_object_exp++;
        }
    }

    private void Level_Up()
    {
        current_exp -= need_exp[level-1];
        level++;
        for(int i = 0;i<= current_object_exp;i++)
        {
            object_exp.transform.GetChild(i).gameObject.SetActive(false);
        }
        current_object_exp = 0;
        text_level.text = level.ToString();
    }

}
