using TMPro;
using UnityEngine;

namespace magic
{
    public class Enemy_Base : MonoBehaviour
    {
        [SerializeField]
        GameObject HP_Canvas;
        [SerializeField, Header("獎勵道具")]
        GameObject bonus;

        private Rigidbody2D rb;
        public Enemy_Data enemy_data;
        private GameObject target;
        Collider2D collider2d;
        Role_Control player;
        SpriteRenderer sprite_renderer;
        
        private float hpMax;
        private float hp;
        private float move_speed;

        private bool is_touch = false;

        private float attack_timer = 0;
        private float attack_cool_time;

        int flip_delay = 50;
        int count = 0;
        bool is_dead = false;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sprite_renderer = GetComponent<SpriteRenderer>();
            target = GameObject.FindWithTag("Player");
            collider2d = GetComponent<Collider2D>();

            hpMax = enemy_data.Hp;
            hp = hpMax;
            move_speed = enemy_data.move_speed;
            attack_cool_time = enemy_data.attack_cool_time;
        }
        private void OnBecameVisible()
        {
            collider2d.enabled = true;
        }
        private void OnBecameInvisible()
        {
            collider2d.enabled = false;
        }

        private void Update()
        {
            Attack();
            Flip();
        }
        private void FixedUpdate()
        {
            Tracking();
        }
        void Tracking()
        {
            Vector2 diff = new Vector2(target.transform.position.x - this.transform.position.x, target.transform.position.y - this.transform.position.y);
            rb.velocity = diff.normalized * move_speed;

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

        public void Take_Damage(float damage)
        {
            if(!is_dead)
            {
                hp -= damage;
                GameObject getDamage = Instantiate(HP_Canvas, transform.position, Quaternion.identity);
                getDamage.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                Destroy(getDamage, 1f);
                if (hp <= 0)
                {
                    Dead();
                }
            }
            
        }
        protected void Dead()
        {
            is_dead = true;
            target.GetComponent<Upgrade_System>().Get_Exp(enemy_data.exp);
            Instantiate(bonus, transform.position, Quaternion.identity); 
            Destroy(gameObject);
        }
        protected virtual void Attack()
        {
            attack_timer += Time.deltaTime;
            if (is_touch && attack_timer >= attack_cool_time)
            {
                player.Take_Attack(enemy_data.attack_damage);
                attack_timer = 0;
            }
        }
        private void Flip()
        {
            count++;
            //轉身
            if (count >= flip_delay)
            {
                if (rb.velocity.x > 0)
                {
                    sprite_renderer.flipX = true;
                    count = 0;
                }
                else if (rb.velocity.x < 0)
                {
                    sprite_renderer.flipX = false;
                    count = 0;
                }
            }

        }

    }
}

