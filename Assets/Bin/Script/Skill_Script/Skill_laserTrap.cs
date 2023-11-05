using UnityEngine;

namespace magic
{
    public class Skill_laserTrap : Skill_Base
    {
        private float duration;
        [SerializeField, Header("攻擊間隔")]
        private float interval = 0.1f;
        [SerializeField, Header("移動速度")]
        private float move_speed = 3f;
        [SerializeField, Header("移動距離")]
        private float move_distance = 3f;

        bool can_attack = false;
        float attack_timer = 0f;
        bool end = false;
        float distance = 0f;
        bool is_right = true;

        Rigidbody2D rb;
        private void Start()
        {
            duration = skill_data.last_time;
            rb = GetComponent<Rigidbody2D>();
        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {

        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                if (can_attack)
                {
                    StartCoroutine(Start_Attack(collision));
                }
            }
        }
        private void Update()
        {
            duration -= Time.deltaTime;
            attack_timer += Time.deltaTime;
            if (duration <= 0 && !end)
            {
                end = true;
                End();
            }
            if (attack_timer >= interval + Time.deltaTime)
            {
                can_attack = false;
                attack_timer = 0f;
            }
            else if (attack_timer >= interval)
            {
                can_attack = true;
            }

        }
        private void FixedUpdate()
        {
            if (is_right)
            {
                rb.velocity = move_speed * transform.right;
                distance += move_speed * Time.fixedDeltaTime;
                if(distance >= move_distance)
                {
                    is_right = false;
                }
            }
            else
            {
                rb.velocity = -move_speed * transform.right;
                distance -= move_speed * Time.fixedDeltaTime;
                if (distance <= -move_distance)
                {
                    is_right = true;
                }
            }
        }
    }

}
