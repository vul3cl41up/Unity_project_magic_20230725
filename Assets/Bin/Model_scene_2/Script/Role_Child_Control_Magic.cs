using UnityEngine;

namespace Model_scene_2
{
    public class Role_Child_Control_Magic : MonoBehaviour
    {
        #region 資料
        Role_Control_Magic parent_script; //人物操控腳本
        GameObject ani_now; //現在使用的動畫物件
        //等待、走路、跳躍、攻擊動畫物件
        GameObject ani_idle;
        GameObject ani_walk;
        GameObject ani_jump;
        GameObject ani_attack;
        #endregion

        private void Start()
        {
            //取得人物操控腳本、等待、走路、跳躍、攻擊動畫物件
            parent_script = GetComponentInParent<Role_Control_Magic>();
            ani_idle = transform.GetChild(0).gameObject;
            ani_walk = transform.GetChild(1).gameObject;
            ani_jump = transform.GetChild(2).gameObject;
            ani_attack = transform.GetChild(3).gameObject;
            ani_now = ani_idle; //預設使用等待動畫物件
        }

        private void FixedUpdate()
        {
            ChangeAni();
        }
        /// <summary>
        /// 更改動畫
        /// </summary>
        void ChangeAni()
        {
            //根據人物操作腳本的現在狀態，若與現在的動畫不同，
            //則關閉現在物件，將現在的動畫物件更改並開啟
            if (parent_script.Status_Now == Role_Control_Magic.Status.Attack)
            {
                if (ani_now != ani_attack)
                {
                    ani_now.SetActive(false);
                    ani_now = ani_attack;
                    ani_now.SetActive(true);
                }
            }
            else if (parent_script.Status_Now == Role_Control_Magic.Status.Jump)
            {
                if (ani_now != ani_jump)
                {
                    ani_now.SetActive(false);
                    ani_now = ani_jump;
                    ani_now.SetActive(true);
                }
            }
            else if (parent_script.Status_Now == Role_Control_Magic.Status.Idle)
            {
                if (ani_now != ani_idle)
                {
                    ani_now.SetActive(false);
                    ani_now = ani_idle;
                    ani_now.SetActive(true);
                }
            }
            else if (parent_script.Status_Now == Role_Control_Magic.Status.Walk)
            {
                if (ani_now != ani_walk)
                {
                    ani_now.SetActive(false);
                    ani_now = ani_walk;
                    ani_now.SetActive(true);
                }
            }

        }
    }
}

