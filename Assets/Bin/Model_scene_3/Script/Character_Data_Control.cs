using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Model_scene_3
{
    public class Character_Data_Control : MonoBehaviour
    {
        public static void Copy_Character_Data(Character_Data copy, Character_Data origin)
        {
            copy.blood = origin.blood;
            copy.blood_now = origin.blood_now;
            copy.magic = origin.magic;
            copy.magic_now = origin.magic_now;
            copy.move_speed = origin.move_speed;
            copy.attack_speed = origin.attack_speed;
            copy.attack_damage = origin.attack_damage;
        }
    }
}