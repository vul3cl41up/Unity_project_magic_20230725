using UnityEngine;

namespace Model_scene_3
{
    public class Animation_Control : MonoBehaviour
    {
        [SerializeField]
        private Character_Data character_data;

        

        void End()
        {
            gameObject.GetComponentInParent<Role_Control>().can_attack = true;
            gameObject.GetComponentInParent<Role_Control>().is_attack = false;
            gameObject.SetActive(false);
        }
    }

}
