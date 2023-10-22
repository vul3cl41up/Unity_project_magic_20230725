using UnityEngine;
namespace magic
{
    public class Skill_longWave : Skill_Base
    {
        [SerializeField, Header("持續時間")]
        private float duration = 3f;
        [SerializeField, Header("攻擊間隔")]
        private float interval = 0.4f;

        bool can_attack = false;
        float attack_timer = 0f;
        Attack_System attack_system;
        bool end = false;
        Animator animator;

        private void Start()
        {
            attack_system = GetComponentInParent<Attack_System>();
            animator = GetComponent<Animator>();
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemy"))
            {
                if(can_attack)
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
                animator.SetTrigger("end_trigger"); 
                end = true;
            }


            if(attack_timer >= interval+Time.deltaTime)
            {
                can_attack = false;
                attack_timer = 0f;
            }
            else if(attack_timer >=interval)
            {
                can_attack = true;
            }

            transform.position = attack_system.direction_now.transform.position;
            transform.rotation = attack_system.direction_now.transform.rotation;

        }
    }
}

