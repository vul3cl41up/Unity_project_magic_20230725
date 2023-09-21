using Model_scene_3;
using System.Collections.Generic;
using UnityEngine;

namespace Model_scene_3
{
    public class State_Data_Control : MonoBehaviour
    {
        public static void Copy_State_Data(State_Data copy, State_Data origin)
        {
            copy.current_scene_index = origin.current_scene_index;
            copy.is_novice_teaching = origin.is_novice_teaching;

            for (int i = 0; i < origin.skill_pool.skill_list.Count; i++)
            {
                Skill_Data_Control.Copy_Skill_Data(copy.skill_pool.skill_list[i], origin.skill_pool.skill_list[i]);
            }

            Character_Data_Control.Copy_Character_Data(copy.character_data, origin.character_data);

        }
    }
}
