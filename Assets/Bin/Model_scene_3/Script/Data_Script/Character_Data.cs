using UnityEngine;

namespace Model_scene_3
{
    [CreateAssetMenu(fileName = "Character_Data", menuName = "Data/Character_Data")]
    public class Character_Data : ScriptableObject
    {
        public float blood;
        public float blood_now;
        public float magic;
        public float magic_now;

        public float move_speed;
        public float attack_speed;
        public float attack_damage;

        public Skill_Data common_attack;
        public Skill_Data skill_1;
        public Skill_Data skill_2;
        public Skill_Data skill_3;
        public Skill_Data skill_4;
    }
}