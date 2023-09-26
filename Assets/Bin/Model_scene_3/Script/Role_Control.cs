using Fungus;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Model_scene_3
{

    public class Role_Control : MonoBehaviour
    {
        #region 資料
        #region 攻擊與移動方向
        public enum Direction { Left = -1, Left_Up = -2, Left_Down = -3, Up = 0, Down = 1, Right = 2, Right_Up = 3, Right_Down = 4 }
        public Direction Direction_Now { get; private set; }
        #endregion

        //資料檔案
        [SerializeField]
        private State_Data current_state_data;
        [SerializeField]
        private State_Data saved_state_data;
        [SerializeField, Header("對話管理")]
        private Flowchart fungusGM;


        public Character_Data Current_Character_Data { get; private set; }

        #region 移動參數
        public Rigidbody2D Rb2D { get; private set; }
        Vector2 input;
        #endregion

        #region 攻擊參數
        private System.EventHandler common_attack_received;
        public event System.EventHandler Common_Attack_Received
        {
            add { common_attack_received += value; }
            remove { common_attack_received -= value; }
        }
        private bool can_common_attack;
        float common_attack_cool_time;
        float common_attack_cool_timer;
        private System.EventHandler skill_1_received;
        public event System.EventHandler Skill_1_Received
        {
            add { skill_1_received += value; }
            remove { skill_1_received -= value; }
        }
        private bool can_skill_1;
        float skill_1_cool_time;
        float skill_1_cool_timer;

        private System.EventHandler skill_2_received;
        public event System.EventHandler Skill_2_Received
        {
            add { skill_2_received += value; }
            remove { skill_2_received -= value; }
        }
        private bool can_skill_2;
        float skill_2_cool_time;
        float skill_2_cool_timer;

        private System.EventHandler skill_3_received;
        public event System.EventHandler Skill_3_Received
        {
            add { skill_3_received += value; }
            remove { skill_3_received -= value; }
        }
        private bool can_skill_3;
        float skill_3_cool_time;
        float skill_3_cool_timer;

        private System.EventHandler skill_4_received;
        public event System.EventHandler Skill_4_Received
        {
            add { skill_4_received += value; }
            remove { skill_4_received -= value; }
        }
        private bool can_skill_4;
        float skill_4_cool_time;
        float skill_4_cool_timer;
        #endregion

        #region 受傷與死亡參數
        private System.EventHandler take_damage_received;
        public event System.EventHandler Take_Damage_Received
        {
            add { take_damage_received += value; }
            remove { take_damage_received -= value; }
        }
        Transform HP_Canvas;
        [SerializeField]
        GameObject HP_Text;
        #endregion
        #endregion

        private void Start()
        {
            State_Data_Control.Copy_State_Data(saved_state_data, current_state_data);
            Current_Character_Data = current_state_data.character_data;

            Rb2D = GetComponent<Rigidbody2D>();
            Direction_Now = Direction.Right;
            HP_Canvas = transform.GetChild(8);

            can_common_attack = true;
            common_attack_cool_time = Current_Character_Data.common_attack.cool_time;
            common_attack_cool_timer = 0f;

            can_skill_1 = true;
            skill_1_cool_time = Current_Character_Data.skill_1.cool_time;
            skill_1_cool_timer = 0f;

            can_skill_2 = true;
            skill_2_cool_time = Current_Character_Data.skill_2.cool_time;
            skill_2_cool_timer = 0f;

            can_skill_3 = true;
            skill_3_cool_time = Current_Character_Data.skill_3.cool_time;
            skill_3_cool_timer = 0f;

            can_skill_4 = true;
            skill_4_cool_time = Current_Character_Data.skill_4.cool_time;
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
            Rb2D.velocity = input * Current_Character_Data.move_speed;
        }

        private void Change_Direction()
        {
            if (input.x > 0 && input.y > 0)
            {
                Direction_Now = Direction.Right_Up;
            }
            else if (input.x > 0 && input.y < 0)
            {
                Direction_Now = Direction.Right_Down;
            }
            else if (input.x > 0)
            {
                Direction_Now = Direction.Right;
            }
            else if (input.x < 0 && input.y > 0)
            {
                Direction_Now = Direction.Left_Up;
            }
            else if (input.x < 0 && input.y < 0)
            {
                Direction_Now = Direction.Left_Down;
            }
            else if (input.x < 0)
            {
                Direction_Now = Direction.Left;
            }
            else if (input.y > 0)
            {
                Direction_Now = Direction.Up;
            }
            else if (input.y < 0)
            {
                Direction_Now = Direction.Down;
            }
        }

        public void Take_Attack(Enemy_Data enemy_Data)
        {
            Current_Character_Data.blood_now -= enemy_Data.attack_damage;
            take_damage_received?.Invoke(this, EventArgs.Empty);

            GameObject getDamage = Instantiate(HP_Text, HP_Canvas.position, Quaternion.identity, HP_Canvas);
            getDamage.GetComponent<TMP_Text>().text = enemy_Data.attack_damage.ToString();
            Destroy(getDamage, 1f);

            if(Current_Character_Data.blood_now <= 0)
            {
                Rb2D.velocity = Vector3.zero;
                fungusGM.SendFungusMessage("遊戲失敗");
            }
        }

        void Common_Attack()
        {
            common_attack_cool_timer += Time.deltaTime;
            if(Input.GetButtonDown("Common_Attack") && can_common_attack)
            {
                print($"使用{current_state_data.character_data.common_attack.skill_name}");
                can_common_attack = false;
                common_attack_received?.Invoke(this, EventArgs.Empty);
            }

            if(common_attack_cool_timer >= common_attack_cool_time)
            {
                can_common_attack = true;
                common_attack_cool_timer = 0;
            }
        }

        void Skill_1()
        {
            skill_1_cool_timer += Time.deltaTime;
            if(Input.GetButtonDown("Skill1") && can_skill_1)
            {
                print($"使用{current_state_data.character_data.skill_1.skill_name}技能");
                can_skill_1 = false;
                skill_1_received?.Invoke(this, EventArgs.Empty);
            }

            if (skill_1_cool_timer >= skill_1_cool_time)
            {
                can_skill_1 = true;
                skill_1_cool_timer = 0;
            }

        }
        void Skill_2()
        {
            skill_2_cool_timer += Time.deltaTime;
            if (Input.GetButtonDown("Skill2") && can_skill_2)
            {
                print($"使用{current_state_data.character_data.skill_2.skill_name}技能");
                can_skill_2 = false;
                skill_2_received?.Invoke(this, EventArgs.Empty);
            }

            if (skill_2_cool_timer >= skill_2_cool_time)
            {
                can_skill_2 = true;
                skill_2_cool_timer = 0;
            }
        }
        void Skill_3()
        {
            skill_3_cool_timer += Time.deltaTime;
            if (Input.GetButtonDown("Skill3") && can_skill_3)
            {
                print($"使用{current_state_data.character_data.skill_3.skill_name}技能");
                can_skill_3 = false;
                skill_3_received?.Invoke(this, EventArgs.Empty);
            }

            if (skill_3_cool_timer >= skill_3_cool_time)
            {
                can_skill_3 = true;
                skill_3_cool_timer = 0;
            }
        }
        void Skill_4()
        {
            skill_4_cool_timer += Time.deltaTime;
            if (Input.GetButtonDown("Skill4") && can_skill_4)
            {
                print($"使用{current_state_data.character_data.skill_4.skill_name}技能");
                can_skill_4 = false;
                skill_4_received?.Invoke(this, EventArgs.Empty);
            }

            if (skill_4_cool_timer >= skill_4_cool_time)
            {
                can_skill_4 = true;
                skill_4_cool_timer = 0;
            }
        }
    }
}