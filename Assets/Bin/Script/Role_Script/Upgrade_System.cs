using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace magic
{
    public class Upgrade_System : MonoBehaviour
    {
        [SerializeField, Header("需要經驗值")]
        private List<float> need_exp;
        [SerializeField, Header("等級文字")]
        private TMP_Text text_level;
        [SerializeField, Header("經驗值物件")]
        private GameObject object_exp;
        [SerializeField, Header("技能升級UI")]
        private GameObject object_upgrade_UI;

        private int role_level;

        private float current_exp;
        private int current_object_exp;
        private void Start()
        {
            role_level = 1;
            current_exp = 0;
            current_object_exp = 0;
            text_level.text = role_level.ToString();
            for (int i = 0; i < object_exp.transform.childCount; i++)
            {
                object_exp.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        public void Get_Exp(float exp)
        {
            current_exp += exp;
            if (current_exp >= need_exp[role_level-1])
            {
                Level_Up();
            }
            while ((current_exp / need_exp[role_level-1] * 50) >= (current_object_exp + 1))
            {
                if (current_object_exp < 49)
                    object_exp.transform.GetChild(current_object_exp).gameObject.SetActive(true);
                current_object_exp++;
            }
        }
        private void Level_Up()
        {
            current_exp -= need_exp[role_level-1];
            role_level++;
            for (int i = 0; i < 49; i++)
            {
                object_exp.transform.GetChild(i).gameObject.SetActive(false);
            }
            current_object_exp = 0;
            text_level.text = role_level.ToString();
            object_upgrade_UI.SetActive(true);
        }

    }
}

