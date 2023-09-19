
using UnityEngine;

namespace Model_scene_3
{
    [CreateAssetMenu(fileName = "Enemy_Data", menuName = "Data/Enemy_Data")]
    public class Enemy_Data : ScriptableObject
    {
        public float blood;
        public float blood_now;

        public float move_speed;
        public float attack_speed;

        public float attack_damage;

    }
}