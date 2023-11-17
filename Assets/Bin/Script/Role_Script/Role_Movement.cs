using UnityEngine;

namespace magic
{
    public class Role_Movement : MonoBehaviour
    {
        [SerializeField, Header("角色資料檔案")]
        private Role_Data role_data;

        private Rigidbody2D rb;
        private Animator animator;
        private SpriteRenderer sprite_renderer;

        public Vector2 move_input { get; private set; }
        private float move_speed;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            sprite_renderer = GetComponent<SpriteRenderer>();
            move_speed = role_data.move_speed;
        }

        private void Update()
        {
            MoveAnimation();
            Flip();
        }

        private void FixedUpdate()
        {
            //每固定時間調整移動速度
            Move();
        }

        private void Move()
        {
            //取得輸入
            move_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
            //標準化讓各方向移動數值相同
            move_input = move_input.normalized;
            rb.velocity = move_input * move_speed;
        }

        void MoveAnimation()
        {
            if (rb.velocity.magnitude > 0)
                animator.SetBool("is_run", true);
            else
                animator.SetBool("is_run", false);
        }
        void Flip()
        {
            if (move_input.x < 0)
            {
                sprite_renderer.flipX = true;
            }
            else if (move_input.x > 0)
            {
                sprite_renderer.flipX = false;
            }
        }

    }

}
