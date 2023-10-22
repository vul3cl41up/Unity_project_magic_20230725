using System.Collections.Generic;
using UnityEngine;

namespace magic
{
    [CreateAssetMenu(fileName = "role_data_file", menuName = "Data/Role_Data_File")]
    public class Role_Data : ScriptableObject
    {
        public float move_speed;

        public float Hp;

        public Skill_Data common_attack;
        public Skill_Data skill_1;
        public Skill_Data skill_2;
        public Skill_Data skill_3;
    }
}

