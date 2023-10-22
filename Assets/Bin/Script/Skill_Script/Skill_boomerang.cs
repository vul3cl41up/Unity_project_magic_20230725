using Fungus;
using UnityEngine;
namespace magic
{
    public class Skill_boomerang : Skill_Base
    {
        [SerializeField, Header("²¾°Ê³t«×")]
        private float speed = 10f;
        bool comeback = false;
        float distance = 0f;
        private GameObject target;
        private Rigidbody2D rb;
        private void Start()
        {
            target = GameObject.FindWithTag("Player");
            rb = gameObject.GetComponent<Rigidbody2D>();
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


