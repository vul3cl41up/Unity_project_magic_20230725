using UnityEngine;

namespace magic
{
    public class Role_Attack : MonoBehaviour
    {
        private Role_Movement role_movement;

        Transform up;
        Transform left_up;
        Transform left;
        Transform left_down;
        Transform down;
        Transform right_down;
        Transform right;
        Transform right_up;
        public GameObject direction_now { get; private set;}

        private void Start()
        {
            role_movement = GetComponent<Role_Movement>();
            #region 取得方向物件
            up = transform.GetChild(0).transform;
            left_up = transform.GetChild(1).gameObject;
            left = transform.GetChild(2).gameObject;
            left_down = transform.GetChild(3).gameObject;
            down = transform.GetChild(4).gameObject;
            right_down = transform.GetChild(5).gameObject;
            right = transform.GetChild(6).gameObject;
            right_up = transform.GetChild(7).gameObject;
            direction_now = right;
            #endregion

        }


        private void Change_Direction()
        {
            if (role_movement.move_input.x > 0)
            {
                if (role_movement.move_input.y > 0)
                {
                    if (direction_now != right_up)
                    {

                    }
                }
                else if (role_movement.move_input.y < 0)
                {

                }
                else
                {

                }    
            }
            else if (role_movement.move_input.x < 0)
            {
                if (role_movement.move_input.y > 0)
                {

                }
                else if (role_movement.move_input.y < 0)
                {

                }
                else
                {

                }
            }
            else
            {
                if (role_movement.move_input.y > 0)
                {

                }
                else if (role_movement.move_input.y < 0)
                {

                }
            }
        }
    }
}

