using UnityEngine;

namespace magic
{
    public class Role_Attack : MonoBehaviour
    {
        private Role_Movement role_movement;

        [SerializeField, Header("人物設定檔")]
        private Role_Data role_data;

        #region 方向物件以及游標
        Transform up;
        Transform left_up;
        Transform left;
        Transform left_down;
        Transform down;
        Transform right_down;
        Transform right;
        Transform right_up;
        public Transform direction_now { get; private set;}
        Transform arrow;
        #endregion

        //技能冷卻計時器
        float common_attack_timer;
        float skill_1_timer;
        float skill_2_timer;
        float skill_3_timer;

        private void Start()
        {
            role_movement = GetComponent<Role_Movement>();
            #region 取得方向物件及游標
            up = transform.GetChild(0).transform;
            left_up = transform.GetChild(1);
            left = transform.GetChild(2);
            left_down = transform.GetChild(3);
            down = transform.GetChild(4);
            right_down = transform.GetChild(5);
            right = transform.GetChild(6);
            right_up = transform.GetChild(7);
            direction_now = right;
            //取得游標
            arrow = transform.GetChild(8);
            #endregion

            common_attack_timer = role_data.common_attack.cool_time;
        }

        private void Update()
        {
            Change_Direction();
            Common_Attack();
            Skill_1();
            Skill_2();
            Skill_3();
        }

        /// <summary>
        /// 處理Common_Attack按鍵的輸入及技能的施放
        /// </summary>
        void Common_Attack()
        {
            common_attack_timer -= Time.deltaTime;
            if(Input.GetButtonDown("Common_Attack")&&common_attack_timer<=0)
            {
                Generate_Skill(role_data.common_attack);
                common_attack_timer = role_data.common_attack.cool_time;
            }
        }

        /// <summary>
        /// 處理Skill1按鍵的輸入及技能的施放
        /// </summary>
        void Skill_1()
        {
            skill_1_timer -= Time.deltaTime;
            if (Input.GetButtonDown("Skill1") && skill_1_timer <= 0)
            {
                Generate_Skill(role_data.skill_1);
                skill_1_timer = role_data.skill_1.cool_time;
            }
        }

        /// <summary>
        /// 處理Skill2按鍵的輸入及技能的施放
        /// </summary>
        void Skill_2()
        {
            skill_2_timer -= Time.deltaTime;
            if (Input.GetButtonDown("Skill2") && skill_2_timer <= 0)
            {
                Generate_Skill(role_data.skill_2);
                skill_2_timer = role_data.skill_2.cool_time;
            }
        }

        /// <summary>
        /// 處理Skill3按鍵的輸入及技能的施放
        /// </summary>
        void Skill_3()
        {
            skill_3_timer -= Time.deltaTime;
            if (Input.GetButtonDown("Skill3") && skill_3_timer <= 0)
            {
                Generate_Skill(role_data.skill_3);
                skill_3_timer = role_data.skill_3.cool_time;
            }
        }

        /// <summary>
        /// 根據輸入的技能資料施放技能
        /// </summary>
        /// <param name="skill_data"></param>
        void Generate_Skill(Skill_Data skill_data)
        {
            switch (skill_data.skill_type)
            {
                case Skill_Type.Common_Attack:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 根據輸入方向，調整攻擊方向
        /// </summary>
        private void Change_Direction()
        {
            if (role_movement.move_input.x > 0)
            {
                if (role_movement.move_input.y > 0)
                {
                    if (direction_now != right_up)
                    {
                        direction_now = right_up;
                    }
                }
                else if (role_movement.move_input.y < 0)
                {
                    if(direction_now != right_down)
                    {
                        direction_now = right_down;
                    }
                }
                else
                {
                    if(direction_now != right)
                    {
                        direction_now = right;
                    }
                }    
            }
            else if (role_movement.move_input.x < 0)
            {
                if (role_movement.move_input.y > 0)
                {
                    if(direction_now != left_up)
                    {
                        direction_now = left_up;
                    }
                }
                else if (role_movement.move_input.y < 0)
                {
                    if(direction_now != left_down)
                    {
                        direction_now = left_down;
                    }
                }
                else
                {
                    if(direction_now != left)
                    {
                        direction_now=left;
                    }
                }
            }
            else
            {
                if (role_movement.move_input.y > 0)
                {
                    if(direction_now != up)
                    {
                        direction_now = up;
                    }
                }
                else if (role_movement.move_input.y < 0)
                {
                    if(direction_now !=  down)
                    {
                        direction_now = down;
                    }
                }
            }

            //將游標更改為現在方向的座標以及旋轉度
            arrow.position = direction_now.position;
            arrow.rotation = direction_now.rotation;
        }
    }
}

