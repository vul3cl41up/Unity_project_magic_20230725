using UnityEngine;

namespace magic
{
    public class Thunder_Effect : MonoBehaviour
    {
        Rigidbody2D rb;
        Animator ani;
        public float stop_time;
        float timer = 0;
        private void OnEnable()
        {
            rb = gameObject.GetComponentInParent<Rigidbody2D>();
            rb.simulated = false;
            ani = gameObject.GetComponentInParent<Animator>();
            ani.enabled = false;
            print(stop_time);
        }

        private void OnDisable()
        {
            rb.simulated = true;
            ani.enabled = true;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            print(timer);
            if(timer >= stop_time)
            {
                Destroy(gameObject);
            }
        }
    }

}
