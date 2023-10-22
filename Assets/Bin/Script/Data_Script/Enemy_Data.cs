using UnityEngine;

namespace magic
{
    [CreateAssetMenu(fileName = "enemy_data_file", menuName = "Data/Enemy_Data_File")]
    public class Enemy_Data : ScriptableObject
    {
        public float Hp;
        public float move_speed;
        public float attack_damage;
        public float exp;
        public float attack_cool_time;

    }
}

