using UnityEngine;
using System.Collections.Generic;

namespace magic
{
    [CreateAssetMenu(fileName = "skill_pool_data_file", menuName = "Data/Skill_Pool_Data_File")]
    public class Skill_Pool_Data : ScriptableObject
    {
        public List<Skill_Data> skill_list;

    }
}

