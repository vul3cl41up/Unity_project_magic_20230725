using UnityEngine;

namespace magic
{
    [CreateAssetMenu(fileName = "skill_data_file", menuName = "Data/Skill_Data_File")]
    public class Skill_Data : ScriptableObject
    {
        public Skill_Type skill_type;
        public string skill_name;
        [TextArea]
        public string skill_description;
        public float cool_time;
        public float skill_damage;
        public GameObject skill_prefab;
    }
}

