using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
namespace magic
{
    public class Hp_Control : MonoBehaviour
    {
        [SerializeField]
        Role_Control role_control;

        List<GameObject> hp_list;

        private void Start()
        {
            hp_list = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                hp_list.Add(transform.GetChild(i).gameObject);
            }
        }

        private void Update()
        {
            for (int i = 1; i < hp_list.Count; i++)
            {
                if (role_control.hp / role_control.hpMax*10 >=i)
                {
                    hp_list[i].SetActive(true);
                }
                else
                {
                    hp_list[i].SetActive(false);
                }
            }
            if (role_control.hp / role_control.hpMax <= 0)
                hp_list[0].SetActive(false);
        }
    }
}

