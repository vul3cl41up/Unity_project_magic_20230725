using System.Collections.Generic;
using UnityEngine;

namespace Model_scene_3
{
    [CreateAssetMenu(fileName = "skill_pool_data", menuName = "Data/Skill_Pool_Data")]
    public class Skill_Pool_Data : ScriptableObject
    {
        public List<Skill_Data> skill_list;
    }
}

