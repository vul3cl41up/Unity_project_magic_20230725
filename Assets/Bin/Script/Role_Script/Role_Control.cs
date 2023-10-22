using Model_scene_1;
using Model_scene_3;
using TMPro;
using UnityEngine;

namespace magic
{
    public class Role_Control : MonoBehaviour
    {
        #region ���
        [SerializeField, Header("�������ɮ�")]
        private Role_Data role_data_file;
        [SerializeField, Header("���⪺�e��")]
        Transform role_canvas;
        [SerializeField, Header("���˦�q�w�m��")]
        GameObject damage_text;
        public Role_Data role_data { get; private set; }
        private Rigidbody2D rb;
        private Animator animator;
        private SpriteRenderer sprite_renderer;
        public Vector2 move_input { get; private set; }

        private float hpMax;
        private float hp;
        #endregion

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            sprite_renderer = GetComponent<SpriteRenderer>();
            role_data = role_data_file;

            hpMax = role_data.Hp;
            hp = hpMax;
        }
        private void Update()
        {
            MoveAnimationAndFlip();
        }
        private void FixedUpdate()
        {
            //�C�T�w�ɶ��վ㲾�ʳt��
            Move();
        }
        private void Move()
        {
            //���o��J
            move_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
            //�зǤ����U��V���ʼƭȬۦP
            move_input = move_input.normalized;
            rb.velocity = move_input * role_data.move_speed;
        }

        public void Take_Attack(float damage)
        {
            hp -= damage;
            InjuriedAnimation(damage);
            if(hp <= 0)
            {
                Dead();
            }

        }
        void Dead()
        {
            rb.velocity = Vector3.zero;
            DeadAnimation();
            gameObject.SetActive(false);
        }
        void DeadAnimation()
        {
            animator.SetBool("is_dead", true);
        }
        void InjuriedAnimation(float damage)
        {
            GameObject getDamage = Instantiate(damage_text, role_canvas.position, Quaternion.identity, role_canvas);
            getDamage.GetComponent<TMP_Text>().text = damage.ToString();
            Destroy(getDamage, 1f);
        }
        void MoveAnimationAndFlip()
        {
            if (move_input.x<0)
            {
                sprite_renderer.flipX = true;
            }
            else if (move_input.x > 0)
            {
                sprite_renderer.flipX = false;
            }

            if (rb.velocity.magnitude > 0)
                animator.SetBool("is_run", true);
            else
                animator.SetBool("is_run", false);
        }
    }
}

