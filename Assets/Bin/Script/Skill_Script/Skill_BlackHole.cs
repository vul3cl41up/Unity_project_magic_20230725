using UnityEngine;

namespace magic
{
    public class Skill_BlackHole : MonoBehaviour
    {
        [SerializeField, Header("移動速度")]
        float move_speed = 8f;
        [SerializeField, Header("預置物")]
        GameObject prefab;
        [SerializeField, Header("持續時間")]
        float last_time = 4.1f;

        float attack_interval = 0.8f;
        float attack_timer = 0f;
        float timer = 0f;

        GameObject target = null;
        Rigidbody2D rb;

        bool can_attack = false;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            timer += Time.deltaTime;
            if (timer > last_time) { Destroy(gameObject); }
            attack_timer += Time.deltaTime;
            if (attack_timer >= attack_interval && can_attack)
            {
                Instantiate(prefab, transform.position+new Vector3(0,0.1f,0), Quaternion.identity, transform);
                attack_timer = 0f;
            }

            if (!target)
            {
                Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 10f);
                if (cols != null)
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if (cols[i].CompareTag("Enemy")) //cols[i].gameObject.GetComponent<Enemy_Base_Control>().Take_Damage(role_control, skill_type);
                        {
                            target = cols[i].gameObject;
                            break;
                        }
                    }
                can_attack = false;
            }

            if (target)
            {
                Vector2 diff = new Vector2(target.transform.position.x - this.transform.position.x, target.transform.position.y - this.transform.position.y - 0.5f);
                rb.velocity = diff.normalized * move_speed;
                if(diff.magnitude < 1f)
                    can_attack = true;
            }



            
        }
    }
}

