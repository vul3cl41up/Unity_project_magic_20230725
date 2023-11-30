using Model_scene_1;
using Model_scene_3;
using System.Collections;
using TMPro;
using UnityEngine;

namespace magic
{
    public class Role_Control : MonoBehaviour
    {
        #region 資料
        [SerializeField, Header("角色資料檔案")]
        private Role_Data role_data_file;
        [SerializeField, Header("角色的畫布")]
        Transform role_canvas;
        [SerializeField, Header("受傷血量預置物")]
        GameObject damage_text;
        [SerializeField, Header("結束面板")]
        GameObject end_panel;

        private Rigidbody2D rb;
        private Animator animator;
        private SpriteRenderer sprite_renderer;
        public Vector2 move_input { get; private set; }

        public float hpMax { get; private set; }
        public float hp { get; private set; }
        private float move_speed;

        bool is_dead = false;
        bool can_move = true;
        #endregion

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            sprite_renderer = GetComponent<SpriteRenderer>();
            hpMax = role_data_file.Hp;
            hp = hpMax;
            move_speed = role_data_file.move_speed;

        }
        private void Update()
        {
            MoveAnimationAndFlip();
        }
        private void FixedUpdate()
        {
            //每固定時間調整移動速度
            Move();
        }
        private void Move()
        {
            if(can_move)
            {
                //取得輸入
                move_input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
                //標準化讓各方向移動數值相同
                move_input = move_input.normalized;
                rb.velocity = move_input * move_speed;
            }
            
        }

        

        public void Take_Attack(float damage)
        {
            if (!is_dead)
            {
                hp -= damage;
                InjuriedAnimation(damage);
                if (hp <= 0)
                    StartCoroutine(Dead());
            }
        }
        IEnumerator Dead()
        {
            rb.velocity = Vector3.zero;
            is_dead = true;
            DeadAnimation();
            enabled = false;
            yield return new WaitForSeconds(1f);
            end_panel.SetActive(true);
            end_panel.GetComponent<End_Panel>().Change_Result_Text("你已死亡!");
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

