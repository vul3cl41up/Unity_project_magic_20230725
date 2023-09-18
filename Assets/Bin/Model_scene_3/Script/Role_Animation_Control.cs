using UnityEngine;
namespace Model_scene_3
{
    public class Role_Animation_Control : MonoBehaviour
    {
        Role_Control role_control;

        void Start ()
        {
            role_control = GetComponent<Role_Control>();
        }

        private void Update()
        {
            MoveAndFlip();
        }

        void MoveAndFlip()
        {
            if((int)role_control.Direction_Now < 0)
            {
                transform.rotation = Quaternion.Euler(0,180,0);
            }
            else if((int)role_control.Direction_Now > 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}