﻿using Model_scene_1;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

namespace Model_scene_2
{
    public class Role_Control_Magic : MonoBehaviour
    {
        #region 資料
        public enum Status { Idle, Walk, Jump, Attack,Skill_J }
        Status status_now;
        public Status Status_Now => status_now;
        private Rigidbody2D rb;
        private float global_timer = 0;
        Vector3 role_position_offset = new Vector3 (0, 0.33f, 0);

        #region 角色物件
        GameObject role_now;
        GameObject role_front;
        GameObject role_back;
        GameObject role_side; 
        #endregion
        #region 方向
        private int x_direction = 0;
        private int change_x;
        private int y_direction = 0;
        private int change_y; 
        #endregion
        #region 移動參數
        private Vector2 oblique_acceleration = new Vector3(3.54f, 3.54f);
        private Vector2 x_acceleration = new Vector3(5f, 0);
        private Vector2 y_acceleration = new Vector3(0, 5f);
        private Vector2 change_acceleration;
        private float resistance = 2.4f; 
        #endregion
        #region 跳躍參數
        private Vector2 oblique_jump_acceleration = new Vector3(4.24f, 4.24f);
        private Vector2 x_jump_acceleration = new Vector3(6f, 0);
        private Vector2 y_jump_acceleration = new Vector3(0, 6f);
        private float jump_resistance = 6.25f;
        private float oblique_jump_resistance = 4.42f;
        private float jump_time = 0.6f;
        private float jump_timer = 0;
        private bool jump_happend = false; 
        #endregion
        #region 攻擊參數
        private float attack_time = 0.6f;
        private float attack_timer = 0f;
        private float time_send_attack = 0.3f;
        private bool canAttack = true;
        Collider2D hit = null;
        Rigidbody2D hit_rb;
        Enemy_base hit_script;
        Transform hit_base;
        #endregion
        #region 攻擊範圍
        [SerializeField]
        private Vector3 attack_size_front = new Vector3(1.5f, 0.5f, 0);
        [SerializeField]
        private Vector3 attack_offset_front = new Vector3(0, -0.25f, 0);
        [SerializeField]
        private Vector3 attack_size_side = new Vector3(0.5f, 1.5f, 0);
        [SerializeField]
        private Vector3 attack_offset_side = new Vector3(-0.75f, 0.7f, 0);
        [SerializeField]
        private Vector3 attack_size_back = new Vector3(1.5f, 0.5f, 0);
        [SerializeField]
        private Vector3 attack_offset_back = new Vector3(0, 1.6f, 0);
        [SerializeField]
        private LayerMask layer_target; 
        #endregion
        #region 道具欄參數
        private bool isTouch = false;
        private GameObject itemObject = null;
        public Inventory myBag;
        #endregion
        #region 技能J參數
        private Vector2 oblique_skill_acceleration = new Vector3(8.49f, 8.49f);
        private float skill_acceleration = 12f;
        private float skill_j_time = 0.1f;
        private float skill_j_timer = 0;
        private float skill_j_cooling_time = 1f;
        private float skill_j_cooling_timer = 0;
        private bool can_skill_j = true;
        private float skill_j_consume = 10f;
        #endregion
        #region 魔力值參數
        private float magic = 100f;
        private float magic_now = 100f;
        [SerializeField]
        private Image magic_image;
        #endregion
        #region 生命值值參數
        private float blood = 100f;
        private float blood_now = 100f;
        [SerializeField]
        private Image blood_image;
        #endregion
        #endregion


