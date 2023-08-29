using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model_scene_1
{
    public class Role_control : MonoBehaviour
    {
        /*
        [SerializeField] float m_moveForce = 400.0f;
        [SerializeField] float m_jumpForce = 100.0f;
        private float m_maxSpeed = 8.0f*8.0f;
        private float m_delayToIdle = 0.0f;
        private Vector2 m_velocity;
        */

        private Animator m_animator; //操作角色的動畫控制
        private Rigidbody2D m_body; //操控角色的剛體
        private AttackSensor attackTrigger; //判斷攻擊範圍
        private ContactFilter2D enemyFilter; //判斷是否為敵人
        public bool attacking = false; //判斷是否在攻擊中
        public Image bloodImage;
        public float attackDamage = 15.0f;
        private float blood = 100.0f;
        private float bloodNow = 100.0f;
        private bool isDead = false;

        //private int m_facingDirection = 1; //角色面向 1=右 -1=左
        private int m_currentAttack = 0; //第幾階段的攻擊 有三階段
        private float m_timeSinceAttack = 0.0f; //攻擊間隔


        //private float m_magic = 80.0f;  //角色魔力量


        void Start()
        {
            m_animator = gameObject.GetComponent<Animator>();
            m_body = gameObject.GetComponent<Rigidbody2D>();
            attackTrigger = transform.Find("AttackTrigger").GetComponent<AttackSensor>();
            enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
        }


        void Update()
        {
            Flip();
            Move();
            Attack();
            HandleAnimation();



        }

        public void TakeDamage(float damage)
        {
            if (!isDead)
            {
                bloodNow -= damage;

                if (bloodNow <= 0)
                {
                    bloodNow = 0;
                    isDead = true;
                    bloodImage.fillAmount = bloodNow / blood;
                    Destroy(gameObject, 1.02f);
                }
                bloodImage.fillAmount = bloodNow / blood;
            }
        }
        private void Attack()
        {

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {

                attackTrigger.transform.SetLocalPositionAndRotation(new Vector3(2.5f, 0.62f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                attackTrigger.transform.SetLocalPositionAndRotation(new Vector3(0.23f, 0.62f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                attackTrigger.transform.SetLocalPositionAndRotation(new Vector3(0.0f, 3.5f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                attackTrigger.transform.SetLocalPositionAndRotation(new Vector3(0.0f, 0.8f, 0.0f), Quaternion.Euler(0.0f, 0.0f, 90.0f));
            }

            if (attacking == true && attackTrigger.AttackTarget() != null)
                attackTrigger.AttackTarget().GetComponent<Enemy_base>().TakeDamage(attackDamage);
            /*
            Collider2D[] enemyColList = new Collider2D[5];
            int enemyCount = attackTrigger.OverlapCollider(enemyFilter, enemyColList);

            if(enemyCount > 0)
            {
                for(int i = 0;i < enemyCount; i++)
                {
                    if (enemyColList[i].GetComponent<Enemy_Dead>().isDead = true) continue;

                    //攻擊並擊退
                    Enemy_base enemy_base = enemyColList[i].GetComponent<Enemy_base>();
                    enemy_base.TakeDamage(1);

                }

            }
            */
        }
        private void HandleAnimation()
        {
            // -- Handle Animations --
            // Increase timer that controls attack combo
            m_timeSinceAttack += Time.deltaTime;
            attacking = false;
            //Attack
            if (isDead)
                m_animator.SetTrigger("Dead");
            else if (Input.GetKeyDown(KeyCode.Z) && m_timeSinceAttack > 0.25f)
            {
                m_currentAttack++;
                attacking = true;
                // Loop back to one after third attack
                if (m_currentAttack > 3)
                    m_currentAttack = 1;

                // Reset Attack combo if time since last attack is too large
                if (m_timeSinceAttack > 1.0f)
                    m_currentAttack = 1;

                // Call one of three attack animations "Attack1", "Attack2", "Attack3"
                m_animator.SetTrigger("Attack" + m_currentAttack);

                // Reset timer
                m_timeSinceAttack = 0.0f;
            }
            else if (Mathf.Abs(m_body.velocity.x) > 0.1 || Mathf.Abs(m_body.velocity.y) > 0.1)
            {
                m_animator.SetInteger("AnimState", 1);
            }
            else
            {
                m_animator.SetInteger("AnimState", 0);
            }
        }
        private void Flip()
        {
            //轉身
            if (m_body.velocity.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (m_body.velocity.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        private void Move()
        {
            m_body.velocity = Input.GetAxis("Horizontal") * Vector2.right * 5 + Input.GetAxis("Vertical") * Vector2.up * 5;
        }

        /*
        private void Move()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                m_delayToIdle = 0.1f;
                if (m_velocity.x < 0)
                {
                    m_body.velocity += (Vector2.right * m_moveForce * Time.deltaTime / m_body.mass * 4);
                    m_velocity = m_body.velocity;
                    Debug.Log(m_velocity);
                }
                else if(m_velocity.x * m_velocity.x + m_velocity.y * m_velocity.y < m_maxSpeed)
                {
                    m_body.velocity += (Vector2.right * m_moveForce * Time.deltaTime / m_body.mass);
                    m_velocity = m_body.velocity;
                    Debug.Log(m_velocity);
                }

            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                m_delayToIdle = 0.1f;
                if (m_velocity.x > 0)
                {
                    m_body.velocity += (Vector2.left * m_moveForce * Time.deltaTime / m_body.mass * 4);
                    m_velocity = m_body.velocity;
                    Debug.Log(m_velocity);
                }
                else if (m_velocity.x * m_velocity.x + m_velocity.y * m_velocity.y < m_maxSpeed)
                {
                    m_body.velocity += (Vector2.left * m_moveForce * Time.deltaTime / m_body.mass);
                    m_velocity = m_body.velocity;
                    Debug.Log(m_velocity);
                }

            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                m_delayToIdle = 0.1f;
                if (m_velocity.y < 0)
                {
                    m_body.velocity += (Vector2.up * m_moveForce * Time.deltaTime / m_body.mass * 4);
                }
                else if (m_velocity.x * m_velocity.x + m_velocity.y * m_velocity.y < m_maxSpeed)
                {
                    m_body.velocity += (Vector2.up * m_moveForce * Time.deltaTime / m_body.mass);
                    m_velocity = m_body.velocity;
                    Debug.Log(m_velocity);
                }

            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                m_delayToIdle = 0.1f;
                if (m_velocity.y > 0)
                {
                    m_body.velocity += (Vector2.down * m_moveForce * Time.deltaTime / m_body.mass * 4);
                }
                else if (m_velocity.x * m_velocity.x + m_velocity.y * m_velocity.y < m_maxSpeed)
                {
                    m_body.velocity += (Vector2.down * m_moveForce * Time.deltaTime / m_body.mass);
                    m_velocity = m_body.velocity;
                    Debug.Log(m_velocity);
                }

            }

            m_delayToIdle -= Time.deltaTime;
            if (m_delayToIdle < 0)
                m_body.velocity = Vector3.zero;
        }
        */
    }

}
