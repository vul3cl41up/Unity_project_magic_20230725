using UnityEngine;
using System;
using UnityEngine.UI;

namespace Model_scene_3
{
    public class Role_Animation_Control : MonoBehaviour
    {
        Role_Control role_control;
        SpriteRenderer sprite_renderer;
        [SerializeField]
        private Image player_blood;

        [SerializeField]
        GameObject fire_ball;

        GameObject up;SpriteRenderer up_sprite_renderer;
        GameObject left_up;SpriteRenderer left_up_sprite_renderer;
        GameObject left; SpriteRenderer left_sprite_renderer;
        GameObject left_down; SpriteRenderer left_down_sprite_renderer;
        GameObject down; SpriteRenderer down_sprite_renderer;
        GameObject right_down; SpriteRenderer right_down_sprite_renderer;
        GameObject right; SpriteRenderer right_sprite_renderer;
        GameObject right_up; SpriteRenderer right_up_sprite_renderer;
        GameObject direction_now; SpriteRenderer direction_now_sprite_renderer;

        Animator animator;


        void Start ()
        {
            role_control = GetComponent<Role_Control>();
            sprite_renderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();

            #region 取得方向物件
            up = transform.GetChild(0).gameObject;
            left_up = transform.GetChild(1).gameObject;
            left = transform.GetChild(2).gameObject;
            left_down = transform.GetChild(3).gameObject;
            down = transform.GetChild(4).gameObject;
            right_down = transform.GetChild(5).gameObject;
            right = transform.GetChild(6).gameObject;
            right_up = transform.GetChild(7).gameObject;
            direction_now = right;
            #endregion
            #region 八個方向的圖片
            up_sprite_renderer = up.GetComponent<SpriteRenderer>();
            left_up_sprite_renderer = left_up.GetComponent<SpriteRenderer>();
            left_sprite_renderer = left.GetComponent<SpriteRenderer>();
            left_down_sprite_renderer = left_down.GetComponent<SpriteRenderer>();
            down_sprite_renderer = down.GetComponent<SpriteRenderer>();
            right_down_sprite_renderer = right_down.GetComponent<SpriteRenderer>();
            right_sprite_renderer = right.GetComponent<SpriteRenderer>();
            right_up_sprite_renderer = right_up.GetComponent<SpriteRenderer>();
            direction_now_sprite_renderer = right_sprite_renderer; 
            #endregion

            role_control.Common_Attack_Received += On_Common_Attack;
            role_control.Skill_1_Received += On_Skill_1;
            role_control.Skill_2_Received += On_Skill_2;
            role_control.Skill_3_Received += On_Skill_3;
            role_control.Skill_4_Received += On_Skill_4;
            role_control.Take_Damage_Received += On_Take_Damage;
        }

        private void On_Take_Damage(object sender, EventArgs e)
        {
            player_blood.fillAmount = role_control.Blood_Now / role_control.Blood;
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
                    direction_now = up;
                    direction_now_sprite_renderer.enabled = false;
                    direction_now_sprite_renderer = up_sprite_renderer;
                    direction_now_sprite_renderer.enabled = true;
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Left_Up)
            {
                if (direction_now != left_up)
                {
                    direction_now = left_up;
                    direction_now_sprite_renderer.enabled = false;
                    direction_now_sprite_renderer = left_up_sprite_renderer;
                    direction_now_sprite_renderer.enabled = true;
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Left)
            {
                if (direction_now != left)
                {
                    direction_now = left;
                    direction_now_sprite_renderer.enabled = false;
                    direction_now_sprite_renderer = left_sprite_renderer;
                    direction_now_sprite_renderer.enabled = true;
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Left_Down)
            {
                if (direction_now != left_down)
                {
                    direction_now = left_down;
                    direction_now_sprite_renderer.enabled = false;
                    direction_now_sprite_renderer = left_down_sprite_renderer;
                    direction_now_sprite_renderer.enabled = true;
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Down)
            {
                if (direction_now != down)
                {
                    direction_now = down;
                    direction_now_sprite_renderer.enabled = false;
                    direction_now_sprite_renderer = down_sprite_renderer;
                    direction_now_sprite_renderer.enabled = true;
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Right_Down)
            {
                if (direction_now != right_down)
                {
                    direction_now = right_down;
                    direction_now_sprite_renderer.enabled = false;
                    direction_now_sprite_renderer = right_down_sprite_renderer;
                    direction_now_sprite_renderer.enabled = true;
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Right)
            {
                if (direction_now != right)
                {
                    direction_now = right;
                    direction_now_sprite_renderer.enabled = false;
                    direction_now_sprite_renderer = right_sprite_renderer;
                    direction_now_sprite_renderer.enabled = true;
                }
            }
            else if (role_control.Direction_Now == Role_Control.Direction.Right_Up)
            {
                if (direction_now != right_up)
                {
                    direction_now = right_up;
                    direction_now_sprite_renderer.enabled = false;
                    direction_now_sprite_renderer = right_up_sprite_renderer;
                    direction_now_sprite_renderer.enabled = true;
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
        private void On_Skill_1(object sender, EventArgs e)
        {
            GameObject fireball = Instantiate(fire_ball, direction_now.transform.position, direction_now.transform.rotation);
            fireball.SetActive(true);
        }
        private void On_Skill_2(object sender, EventArgs e)
        {

        }
        private void On_Skill_3(object sender, EventArgs e)
        {

        }
        private void On_Skill_4(object sender, EventArgs e)
        {

        }

        

        

        
    }
}