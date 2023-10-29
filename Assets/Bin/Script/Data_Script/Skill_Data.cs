using System.Collections.Generic;
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
        public int skill_level;
        public List<float> cool_time_List;
        public List<float> skill_damage_List;
        [TextArea]
        public List<string> skill_upgrade_description;
        public float stop_time;
        public List<float> stop_time_List;
        public float scale;
        public List<float> scale_List;
        public int times;
        public List<int> times_List;
        public float last_time;
        public List<float> last_time_List;
        public Sprite skill_image;
    }
}