        #region 事件
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            //Gizmos.DrawCube(transform.position + transform.TransformDirection(attack_offset_front), attack_size_front);
            //Gizmos.DrawCube(transform.position + transform.TransformDirection(attack_offset_side), attack_size_side);
            //Gizmos.DrawCube(transform.position + transform.TransformDirection(attack_offset_back), attack_size_back);
        }
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            role_front = transform.GetChild(0).gameObject;
            role_back = transform.GetChild(1).gameObject;
            role_side = transform.GetChild(2).gameObject;
            role_now = role_front;
            status_now = Status.Idle;
            layer_target = LayerMask.GetMask("Enemy");
        }
        private void Update()
        {
            PickUp();
        }
        private void FixedUpdate()
        {
            global_timer += Time.fixedDeltaTime;
            RunStatus();
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Prop")
            {
                isTouch = true;
                itemObject = collision.gameObject;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Prop")
            {
                isTouch = false;
                itemObject = null;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 根據不同的狀態執行相應的行為
        /// </summary>
        void RunStatus()
        {
            if (status_now == Status.Idle)
                RunIdleStatus();
            else if (status_now == Status.Walk)
                RunWalkStatus();
            else if (status_now == Status.Jump)
                RunJumpStatus();
            else if (status_now == Status.Attack)
                RunAttackStatus();
            else if (status_now == Status.Skill_J)
                RunSkillJStatus();
        }
        /// <summary>
        /// 當狀態是等待時進行的動作
        /// </summary>
        void RunIdleStatus()
        {
            ChangeAcceleration();
            if (Input.GetKeyDown(KeyCode.J) && SkillJFire())
                status_now = Status.Skill_J;
            else if (Input.GetKeyDown(KeyCode.H))
                status_now = Status.Attack;
            else if (Input.GetKeyDown(KeyCode.Space))
                status_now = Status.Jump;
            else if (change_x != 0 || change_y != 0)
                status_now = Status.Walk;

        }
        /// <summary>
        /// 當狀態是走路/跑步時進行的動作
        /// </summary>
        void RunWalkStatus()
        {
            ChangeAcceleration();

            if (Input.GetKeyDown(KeyCode.J) && SkillJFire())
            {
                status_now = Status.Skill_J;
                return;
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                status_now = Status.Attack;
                return;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                status_now = Status.Jump;
                return;
            }
            else if (rb.velocity != Vector2.zero)
                status_now = Status.Walk;
            else
                status_now = Status.Idle;


            ChangeDirect();
            ChangeVeolocity();
        }
        void RunJumpStatus()
        {
            jump_timer += Time.fixedDeltaTime;
            if (jump_timer >= jump_time)
            {
                jump_timer = 0;
                status_now = Status.Walk;
                jump_happend = false;
                return;
            }
            if (!jump_happend)
            {
                jump_happend = true;

                if (change_x == 0 && change_y == 0)
                {
                    return;
                }
                else if (change_x != 0 && change_y != 0)
                {
                    if (Mathf.Abs(rb.velocity.x) <= oblique_jump_resistance)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        x_direction = 0;
                    }
                    else
                    {
                        rb.velocity -= new Vector2(x_direction * oblique_jump_resistance, rb.velocity.y);
                    }

                    if (Mathf.Abs(rb.velocity.y) <= oblique_jump_resistance)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                        y_direction = 0;
                    }
                    else
                    {
                        rb.velocity -= new Vector2(rb.velocity.x, y_direction * oblique_jump_resistance);
                    }

                    rb.velocity += new Vector2(change_x * oblique_jump_acceleration.x, change_y * oblique_jump_acceleration.y);
                }
                else if (change_x != 0)
                {
                    if (Mathf.Abs(rb.velocity.x) <= jump_resistance)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        x_direction = 0;
                    }
                    else
                    {
                        rb.velocity -= new Vector2(x_direction * jump_resistance, rb.velocity.y);
                    }

                    rb.velocity += x_jump_acceleration * change_x;
                }
                else if (change_y != 0)
                {
                    if (Mathf.Abs(rb.velocity.y) <= jump_resistance)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                    }
                    else
                    {
                        rb.velocity -= new Vector2(rb.velocity.x, y_direction * jump_resistance);
                    }
                    rb.velocity += y_jump_acceleration * change_y;
                }

                ChangeDirect();
            }
            if (Input.GetKeyDown(KeyCode.J) && SkillJFire())
            {
                status_now = Status.Skill_J;
                jump_timer = 0;
                jump_happend = false;
                return;
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                jump_timer = 0;
                status_now = Status.Attack;
                jump_happend = false;
                return;
            }

        }
        void RunAttackStatus()
        {
            attack_timer += Time.fixedDeltaTime;
            if (attack_timer >= time_send_attack && canAttack)
            {
                canAttack = false;
                if (AttackTarget())
                    AttackDeal();
            }
            if (attack_timer >= attack_time)
            {
                attack_timer = 0;
                canAttack = true;
                status_now = Status.Walk;
            }

        }
        void RunSkillJStatus()
        {
            skill_j_timer += Time.fixedDeltaTime;
            if(can_skill_j)
            {
                can_skill_j = false;
                magic_now -= skill_j_consume;
                magic_image.fillAmount = magic_now/magic;

                if (change_x != 0 && change_y != 0)
                {
                    rb.velocity += new Vector2(oblique_skill_acceleration.x * change_x, oblique_skill_acceleration.y * change_y);
                }
                else if (change_x != 0)
                {
                    rb.velocity += new Vector2(change_x * skill_acceleration, rb.velocity.y);
                }
                else if (change_y != 0)
                {
                    rb.velocity += new Vector2(rb.velocity.x, change_y * skill_acceleration);
                }

            }
            else if(skill_j_timer >= skill_j_time)
            {
                can_skill_j = true;
                skill_j_timer = 0;
                skill_j_cooling_timer = global_timer;
                status_now = Status.Walk;
            }
        }
        private void ChangeAcceleration()
        {
            if (rb.velocity.x > 0)
                x_direction = 1;
            else if (rb.velocity.x == 0)
                x_direction = 0;
            else if (rb.velocity.x < 0)
                x_direction = -1;

            if (rb.velocity.y > 0)
                y_direction = 1;
            else if (rb.velocity.y == 0)
                y_direction = 0;
            else if (rb.velocity.y < 0)
                y_direction = -1;

            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            {
                change_x = -1;
                change_y = 1;
                change_acceleration = oblique_acceleration;
            }
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
            {
                change_x = -1;
                change_y = -1;
                change_acceleration = oblique_acceleration;
            }
            else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            {
                change_x = 1;
                change_y = 1;
                change_acceleration = oblique_acceleration;
            }
            else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
            {
                change_x = 1;
                change_y = -1;
                change_acceleration = oblique_acceleration;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                change_x = -1;
                change_y = 0;
                change_acceleration = x_acceleration;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                change_x = 1;
                change_y = 0;
                change_acceleration = x_acceleration;
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                change_x = 0;
                change_y = 1;
                change_acceleration = y_acceleration;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                change_x = 0;
                change_y = -1;
                change_acceleration = y_acceleration;
            }
            else
            {
                change_x = 0;
                change_y = 0;
            }
        }
        void ChangeVeolocity()
        {
            if (change_x == 0 && change_y == 0)
            {
                rb.velocity -= rb.velocity / resistance * Time.fixedDeltaTime;
                if (Mathf.Abs(rb.velocity.x) < 4)
                    rb.velocity = new Vector2(0, rb.velocity.y);

                if (Mathf.Abs(rb.velocity.y) < 4)
                    rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            if (change_x + x_direction == 0)
            {
                if (Mathf.Abs(rb.velocity.x) < 7)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    x_direction = 0;
                }
                else
                {
                    rb.velocity += new Vector2(7 * change_x, 0);
                }
            }
            else if (change_x == 0)
            {
                if (Mathf.Abs(rb.velocity.x) < 4f)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    x_direction = 0;
                }
            }
            else
            {
                x_direction = change_x;
                rb.velocity += new Vector2(change_acceleration.x * Time.fixedDeltaTime * change_x, 0);
            }

            if (change_y + y_direction == 0)
            {
                if (Mathf.Abs(rb.velocity.y) < 7)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    y_direction = 0;
                }
                else
                {
                    rb.velocity += new Vector2(0, 7 * change_y);
                }
            }
            else if (change_y == 0)
            {
                if (Mathf.Abs(rb.velocity.y) < 4f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    y_direction = 0;
                }
            }
            else
            {
                y_direction = change_y;
                rb.velocity += new Vector2(0, change_acceleration.y * Time.fixedDeltaTime * change_y);
            }

            rb.velocity -= (rb.velocity / resistance * Time.fixedDeltaTime);
        }
        void ChangeDirect()
        {
            if (x_direction == 0 || y_direction == 0)
            {

                if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && role_now == role_side && transform.rotation.y != 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0f, 0);
                }
                else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    if (role_now != role_side)
                    {
                        role_now.SetActive(false);
                        role_now = role_side;
                        role_now.SetActive(true);
                    }
                }
                else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && role_now == role_side && transform.rotation.y == 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180f, 0);
                }
                else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    if (role_now != role_side)
                    {
                        role_now.SetActive(false);
                        role_now = role_side;
                        transform.rotation = Quaternion.Euler(0, 180f, 0);
                        role_now.SetActive(true);
                    }

                }
                else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
                {
                    if (role_now != role_front)
                    {
                        role_now.SetActive(false);
                        role_now = role_front;
                        transform.rotation = Quaternion.Euler(0, 0f, 0);
                        role_now.SetActive(true);
                    }
                }
                else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                {
                    if (role_now != role_back)
                    {
                        role_now.SetActive(false);
                        role_now = role_back;
                        transform.rotation = Quaternion.Euler(0, 0f, 0);
                        role_now.SetActive(true);
                    }
                }
            }
        }
        bool AttackTarget()
        {
            if (role_now == role_front)
            {
                hit = Physics2D.OverlapBox(transform.position + transform.TransformDirection(attack_offset_front), attack_size_front, 0, layer_target);
            }
            else if (role_now == role_back)
            {
                hit = Physics2D.OverlapBox(transform.position + transform.TransformDirection(attack_offset_back), attack_size_back, 0, layer_target);
            }
            else if (role_now == role_side)
            {
                hit = Physics2D.OverlapBox(transform.position + transform.TransformDirection(attack_offset_side), attack_size_side, 0, layer_target);
            }
            
            if(hit)
            {
                hit_rb = hit.GetComponentInParent<Rigidbody2D>();
                hit_script = hit.GetComponentInParent<Enemy_base>();
                hit_base = hit.GetComponentInParent<Transform>();
            }
            return hit;
        }
        void AttackDeal()
        {
            
        }
        bool SkillJFire()
        {
            if (magic_now < skill_j_consume)
                return false;
            if (global_timer - skill_j_cooling_timer < skill_j_cooling_time)
                return false;
            if(change_x == 0 && change_y == 0)
                return false;
            return true;
        }
        private void PickUp()
        {
            if (isTouch && itemObject != null && Input.GetKeyDown(KeyCode.G))
            {
                Item item = itemObject.GetComponent<ItemOnWorld>().thisItem;

                int index = myBag.itemList.FindIndex(x => x == item);
                if (index != -1)
                    myBag.itemHold[index]++;
                else
                {
                    myBag.itemList.Add(item);
                    myBag.itemHold.Add(1);
                }

                Destroy(itemObject);
                Inventory_Manager.Refresh();
            }
        }
        #endregion
    }
}

