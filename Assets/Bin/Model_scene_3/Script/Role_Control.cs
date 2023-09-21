using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Model_scene_3
{
    
    public class Role_Control : MonoBehaviour
    {
        public enum Direction { Left = -1, Left_Up = -2, Left_Down = -3, Up = 0, Down = 1, Right = 2, Right_Up = 3, Right_Down = 4 }
        Direction direction;
        public Direction Direction_Now => direction;

        public State_Data state_data;

        public Rigidbody2D Rb2D { get; private set; }
        public bool can_attack = true;

        Vector2 delta_input;
        public bool is_attack = false;

        private EventHandler common_attack_received;
        public event EventHandler Common_Attack_Received
        {
            add { common_attack_received += value;}
            remove { common_attack_received -= value;}
        }

        private EventHandler take_damage_received;
        public event EventHandler Take_Damage_Received
        {
            add { take_damage_received += value; }
            remove { take_damage_received -= value; }
        }

        private void Start()
        {
            Rb2D = GetComponent<Rigidbody2D>();
            direction = Direction.Right;
        }
        private void Update()
        {
            Common_Attack();
            Skill_1();
            Skill_2();
            Skill_3();
            Skill_4();
        }
        private void FixedUpdate()
        {
            Move();
            Change_Direction();
        }

        private void Move()
        {
            delta_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
            delta_input = delta_input.normalized;
            Rb2D.velocity = delta_input * state_data.character_data.move_speed;
        }

        private void Change_Direction()
        {
            if (is_attack)
                return;
            if (delta_input.x > 0 && delta_input.y > 0)
            {
                direction = Direction.Right_Up;
            }
            else if (delta_input.x > 0 && delta_input.y < 0)
            {
                direction = Direction.Right_Down;
            }
            else if (delta_input.x > 0)
            {
                direction = Direction.Right;
            }
            else if (delta_input.x < 0 && delta_input.y > 0)
            {
                direction = Direction.Left_Up;
            }
            else if (delta_input.x < 0 && delta_input.y < 0)
            {
                direction = Direction.Left_Down;
            }
            else if (delta_input.x < 0)
            {
                direction = Direction.Left;
            }
            else if (delta_input.y > 0)
            {
                direction = Direction.Up;
            }
            else if (delta_input.y < 0)
            {
                direction = Direction.Down;
            }
        }

        public void Take_Attack(Enemy_Data enemy_Data)
        {
            state_data.character_data.blood_now -= enemy_Data.attack_damage;
            take_damage_received?.Invoke(this, EventArgs.Empty);
        }

        void Common_Attack()
        {
            if(Input.GetButtonDown("Common_Attack") && can_attack)
            {
                print($"使用{state_data.character_data.common_attack.skill_name}");
                is_attack = true;
                can_attack = false;
                common_attack_received?.Invoke(this, EventArgs.Empty);
            }
        }

        void Skill_1()
        {
            if(Input.GetButtonDown("Skill1"))
            {
                print($"使用{state_data.character_data.skill_1.skill_name}技能");
            }
        }
        void Skill_2()
        {
            if (Input.GetButtonDown("Skill2"))
            {
                print($"使用{state_data.character_data.skill_2.skill_name}技能");
            }
        }
        void Skill_3()
        {
            if (Input.GetButtonDown("Skill3"))
            {
                print($"使用{state_data.character_data.skill_3.skill_name}技能");
            }
        }
        void Skill_4()
        {
            if (Input.GetButtonDown("Skill4"))
            {
                print($"使用{state_data.character_data.skill_4.skill_name}技能");
            }
        }
    }
}