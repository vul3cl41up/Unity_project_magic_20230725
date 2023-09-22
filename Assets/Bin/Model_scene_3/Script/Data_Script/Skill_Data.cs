using UnityEngine;

namespace Model_scene_3
{
    [CreateAssetMenu(fileName = "Skill_Data", menuName = "Data/Skill_Data")]
    public class Skill_Data : ScriptableObject
    {
        public Skill_Type skill_type;
        public string skill_name;
        [TextArea]
        public string skill_description;
        public Sprite skill_image;
        public float cool_time;
        public GameObject skill_prefab;
    }
}

