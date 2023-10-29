using UnityEngine;

namespace magic
{
    public class Bouns : MonoBehaviour
    {
        GameObject target;
        bool touch = false;
        Rigidbody2D rb;
        float move_speed = 10f;
        bool done = false;
        Pick_Up pick_up;

        private void Start()
        {
            target = GameObject.FindWithTag("Player");
            rb = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("pickup"))
            {
                touch = true;
                pick_up = collision.GetComponent<Pick_Up>();
            }
            if (collision.CompareTag("Player") && !done)
            {
                Destroy(gameObject, 0.2f);
                pick_up.get_bonus(1);
                done = true;
            }
        }
        private void Update()
        {
            if(touch)
            {
                Vector2 diff = new Vector2(target.transform.position.x - this.transform.position.x, target.transform.position.y - this.transform.position.y+0.8f);
                rb.velocity = diff.normalized * move_speed;
            }
        }

    }
}

