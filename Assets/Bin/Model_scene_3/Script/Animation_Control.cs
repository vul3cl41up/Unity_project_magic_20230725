﻿using System.Collections;
using UnityEngine;

namespace Model_scene_3
{
    public class Animation_Control : MonoBehaviour
    {
        private Role_Control role_control;
        [SerializeField]
        private Skill_Type skill_type;

        private void Start()
        {
            role_control = GetComponentInParent<Role_Control>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            StartCoroutine(Start_Attack(collision));
        }

        void End()
        {
            gameObject.SetActive(false);
        }

        IEnumerator Start_Attack(Collider2D collision)
        {
            yield return new WaitForSeconds(0.2f);
            collision.gameObject.GetComponent<Enemy_Base_Control>().Take_Damage(role_control, skill_type);
        }
    }

}
