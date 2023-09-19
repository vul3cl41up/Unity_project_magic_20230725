using UnityEngine;
using System;

namespace Model_scene_3
{
    public class Role_Animation_Control : MonoBehaviour
    {
        Role_Control role_control;
        SpriteRenderer sprite_renderer;

        GameObject up;
        GameObject left_up;
        GameObject left;
        GameObject left_down;
        GameObject down;
        GameObject right_down;
        GameObject right;
        GameObject right_up;
        GameObject direction_now;

        Animator animator;


        void Start ()
        {
            role_control = GetComponent<Role_Control>();
            sprite_renderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            up = transform.GetChild(0).gameObject;
            left_up = transform.GetChild(1).gameObject;
            left = transform.GetChild(2).gameObject;
            left_down = transform.GetChild(3).gameObject;
            down = transform.GetChild(4).gameObject;
            right_down = transform.GetChild(5).gameObject;
            right = transform.GetChild(6).gameObject;
            right_up = transform.GetChild(7).gameObject;
            direction_now = right;

            role_control.Common_Attack_Received += On_Common_Attack;
        }

        private void Update()
        {
            MoveAndFlip();
            Change_Direction();
        }

        void Change_Direction()
        {
            if(role_control.Direction_Now == Role_Control.Direction.Up)
            {
                if(direction_now != up)
                {
                    direction_now.SetActive(false);
                    direction_now = up;
                    direction_now.SetActive(true);
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Left_Up)
            {
                if (direction_now != left_up)
                {
                    direction_now.SetActive(false);
                    direction_now = left_up;
                    direction_now.SetActive(true);
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Left)
            {
                if (direction_now != left)
                {
                    direction_now.SetActive(false);
                    direction_now = left;
                    direction_now.SetActive(true);
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Left_Down)
            {
                if (direction_now != left_down)
                {
                    direction_now.SetActive(false);
                    direction_now = left_down;
                    direction_now.SetActive(true);
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Down)
            {
                if (direction_now != down)
                {
                    direction_now.SetActive(false);
                    direction_now = down;
                    direction_now.SetActive(true);
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Right_Down)
            {
                if (direction_now != right_down)
                {
                    direction_now.SetActive(false);
                    direction_now = right_down;
                    direction_now.SetActive(true);
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Right)
            {
                if (direction_now != right)
                {
                    direction_now.SetActive(false);
                    direction_now = right;
                    direction_now.SetActive(true);
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Right_Up)
            {
                if (direction_now != right_up)
                {
                    direction_now.SetActive(false);
                    direction_now = right_up;
                    direction_now.SetActive(true);
                }
            }
        }

        void MoveAndFlip()
        {
            if((int)role_control.Direction_Now < 0)
            {
                //transform.rotation = Quaternion.Euler(0,180,0);
                sprite_renderer.flipX = true;
            }
            else if((int)role_control.Direction_Now > 1)
            {
                //transform.rotation = Quaternion.Euler(0, 0, 0);
                sprite_renderer.flipX = false;
            }

            if (role_control.Rb2D.velocity.magnitude > 0)
                animator.SetBool("is_run", true);
            else
                animator.SetBool("is_run", false);
        }

        void On_Common_Attack(object sender, EventArgs e)
        {
            direction_now.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}