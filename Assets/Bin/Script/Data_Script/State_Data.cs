using System.Collections.Generic;
using UnityEngine;

namespace magic
{
    [CreateAssetMenu(fileName = "state_data_file", menuName = "Data/State_Data_File")]
    public class State_Data : ScriptableObject
    {
        public int bouns_number;
        public List<bool> pass_stage;
        public int map;
    }

}
