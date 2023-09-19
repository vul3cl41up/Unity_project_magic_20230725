using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Model_scene_3
{
    public enum Skill_Type { Common_Attack = -1, Skill_A = 0, Skill_B = 1, Skill_C = 2, Skill_D = 3 }
    public class Role_Control : MonoBehaviour
    {
        public enum Direction { Left = -1, Left_Up = -2, Left_Down = -3, Up = 0, Down = 1, Right = 2, Right_Up = 3, Right_Down = 4 }
        Direction direction;
        public Direction Direction_Now => direction;

        public Character_Data init_character_data;
        public Character_Data character_data;

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
            Init();
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

        private void Init()
        {
            character_data.blood = init_character_data.blood;
            character_data.blood_now = init_character_data.blood_now;
            character_data.magic = init_character_data.magic;
            character_data.magic_now = init_character_data.magic_now;
            character_data.move_speed = init_character_data.move_speed;
            character_data.attack_speed = init_character_data.attack_speed;
            character_data.attack_damage = init_character_data.attack_damage;

            Copy_Skill_Data(character_data.common_attack,init_character_data.common_attack);
            Copy_Skill_Data(character_data.skill_1,init_character_data.skill_1);
            Copy_Skill_Data(character_data.skill_2,init_character_data.skill_2);
            Copy_Skill_Data(character_data.skill_3, init_character_data.skill_3);
            Copy_Skill_Data(character_data.skill_4, init_character_data.skill_4);
        }

        void Copy_Skill_Data(Skill_Data copy_skill_data, Skill_Data origin_skill_data)
        {
            copy_skill_data.skill_type = origin_skill_data.skill_type;
            copy_skill_data.skill_name = origin_skill_data.skill_name;
            copy_skill_data.skill_description = origin_skill_data.skill_description;
            copy_skill_data.skill_image = origin_skill_data.skill_image;
            copy_skill_data.cool_time = origin_skill_data.cool_time;
        }

        private void Move()
        {
            delta_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
            delta_input = delta_input.normalized;
            Rb2D.velocity = delta_input * character_data.move_speed;
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
            character_data.blood_now -= enemy_Data.attack_damage;
            take_damage_received?.Invoke(this, EventArgs.Empty);
        }

        void Common_Attack()
        {
            if(Input.GetButtonDown("Common_Attack") && can_attack)
            {
                print($"使用{character_data.common_attack.skill_name}");
                is_attack = true;
                can_attack = false;
                common_attack_received?.Invoke(this, EventArgs.Empty);
            }
        }

        void Skill_1()
        {
            if(Input.GetButtonDown("Skill1"))
            {
                print($"使用{character_data.skill_1.skill_name}技能");
            }
        }
        void Skill_2()
        {
            if (Input.GetButtonDown("Skill2"))
            {
                print($"使用{character_data.skill_2.skill_name}技能");
            }
        }
        void Skill_3()
        {
            if (Input.GetButtonDown("Skill3"))
            {
                print($"使用{character_data.skill_3.skill_name}技能");
            }
        }
        void Skill_4()
        {
            if (Input.GetButtonDown("Skill4"))
            {
                print($"使用{character_data.skill_4.skill_name}技能");
            }
        }
    }
}