using UnityEngine;
namespace magic
{
    public class Skill_boomerang : Skill_Base
    {
        [SerializeField, Header("移動速度")]
        private float speed = 10f;
        [SerializeField, Header("預置物")]
        GameObject prefab;
        bool comeback = false;
        float distance = 0f;
        private GameObject target;
        private Rigidbody2D rb;
        private void Start()
        {
            target = GameObject.FindWithTag("Player");
            rb = gameObject.GetComponent<Rigidbody2D>();
        }
        public void Judge_Action()
        {
            if (skill_data.skill_level == 4)
            {
                Instantiate(prefab, transform.position + transform.rotation * new Vector3(0, 0.4f, 0), transform.rotation);
                GameObject new_skill = Instantiate(prefab, transform.position + transform.rotation * new Vector3(0, 0.4f, 0), transform.rotation);
                new_skill.transform.rotation = new_skill.transform.rotation * Quaternion.Euler(0, 0, 45f);
                new_skill = Instantiate(prefab, transform.position + transform.rotation * new Vector3(0, -0.4f, 0), transform.rotation);
                new_skill.transform.rotation = new_skill.transform.rotation * Quaternion.Euler(0, 0, -45f);
                transform.position = transform.position + transform.rotation * new Vector3(0, -0.4f, 0);
            }
            else if (skill_data.skill_level >= 1)
            {
                Instantiate(prefab, transform.position + transform.rotation * new Vector3(0, 0.4f, 0), transform.rotation);
                transform.position = transform.position + transform.rotation * new Vector3(0, -0.4f, 0);
            }
            
            
        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if (comeback)
            {
                if(collision.CompareTag("Player"))
                {
                    Destroy(gameObject, 0.2f);
                }
            }
        }
        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if(!comeback)
            {
                transform.position += transform.rotation * new Vector3(speed * Time.fixedDeltaTime, 0, 0);
                distance += speed * Time.fixedDeltaTime;
                if(distance >= 8f)
                    comeback = true;
            }
            else
            {
                Vector2 diff = new Vector2(target.transform.position.x - this.transform.position.x, target.transform.position.y - this.transform.position.y + 0.8f);
                rb.velocity = diff.normalized * speed;

            }

        }
    }
}


