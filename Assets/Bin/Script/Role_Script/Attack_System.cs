using UnityEngine;

namespace magic
{
    public class Attack_System : MonoBehaviour
    {
        [SerializeField, Header("������")]
        private Role_Data role_data;

        private Role_Control role_control;
        GameObject up; SpriteRenderer up_sprite_renderer;
        GameObject left_up; SpriteRenderer left_up_sprite_renderer;
        GameObject left; SpriteRenderer left_sprite_renderer;
        GameObject left_down; SpriteRenderer left_down_sprite_renderer;
        GameObject down; SpriteRenderer down_sprite_renderer;
        GameObject right_down; SpriteRenderer right_down_sprite_renderer;
        GameObject right; SpriteRenderer right_sprite_renderer;
        GameObject right_up; SpriteRenderer right_up_sprite_renderer;
        public GameObject direction_now { get; private set; }
        SpriteRenderer direction_now_sprite_renderer;

        float common_attack_timer = 0;
        float skill_1_timer = 0;
        float skill_2_timer = 0;
        float skill_3_timer = 0;

        private void Start()
        {
            role_control = GetComponent<Role_Control>();
            #region ���o��V����
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
            #region �K�Ӥ�V���Ϥ�
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

            common_attack_timer = role_data.common_attack.cool_time;
            skill_1_timer = role_data.skill_1.cool_time;
            skill_2_timer = role_data.skill_2.cool_time;
            skill_3_timer = role_data.skill_3.cool_time;
        }
        private void Update()
        {
            Change_Direction();
            Common_Attack();
            skill_1();
            skill_2();
            skill_3();

        }
        void Common_Attack()
        {
            if (role_data.common_attack)
            {
                common_attack_timer -= Time.deltaTime;
                if (Input.GetButtonDown("Common_Attack") && common_attack_timer <= 0)
                {
                    Generate_Skill(role_data.common_attack);
                    common_attack_timer = role_data.common_attack.cool_time;
                }
            }
        }
        void skill_1()
        {
            if (role_data.skill_1)
            {
                skill_1_timer -= Time.deltaTime;
                if (Input.GetButtonDown("Skill1") && skill_1_timer <= 0)
                {
                    Generate_Skill(role_data.skill_1);
                    skill_1_timer = role_data.skill_1.cool_time;
                }
            }
        }
        void skill_2()
        {
            if (role_data.skill_2)
            {
                skill_2_timer -= Time.deltaTime;
                if (Input.GetButtonDown("Skill2") && skill_2_timer <= 0)
                {
                    Generate_Skill(role_data.skill_2);
                    skill_2_timer = role_data.skill_2.cool_time;
                }
            }
        }
        void skill_3()
        {
            if (role_data.skill_3)
            {
                skill_3_timer -= Time.deltaTime;
                if (Input.GetButtonDown("Skill3") && skill_3_timer <= 0)
                {
                    Generate_Skill(role_data.skill_3);
                    skill_3_timer = role_data.skill_3.cool_time;
                }
            }
        }

        void Generate_Skill(Skill_Data skill_data)
        {
            switch (skill_data.skill_type)
            {
                case Skill_Type.Common_Attack:
                case Skill_Type.Skill_Poison:
                case Skill_Type.Skill_LoneWave:
                case Skill_Type.Skill_BoomWave:
                    Instantiate(skill_data.skill_prefab,
                    direction_now.transform.position + direction_now.transform.rotation * skill_data.skill_prefab.transform.localPosition,
                    (direction_now.transform.rotation * skill_data.skill_prefab.transform.rotation),
                    direction_now.transform);
                    break;
                case Skill_Type.Skill_Horn:
                    Instantiate(skill_data.skill_prefab,
                    transform.position + transform.rotation * skill_data.skill_prefab.transform.localPosition,
                    (transform.rotation * skill_data.skill_prefab.transform.rotation),
                    transform);
                    break;
                default:
                    Instantiate(skill_data.skill_prefab,
                    direction_now.transform.position + direction_now.transform.rotation * skill_data.skill_prefab.transform.localPosition,
                    (direction_now.transform.rotation * skill_data.skill_prefab.transform.rotation));
                    break;
            }
        }
        private void Change_Direction()
        {
            if (role_control.move_input.x > 0)
            {
                if (role_control.move_input.y > 0)
                {
                    if (direction_now != right_up)
                    {
                        direction_now = right_up;
                        direction_now_sprite_renderer.enabled = false;
                        direction_now_sprite_renderer = right_up_sprite_renderer;
                        direction_now_sprite_renderer.enabled = true;
                    }
                }
                else if (role_control.move_input.y < 0)
                {
                    if (direction_now != right_down)
                    {
                        direction_now = right_down;
                        direction_now_sprite_renderer.enabled = false;
                        direction_now_sprite_renderer = right_down_sprite_renderer;
                        direction_now_sprite_renderer.enabled = true;
                    }
                }
                else
                {
                    if (direction_now != right)
                    {
                        direction_now = right;
                        direction_now_sprite_renderer.enabled = false;
                        direction_now_sprite_renderer = right_sprite_renderer;
                        direction_now_sprite_renderer.enabled = true;
                    }
                }
            }
            else if (role_control.move_input.x < 0)
            {
                if (role_control.move_input.y > 0)
                {
                    if (direction_now != left_up)
                    {
                        direction_now = left_up;
                        direction_now_sprite_renderer.enabled = false;
                        direction_now_sprite_renderer = left_up_sprite_renderer;
                        direction_now_sprite_renderer.enabled = true;
                    }
                }
                else if (role_control.move_input.y < 0)
                {
                    if (direction_now != left_down)
                    {
                        direction_now = left_down;
                        direction_now_sprite_renderer.enabled = false;
                        direction_now_sprite_renderer = left_down_sprite_renderer;
                        direction_now_sprite_renderer.enabled = true;
                    }
                }
                else
                {
                    if (direction_now != left)
                    {
                        direction_now = left;
                        direction_now_sprite_renderer.enabled = false;
                        direction_now_sprite_renderer = left_sprite_renderer;
                        direction_now_sprite_renderer.enabled = true;
                    }
                }
            }
            else if (role_control.move_input.y > 0)
            {
                if (direction_now != up)
                {
                    direction_now = up;
                    direction_now_sprite_renderer.enabled = false;
                    direction_now_sprite_renderer = up_sprite_renderer;
                    direction_now_sprite_renderer.enabled = true;
                }
            }
            else if (role_control.move_input.y < 0)
            {
                if (direction_now != down)
                {
                    direction_now = down;
                    direction_now_sprite_renderer.enabled = false;
                    direction_now_sprite_renderer = down_sprite_renderer;
                    direction_now_sprite_renderer.enabled = true;
                }
            }
        }
    }
}
