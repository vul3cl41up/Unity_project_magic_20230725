using UnityEngine;

namespace magic
{
    public class Range_Enemy_Attack : MonoBehaviour
    {
        [SerializeField, Header("攻擊移動速度")]
        float move_speed = 7f;
        [SerializeField, Header("敵人資料")]
        Enemy_Data enemy_data;

        Rigidbody2D rb;
        float timer = 0f;
        bool done = false;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = transform.rotation * Vector3.left * move_speed;
        }

        private void Update()
        {
            timer += Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player") && !done)
            {
                collision.gameObject.GetComponent<Role_Control>().Take_Attack(enemy_data.attack_damage);
                Destroy(gameObject, 0.2f);
                done = true;
            }
        }

        public void Long_Time()
        {
            if(timer >= 6)
            {
                Destroy(gameObject);
            }
        }
    }

}
