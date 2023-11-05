using UnityEngine;

namespace magic
{
    public class Range_Enemy : Enemy_Base
    {
        [SerializeField, Header("§ðÀ»¶ZÂ÷")]
        float attack_range = 10f;
        [SerializeField, Header("§ðÀ»¹w¸mª«")]
        GameObject attack_prefab;

        Animator ani;

        private float attack_timer = 0;
        private float attack_cool_time;

        int move_delay = 30;
        int move_count = 0;
        bool start_attack = false;
        float angle;
        

        protected override void Start()
        {
            base.Start();
            ani = GetComponent<Animator>();
            attack_cool_time = enemy_data.attack_cool_time;
        }
        protected override void Update()
        {
            base.Update();
            Attack();
        }

        private void Attack()
        {
            attack_timer += Time.deltaTime;
            if (start_attack)
            {
                if (attack_timer >= attack_cool_time)
                {
                    ani.SetTrigger("attack_trigger");
                    Instantiate(attack_prefab, transform.position + Quaternion.Euler(0,0,angle)* new Vector3(0.5f,0,0), Quaternion.Euler(0, 0, angle));
                    attack_timer = 0;
                }
            }
            
        }
        protected override void Tracking()
        {
            move_count++;
            Vector2 diff = new Vector2(target.transform.position.x - this.transform.position.x, target.transform.position.y - this.transform.position.y+0.8f);
            if(diff.y >0)
                angle = -Vector2.Angle(Vector2.left,diff);
            else
                angle = Vector2.Angle(Vector2.left, diff);


            if (diff.magnitude <=attack_range)
            {
                start_attack = true;
            }
            else
            {
                start_attack=false;
            }

            if (diff.magnitude > 5 && move_count >= move_delay)
            {
                rb.velocity = diff.normalized * move_speed;
                move_count = 0;
            }
            else if(diff.magnitude <= 5 && move_count>= move_delay) 
            {
                rb.velocity = Vector2.zero;
                move_count = 0;
            }

        }
    }

}

