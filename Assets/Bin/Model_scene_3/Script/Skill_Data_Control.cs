using UnityEngine;

namespace Model_scene_3
{
    public enum Skill_Type { Common_Attack = 0, Skill_A = 1, Skill_B = 2, Skill_C = 3, Skill_D = 4 }
    public class Skill_Data_Control : MonoBehaviour
    {
        public static void Copy_Skill_Data(Skill_Data copy, Skill_Data origin)
        {
            copy.skill_type = origin.skill_type;
            copy.skill_name = origin.skill_name;
            copy.skill_description = origin.skill_description;
            copy.skill_image = origin.skill_image;
            copy.cool_time = origin.cool_time;
        }
    }
    
}