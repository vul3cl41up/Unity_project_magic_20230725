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
        public Image bloodImage;
        private GameObject target;
        public Enemy_Data this_enemy_data;

        private float attackDamage = 15.0f;

        private float blood = 100.0f;
        private float bloodNow = 100.0f;

        private bool isDead = false;


        private float trackSpeed = 4f;
        private float stopAfterAttack = 0f;

        
        public float FollowRadius = 6f;
        private LayerMask playerFilter;

        private void Start()
        {
            animator = GetComponent<Animator>();
            m_rb2D = GetComponent<Rigidbody2D>();
            playerFilter = LayerMask.GetMask("Player");
            target = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            stopAfterAttack -= Time.deltaTime;
            Tracking();
            Flip();
            HandleAnim();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && stopAfterAttack < 0)
            {
                m_rb2D.velocity = Vector2.zero;
                stopAfterAttack = 1.0f;
                //collision.gameObject.GetComponent<Role_Control>().TakeDamage(attackDamage);
            }
        }

        void Tracking()
        {
            if (target != null && stopAfterAttack < 0)
            {
                Vector2 diff = new Vector2(target.transform.position.x - this.transform.position.x, target.transform.position.y - this.transform.position.y);
                if (diff.x != 0 || diff.y != 0)
                    m_rb2D.velocity = diff.normalized * trackSpeed;
                else
                    m_rb2D.velocity = Vector2.zero;
            }
            else
            {
                m_rb2D.velocity = Vector2.zero;
            }
        }
        private void Flip()
        {
            //轉身
            if (m_rb2D.velocity.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (m_rb2D.velocity.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        void HandleAnim()
        {
            if (m_rb2D.velocity.x > 0.1 || m_rb2D.velocity.y > 0.1)
                animator.SetInteger("AnimState", 1);
            else
                animator.SetInteger("AnimState", 0);

            if (isDead)
                animator.SetTrigger("Dead");
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
                    Destroy(gameObject, 1f);
                }
                bloodImage.fillAmount = bloodNow / blood;
            }
        }


    }
}

