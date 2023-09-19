using Model_scene_1;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

namespace Model_scene_2
{
    public class Role_Control_Magic : MonoBehaviour
    {
        #region 資料
        #region 角色整體變數
        public enum Status { Idle, Walk, Jump, Attack, Skill_J } //狀態列舉
        Status status_now; //目前的狀態
        public Status Status_Now => status_now; //讓別的腳本也能獲得目前的狀態
        private Rigidbody2D rb;
        private float global_timer = 0; //整體的計時器
        #endregion
        #region 角色物件
        //根據不同方向使用的物件
        GameObject role_now;
        GameObject role_front;
        GameObject role_back;
        GameObject role_side;
        #endregion
        #region 方向
        private int x_direction = 0; //x軸速度方向-1,0,1
        private int change_x;  //x軸加速度方向 -1,0,1
        private int y_direction = 0;//y軸速度方向-1,0,1
        private int change_y;  //y軸加速度方向 -1,0,1
        #endregion
        #region 移動參數
        private Vector2 oblique_acceleration = new Vector3(3.54f, 3.54f); //當移動方向為斜向時的加速度值
        private Vector2 x_acceleration = new Vector3(5f, 0); //當移動方向為x軸的加速度值
        private Vector2 y_acceleration = new Vector3(0, 5f); //當移動方向為y軸的加速度值
        private Vector2 change_acceleration; //存取目前的加速度值
        private float resistance = 2.4f; //為了讓速度達到一定數值就無法再增加，
                                         //會以目前速度/resistance得到阻力的加速度，
                                         //因此當速度達到5*2.4=12就會無法再增加速度(因為加速度最大為5)
        #endregion
        #region 跳躍參數
        //跳躍的加速度數值
        private Vector2 oblique_jump_acceleration = new Vector3(4.24f, 4.24f);
        private Vector2 x_jump_acceleration = new Vector3(6f, 0);
        private Vector2 y_jump_acceleration = new Vector3(0, 6f);
        //跳躍阻力，先減少對應的速度再加上加速度
        private float jump_resistance = 6.25f;
        private float oblique_jump_resistance = 4.42f;
        //跳躍時間
        private float jump_time = 0.6f;
        //計算跳躍時間的計時器
        private float jump_timer = 0;
        //跳躍是否已經執行
        private bool jump_happend = false;
        #endregion
        #region 攻擊參數
        private float attack_time = 0.6f;
        private float attack_timer = 0f;
        private float time_send_attack = 0.3f; //根據動畫調整攻擊判斷時間
        private bool canAttack = true;
        Collider2D hit = null;
        Rigidbody2D hit_rb;
        Enemy_base_Control hit_script;
        Transform hit_base;
        #endregion
        #region 攻擊範圍
        [SerializeField]
        private Vector3 attack_size_front = new Vector3(1.5f, 0.5f, 0);
        [SerializeField]
        private Vector3 attack_offset_front = new Vector3(0, -0.25f, 0);
        [SerializeField]
        private Vector3 attack_size_side = new Vector3(0.5f, 1.5f, 0);
        [SerializeField]
        private Vector3 attack_offset_side = new Vector3(-0.75f, 0.7f, 0);
        [SerializeField]
        private Vector3 attack_size_back = new Vector3(1.5f, 0.5f, 0);
        [SerializeField]
        private Vector3 attack_offset_back = new Vector3(0, 1.6f, 0);
        [SerializeField]
        private LayerMask layer_target; 
        #endregion
        #region 道具欄參數
        private bool isTouch = false;
        private GameObject itemObject = null;
        public Inventory myBag;
        #endregion
        #region 技能J參數
        private Vector2 oblique_skill_acceleration = new Vector3(8.49f, 8.49f);
        private float skill_acceleration = 12f;
        private float skill_j_time = 0.1f;
        private float skill_j_timer = 0;
        private float skill_j_cooling_time = 1f;
        private float skill_j_cooling_timer = 0;
        private bool can_skill_j = true;
        private float skill_j_consume = 10f;
        #endregion
        #region 魔力值參數
        private float magic = 100f;
        private float magic_now = 100f;
        [SerializeField]
        private Image magic_image;
        #endregion
        #region 生命值值參數
        //private float blood = 100f;
        //private float blood_now = 100f;
        [SerializeField]
        private Image blood_image;
        #endregion
        #endregion
        #region 事件
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            //Gizmos.DrawCube(transform.position + transform.TransformDirection(attack_offset_front), attack_size_front);
            //Gizmos.DrawCube(transform.position + transform.TransformDirection(attack_offset_side), attack_size_side);
            //Gizmos.DrawCube(transform.position + transform.TransformDirection(attack_offset_back), attack_size_back);
        }
        private void Start()
        {
            #region 角色整體變數
            rb = GetComponent<Rigidbody2D>();
            status_now = Status.Idle;
            #endregion
            //取得正面、側面、背面的角色物件，預設為正面
            role_front = transform.GetChild(0).gameObject;
            role_back = transform.GetChild(1).gameObject;
            role_side = transform.GetChild(2).gameObject;
            role_now = role_front;
            layer_target = LayerMask.GetMask("Enemy");
        }
        private void Update()
        {
            PickUp();
        }
        private void FixedUpdate()
        {
            global_timer += Time.fixedDeltaTime;
            RunStatus();
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Prop")
            {
                isTouch = true;
                itemObject = collision.gameObject;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Prop")
            {
                isTouch = false;
                itemObject = null;
            }
        }
        #endregion
        #region 方法
        /// <summary>
        /// 根據不同的狀態執行相應的行為
        /// </summary>
        void RunStatus()
        {
            if (status_now == Status.Idle)
                RunIdleStatus();
            else if (status_now == Status.Walk)
                RunWalkStatus();
            else if (status_now == Status.Jump)
                RunJumpStatus();
            else if (status_now == Status.Attack)
                RunAttackStatus();
            else if (status_now == Status.Skill_J)
                RunSkillJStatus();
        }
        /// <summary>
        /// 當狀態是等待時進行的動作
        /// </summary>
        void RunIdleStatus()
        {
            //獲得目前速度方向、加速度方向及數值
            ChangeAcceleration();
            if (Input.GetKeyDown(KeyCode.J) && SkillJFire())
                status_now = Status.Skill_J;
            else if (Input.GetKeyDown(KeyCode.H))
                status_now = Status.Attack;
            else if (Input.GetKeyDown(KeyCode.Space))
                status_now = Status.Jump;
            else if (change_x != 0 || change_y != 0)
                status_now = Status.Walk;
        }
        /// <summary>
        /// 當狀態是走路/跑步時進行的動作
        /// </summary>
        void RunWalkStatus()
        {
            ChangeAcceleration();

            if (Input.GetKeyDown(KeyCode.J) && SkillJFire())
            {
                status_now = Status.Skill_J;
                return;
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                status_now = Status.Attack;
                return;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                status_now = Status.Jump;
                return;
            }
            else if (rb.velocity != Vector2.zero)
                status_now = Status.Walk;
            else
                status_now = Status.Idle;


            ChangeDirect();
            ChangeVelocity();
        }
        /// <summary>
        /// 當狀態是跳躍時進行的動作
        /// </summary>
        void RunJumpStatus()
        {
            //跳躍計時器開始計算
            jump_timer += Time.fixedDeltaTime;
            //若跳躍還未執行
            if (!jump_happend)
            {
                jump_happend = true;
                //若沒有輸入方向則原地跳躍
                if (change_x == 0 && change_y == 0)
                {
                    return;
                }
                else if (change_x != 0 && change_y != 0)//當跳躍方向為斜向
                {
                    //處理x軸加速度
                    //當速度小於阻力，則先將速度歸零否則減少阻力的加速度
                    if (Mathf.Abs(rb.velocity.x) <= oblique_jump_resistance)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        x_direction = 0;
                    }
                    else
                    {
                        rb.velocity -= new Vector2(x_direction * oblique_jump_resistance, rb.velocity.y);
                    }
                    //處理y軸加速度
                    if (Mathf.Abs(rb.velocity.y) <= oblique_jump_resistance)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                        y_direction = 0;
                    }
                    else
                    {
                        rb.velocity -= new Vector2(rb.velocity.x, y_direction * oblique_jump_resistance);
                    }
                    //加上跳躍加速度
                    rb.velocity += new Vector2(change_x * oblique_jump_acceleration.x, change_y * oblique_jump_acceleration.y);
                }
                else if (change_x != 0) //當跳躍方向為左右
                {
                    //當速度小於阻力，則先將速度歸零否則減少阻力的加速度
                    if (Mathf.Abs(rb.velocity.x) <= jump_resistance)
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        x_direction = 0;
                    }
                    else
                    {
                        rb.velocity -= new Vector2(x_direction * jump_resistance, rb.velocity.y);
                    }
                    //加上跳躍加速度
                    rb.velocity += x_jump_acceleration * change_x;
                }
                else if (change_y != 0) //當跳躍方向為上下
                {
                    //當速度小於阻力，則先將速度歸零否則減少阻力的加速度
                    if (Mathf.Abs(rb.velocity.y) <= jump_resistance)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                    }
                    else
                    {
                        rb.velocity -= new Vector2(rb.velocity.x, y_direction * jump_resistance);
                    }
                    //加上跳躍加速度
                    rb.velocity += y_jump_acceleration * change_y;
                }
                //根據速度的改變，決定要不要更改角色方向
                ChangeDirect();
            }
            else if(jump_timer >= jump_time) //超過跳躍時間則回到走路狀態，並重置參數
            {
                jump_timer = 0;
                status_now = Status.Walk;
                jump_happend = false;
            }
            
            if (Input.GetKeyDown(KeyCode.J) && SkillJFire())
            {
                status_now = Status.Skill_J;
                jump_timer = 0;
                jump_happend = false;
                return;
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                jump_timer = 0;
                status_now = Status.Attack;
                jump_happend = false;
                return;
            }
        }
        /// <summary>
        /// 當狀態是攻擊時進行的動作
        /// </summary>
        void RunAttackStatus()
        {
            //攻擊計時器開始計時
            attack_timer += Time.fixedDeltaTime;
            //當時間超過攻擊判定時間，若尚未攻擊判斷，進行攻擊判斷
            if (attack_timer >= time_send_attack && canAttack)
            {
                canAttack = false;
                if (AttackTarget()) //若攻擊範圍內有怪物
                    AttackDeal();
            }
            else if (attack_timer >= attack_time) //當超過攻擊時間，重置攻擊參數，回到走路狀態
            {
                attack_timer = 0;
                canAttack = true;
                status_now = Status.Walk;
            }
        }

        /// <summary>
        /// 當狀態是Skill_J時進行的動作
        /// </summary>
        void RunSkillJStatus()
        {
            //Skill J計時器開始計時
            skill_j_timer += Time.fixedDeltaTime;
            //若尚未執行
            if (can_skill_j)
            {
                can_skill_j = false;
                magic_now -= skill_j_consume; //魔力消耗
                magic_image.fillAmount = magic_now / magic; //調整魔力顯示
                                                            //根據加速度方向調整速度
                if (change_x != 0 && change_y != 0)
                {
                    rb.velocity += new Vector2(oblique_skill_acceleration.x * change_x, oblique_skill_acceleration.y * change_y);
                }
                else if (change_x != 0)
                {
                    rb.velocity += new Vector2(change_x * skill_acceleration, rb.velocity.y);
                }
                else if (change_y != 0)
                {
                    rb.velocity += new Vector2(rb.velocity.x, change_y * skill_acceleration);
                }
            }
            //當Skill J執行完畢回到走路狀態並開始計算冷卻時間
            else if (skill_j_timer >= skill_j_time)
            {
                can_skill_j = true;
                skill_j_timer = 0;
                skill_j_cooling_timer = global_timer;
                status_now = Status.Walk;
            }
        }
        /// <summary>
        /// 獲得目前的速度方向、加速度方向及數值
        /// </summary>
        private void ChangeAcceleration()
        {
            //根據目前的速度獲得目前的速度方向
            if (rb.velocity.x > 0)
                x_direction = 1;
            else if (rb.velocity.x == 0)
                x_direction = 0;
            else if (rb.velocity.x < 0)
                x_direction = -1;

            if (rb.velocity.y > 0)
                y_direction = 1;
            else if (rb.velocity.y == 0)
                y_direction = 0;
            else if (rb.velocity.y < 0)
                y_direction = -1;

            //根據輸入獲得加速度方向以及要使用的加速度值
            //使用WASD鍵或著方向鍵
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            {
                change_x = -1;
                change_y = 1;
                change_acceleration = oblique_acceleration;
            }
            else if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
            {
                change_x = -1;
                change_y = -1;
                change_acceleration = oblique_acceleration;
            }
            else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
            {
                change_x = 1;
                change_y = 1;
                change_acceleration = oblique_acceleration;
            }
            else if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                && (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
            {
                change_x = 1;
                change_y = -1;
                change_acceleration = oblique_acceleration;
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                change_x = -1;
                change_y = 0;
                change_acceleration = x_acceleration;
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                change_x = 1;
                change_y = 0;
                change_acceleration = x_acceleration;
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                change_x = 0;
                change_y = 1;
                change_acceleration = y_acceleration;
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                change_x = 0;
                change_y = -1;
                change_acceleration = y_acceleration;
            }
            else
            {
                change_x = 0;
                change_y = 0;
            }
        }
        /// <summary>
        /// 根據加速度改變當前速度
        /// </summary>
        void ChangeVelocity()
        {
            //當沒有加速度時
            if (change_x == 0 && change_y == 0)
            {
                rb.velocity -= rb.velocity / resistance * Time.fixedDeltaTime;
                //因為速度的減少是根據當前速度的比值，當速度很小時，減少幅度會很少，一直到不了0
                //因此當低於一定數值(在此為4)時就將速度歸零
                if (Mathf.Abs(rb.velocity.x) < 4)
                    rb.velocity = new Vector2(0, rb.velocity.y);

                if (Mathf.Abs(rb.velocity.y) < 4)
                    rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            //處理x軸方向加速度
            if (change_x + x_direction == 0)
            {
                //當加速度方向與目前速度方向相反時，改變的數值為7，小於7則歸零
                if (Mathf.Abs(rb.velocity.x) < 7)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    x_direction = 0;
                }
                else
                {
                    rb.velocity += new Vector2(7 * change_x, 0);
                }
            }
            else if (change_x == 0)
            {
                //當低於一定數值(在此為4)時就將速度歸零
                if (Mathf.Abs(rb.velocity.x) < 4f)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    x_direction = 0;
                }
            }
            else
            {
                //當加速度方向不為0或著與當前速度方向相反時
                //將速度方向設定為加速度方向並依照加速度數值調整速度
                x_direction = change_x;
                rb.velocity += new Vector2(change_acceleration.x * Time.fixedDeltaTime * change_x, 0);
            }

            //處理y軸方向加速度
            if (change_y + y_direction == 0)
            {
                //當加速度方向與目前速度方向相反時，改變的數值為7，小於7則歸零
                if (Mathf.Abs(rb.velocity.y) < 7)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    y_direction = 0;
                }
                else
                {
                    rb.velocity += new Vector2(0, 7 * change_y);
                }
            }
            else if (change_y == 0)
            {
                //當低於一定數值(在此為4)時就將速度歸零
                if (Mathf.Abs(rb.velocity.y) < 4f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    y_direction = 0;
                }
            }
            else
            {
                //當加速度方向不為0或著與當前速度方向相反時
                //將速度方向設定為加速度方向並依照加速度數值調整速度
                y_direction = change_y;
                rb.velocity += new Vector2(0, change_acceleration.y * Time.fixedDeltaTime * change_y);
            }

            //根據目前速度減少一定比例的速度
            rb.velocity -= (rb.velocity / resistance * Time.fixedDeltaTime);
        }
        /// <summary>
        /// 更改角色方向
        /// </summary>
        void ChangeDirect()
        {
            //當現在的速度方向準備轉向時(速度為0時)判斷是否要更改角色方向
            if (x_direction == 0 || y_direction == 0)
            {
                //根據加速度方向決定要如何轉向
                //以側面為優先，若現在為向左(右)但加速度方向為向右(左)則將物件翻面
                //若現在不是側面，但x軸加速度方向不為0，則將物件更改為側面並根據加速度方向設定左右旋轉度
                //若x軸加速度為0，y軸加速度方向為1則將物件更改為背面，y軸加速度方向為-1則更改為正面
                if (change_x == -1 && role_now == role_side && transform.rotation.y != 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0f, 0);
                }
                else if (change_x == -1)
                {
                    if (role_now != role_side)
                    {
                        role_now.SetActive(false);
                        role_now = role_side;
                        role_now.SetActive(true);
                    }
                }
                else if (change_x == 1 && role_now == role_side && transform.rotation.y == 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180f, 0);
                }
                else if (change_x == 1)
                {
                    if (role_now != role_side)
                    {
                        role_now.SetActive(false);
                        role_now = role_side;
                        transform.rotation = Quaternion.Euler(0, 180f, 0);
                        role_now.SetActive(true);
                    }

                }
                else if (change_y == -1)
                {
                    if (role_now != role_front)
                    {
                        role_now.SetActive(false);
                        role_now = role_front;
                        transform.rotation = Quaternion.Euler(0, 0f, 0);
                        role_now.SetActive(true);
                    }
                }
                else if (change_y == 1)
                {
                    if (role_now != role_back)
                    {
                        role_now.SetActive(false);
                        role_now = role_back;
                        transform.rotation = Quaternion.Euler(0, 0f, 0);
                        role_now.SetActive(true);
                    }
                }
            }
        }
        /// <summary>
        /// 攻擊判斷
        /// </summary>
        /// <returns>是否有攻擊到敵人</returns>
        bool AttackTarget()
        {
            //根據不同方向調整偵測範圍
            if (role_now == role_front)
            {
                hit = Physics2D.OverlapBox(transform.position + transform.TransformDirection(attack_offset_front), attack_size_front, 0, layer_target);
            }
            else if (role_now == role_back)
            {
                hit = Physics2D.OverlapBox(transform.position + transform.TransformDirection(attack_offset_back), attack_size_back, 0, layer_target);
            }
            else if (role_now == role_side)
            {
                hit = Physics2D.OverlapBox(transform.position + transform.TransformDirection(attack_offset_side), attack_size_side, 0, layer_target);
            }
            
            //獲得需要的參數
            if(hit)
            {
                hit_rb = hit.GetComponentInParent<Rigidbody2D>();
                hit_script = hit.GetComponentInParent<Enemy_base_Control>();
                hit_base = hit.GetComponentInParent<Transform>();
            }
            return hit;
        }
        void AttackDeal()
        {
            
        }
        /// <summary>
        /// Skill J技能使用判定
        /// </summary>
        /// <returns>是否可以執行Skill J</returns>
        bool SkillJFire()
        {
            if (magic_now < skill_j_consume)
                return false;
            if (global_timer - skill_j_cooling_timer < skill_j_cooling_time)
                return false;
            if(change_x == 0 && change_y == 0)
                return false;
            return true;
        }
        private void PickUp()
        {
            if (isTouch && itemObject != null && Input.GetKeyDown(KeyCode.G))
            {
                Item item = itemObject.GetComponent<ItemOnWorld>().thisItem;

                int index = myBag.itemList.FindIndex(x => x == item);
                if (index != -1)
                    myBag.itemHold[index]++;
                else
                {
                    myBag.itemList.Add(item);
                    myBag.itemHold.Add(1);
                }

                Destroy(itemObject);
                Inventory_Manager.Refresh();
            }
        }
        #endregion
    }
}

