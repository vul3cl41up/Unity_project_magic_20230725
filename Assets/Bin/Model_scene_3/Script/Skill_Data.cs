using UnityEngine;

namespace Model_scene_3
{
    [CreateAssetMenu(fileName = "Skill_Data", menuName = "Data/Skill_Data")]
    public class Skill_Data : ScriptableObject
    {
        public int skill_index;
        public string skill_name;
        [TextArea]
        public string skill_description;
    }
}

