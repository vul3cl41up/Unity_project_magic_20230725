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
        #region 資料
        #region 攻擊與移動方向
        public enum Direction { Left = -1, Left_Up = -2, Left_Down = -3, Up = 0, Down = 1, Right = 2, Right_Up = 3, Right_Down = 4 }
        Direction direction;
        public Direction Direction_Now => direction;
        #endregion

        //資料檔案
        public State_Data state_data;

        #region 移動參數
        public Rigidbody2D Rb2D { get; private set; }
        Vector2 input;
        float move_speed;
        #endregion

        #region 攻擊參數
        public float Attack_Damage { get; private set; }
        public Skill_Data Common_Attack_Data { get; private set; }
        private EventHandler common_attack_received;
        public event EventHandler Common_Attack_Received
        {
            add { common_attack_received += value; }
            remove { common_attack_received -= value; }
        }
        public bool Can_Common_Attack { get;set; }
        float common_attack_cool_time;
        float common_attack_cool_timer;
        public Skill_Data Skill_1_Data { get; private set; }
        private EventHandler skill_1_received;
        public event EventHandler Skill_1_Received
        {
            add { skill_1_received += value; }
            remove { skill_1_received -= value; }
        }
        public bool Can_Skill_1 { get; set; }
        float skill_1_cool_time;
        float skill_1_cool_timer;
        public Skill_Data Skill_2_Data { get; private set; }
        private EventHandler skill_2_received;
        public event EventHandler Skill_2_Received
        {
            add { skill_2_received += value; }
            remove { skill_2_received -= value; }
        }
        public bool Can_Skill_2 { get; set; }
        float skill_2_cool_time;
        float skill_2_cool_timer;

        public Skill_Data Skill_3_Data { get; private set; }
        private EventHandler skill_3_received;
        public event EventHandler Skill_3_Received
        {
            add { skill_3_received += value; }
            remove { skill_3_received -= value; }
        }
        public bool Can_Skill_3 { get; set; }
        float skill_3_cool_time;
        float skill_3_cool_timer;

        public Skill_Data Skill_4_Data { get; private set; }
        private EventHandler skill_4_received;
        public event EventHandler Skill_4_Received
        {
            add { skill_4_received += value; }
            remove { skill_4_received -= value; }
        }
        public bool Can_Skill_4 { get; set; }
        float skill_4_cool_time;
        float skill_4_cool_timer;
        #endregion

        #region 受傷與死亡參數
        private EventHandler take_damage_received;
        public event EventHandler Take_Damage_Received
        {
            add { take_damage_received += value; }
            remove { take_damage_received -= value; }
        }
        public float Blood { get; private set; }
        public float Blood_Now { get; private set; }
        #endregion
        #endregion

        private void Start()
        {
            Rb2D = GetComponent<Rigidbody2D>();
            direction = Direction.Right;
            move_speed = state_data.character_data.move_speed;

            Blood = state_data.character_data.blood;
            Blood_Now = Blood;

            Attack_Damage = state_data.character_data.attack_damage;

            Common_Attack_Data = state_data.character_data.common_attack;
            Can_Common_Attack = true;
            common_attack_cool_time = Common_Attack_Data.cool_time;
            common_attack_cool_timer = 0f;

            Skill_1_Data = state_data.character_data.skill_1;
            Can_Skill_1 = true;
            skill_1_cool_time = Skill_1_Data.cool_time;
            skill_1_cool_timer = 0f;

            Skill_2_Data = state_data.character_data.skill_2;
            Can_Skill_2 = true;
            skill_2_cool_time = Skill_2_Data.cool_time;
            skill_2_cool_timer = 0f;

            Skill_3_Data = state_data.character_data.skill_3;
            Can_Skill_3 = true;
            skill_3_cool_time = Skill_3_Data.cool_time;
            skill_3_cool_timer = 0f;

            Skill_4_Data = state_data.character_data.skill_4;
            Can_Skill_4 = true;
            skill_4_cool_time = Skill_4_Data.cool_time;
            skill_4_cool_timer = 0f;
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
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
            input = input.normalized;
            Rb2D.velocity = input * move_speed;
        }

        private void Change_Direction()
        {
            if (input.x > 0 && input.y > 0)
            {
                direction = Direction.Right_Up;
            }
            else if (input.x > 0 && input.y < 0)
            {
                direction = Direction.Right_Down;
            }
            else if (input.x > 0)
            {
                direction = Direction.Right;
            }
            else if (input.x < 0 && input.y > 0)
            {
                direction = Direction.Left_Up;
            }
            else if (input.x < 0 && input.y < 0)
            {
                direction = Direction.Left_Down;
            }
            else if (input.x < 0)
            {
                direction = Direction.Left;
            }
            else if (input.y > 0)
            {
                direction = Direction.Up;
            }
            else if (input.y < 0)
            {
                direction = Direction.Down;
            }
        }

        public void Take_Attack(Enemy_Data enemy_Data)
        {
            Blood_Now -= enemy_Data.attack_damage;
            take_damage_received?.Invoke(this, EventArgs.Empty);
        }

        void Common_Attack()
        {
            common_attack_cool_timer += Time.deltaTime;
            if(Input.GetButtonDown("Common_Attack") && Can_Common_Attack)
            {
                print($"使用{state_data.character_data.common_attack.skill_name}");
                Can_Common_Attack = false;
                common_attack_received?.Invoke(this, EventArgs.Empty);
            }

            if(common_attack_cool_timer >= common_attack_cool_time)
            {
                Can_Common_Attack = true;
                common_attack_cool_timer = 0;
            }
        }

        void Skill_1()
        {
            skill_1_cool_timer += Time.deltaTime;
            if(Input.GetButtonDown("Skill1") && Can_Skill_1)
            {
                print($"使用{state_data.character_data.skill_1.skill_name}技能");
                Can_Skill_1 = false;
                skill_1_received?.Invoke(this, EventArgs.Empty);
            }

            if (skill_1_cool_timer >= skill_1_cool_time)
            {
                Can_Skill_1 = true;
                skill_1_cool_timer = 0;
            }

        }
        void Skill_2()
        {
            skill_2_cool_timer += Time.deltaTime;
            if (Input.GetButtonDown("Skill2") && Can_Skill_2)
            {
                print($"使用{state_data.character_data.skill_2.skill_name}技能");
                Can_Skill_2 = false;
                skill_2_received?.Invoke(this, EventArgs.Empty);
            }

            if (skill_2_cool_timer >= skill_2_cool_time)
            {
                Can_Skill_2 = true;
                skill_2_cool_timer = 0;
            }
        }
        void Skill_3()
        {
            skill_3_cool_timer += Time.deltaTime;
            if (Input.GetButtonDown("Skill3") && Can_Skill_3)
            {
                print($"使用{state_data.character_data.skill_3.skill_name}技能");
                Can_Skill_3 = false;
                skill_3_received?.Invoke(this, EventArgs.Empty);
            }

            if (skill_3_cool_timer >= skill_3_cool_time)
            {
                Can_Skill_3 = true;
                skill_3_cool_timer = 0;
            }
        }
        void Skill_4()
        {
            skill_4_cool_timer += Time.deltaTime;
            if (Input.GetButtonDown("Skill4") && Can_Skill_4)
            {
                print($"使用{state_data.character_data.skill_4.skill_name}技能");
                Can_Skill_4 = false;
                skill_4_received?.Invoke(this, EventArgs.Empty);
            }

            if (skill_4_cool_timer >= skill_4_cool_time)
            {
                Can_Skill_4 = true;
                skill_4_cool_timer = 0;
            }
        }
    }
}