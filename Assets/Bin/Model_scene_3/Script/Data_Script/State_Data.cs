using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Model_scene_3
{
    [CreateAssetMenu(fileName = "state_data", menuName = "Data/State_Data")]
    public class State_Data : ScriptableObject
    {
        public int current_scene_index;
        public bool is_novice_teaching;

        public Skill_Pool_Data skill_pool;
        public Character_Data character_data;
    }
}