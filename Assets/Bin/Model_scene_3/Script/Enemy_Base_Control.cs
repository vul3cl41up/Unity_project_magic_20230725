using Model_scene_1;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

namespace Model_scene_3
{
    public class Enemy_Base_Control : MonoBehaviour
    {
        //載入怪物資料
        //怪物的基礎功能，移動、人物碰到會造成傷害、怪物碰到攻擊會受傷
        //連同動畫控制
        private Animator animator;
        private Rigidbody2D m_rb2D;
        //public Image bloodImage;
        public Enemy_Data this_enemy_data;

        private GameObject target;


        private float blood;
        private float blood_now;
        private float move_speed;
        private float attack_speed;

        private bool is_dead = false;
        private bool is_touch = false;
        private bool can_attack = true;

        private float attack_timer = 0;
        private float attack_cool_time;

        Role_Control player;

        int count = 50;


        private void Start()
        {
            animator = GetComponent<Animator>();
            m_rb2D = GetComponent<Rigidbody2D>();
            target = GameObject.FindWithTag("Player");


            Init_Data();
        }

        void Init_Data()
        {
            blood = this_enemy_data.blood;
            blood_now = blood;
            move_speed = this_enemy_data.move_speed;
            attack_speed = this_enemy_data.attack_speed;
            attack_cool_time = 4 / attack_speed;
        }

        private void Update()
        {
            Attack();
            Handle_Anim();
        }

        private void FixedUpdate()
        {
            Tracking();
        }

        void Tracking()
        {
            Vector2 diff = new Vector2(target.transform.position.x - this.transform.position.x, target.transform.position.y - this.transform.position.y);
            m_rb2D.velocity = diff.normalized * move_speed;

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                player = collision.gameObject.GetComponent<Role_Control>();
                is_touch = true;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            player = null;
            is_touch = false;
        }

        protected virtual void Attack()
        {
            attack_timer += Time.deltaTime;
            if (attack_timer > attack_cool_time) can_attack = true;
            if (is_touch && can_attack)
            {
                player.Take_Attack(this_enemy_data);
                can_attack = false;
                attack_timer = 0;
            }
        }

        private void Flip()
        {
            count--;
            //轉身
            if (count <= 0)
            {
                if (m_rb2D.velocity.x > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    count = 50;
                }
                else if (m_rb2D.velocity.x < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    count = 50;
                }
            }

        }

        protected virtual void Handle_Anim()
        {
            Flip();
            //bloodImage.fillAmount = blood_now / blood;

            /*if (m_rb2D.velocity.magnitude > 0)
                animator.SetInteger("AnimState", 1);
            else
                animator.SetInteger("AnimState", 0);

            if (is_dead)
                animator.SetTrigger("Dead");*/
        }

        public void Take_Damage(Character_Data character_Data, Skill_Type skill_type)
        {
            if (skill_type == character_Data.common_attack.skill_type)
            {
                Damage_Deal(character_Data, character_Data.common_attack);
            }
            else if (skill_type == character_Data.skill_1.skill_type)
            {
                Damage_Deal(character_Data,
                    character_Data.skill_1);
            }
            else if (skill_type == character_Data.skill_2.skill_type)
            {
                Damage_Deal(character_Data,
                    character_Data.skill_2);
            }
            else if (skill_type == character_Data.skill_3.skill_type)
            {
                Damage_Deal(character_Data,
                    character_Data.skill_3);
            }
            else if (skill_type == character_Data.skill_4.skill_type)
            {
                Damage_Deal(character_Data,
                    character_Data.skill_4);
            }

            if (blood_now <= 0)
            {
                Destroy(gameObject);
            }

        }
        protected virtual void Damage_Deal(Character_Data character_data, Skill_Data skill_data)
        {

            if (skill_data.skill_type == Skill_Type.Common_Attack)
            {
                blood_now -= character_data.attack_damage;
            }
            else if(skill_data.skill_type == Skill_Type.Skill_A)
            {
                blood_now -= character_data.attack_damage;
            }
            else if (skill_data.skill_type == Skill_Type.Skill_B)
            {
                blood_now -= character_data.attack_damage;
            }
            else if (skill_data.skill_type == Skill_Type.Skill_C)
            {
                blood_now -= character_data.attack_damage;
            }
            else if (skill_data.skill_type == Skill_Type.Skill_D)
            {
                blood_now -= character_data.attack_damage;
            }

        }


}
}

