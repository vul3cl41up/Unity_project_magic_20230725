using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

namespace Model_scene_3
{
    public class Enemy_Generate_Control : MonoBehaviour
    {
        private GameObject target;
        [SerializeField, Header("產生的怪物")]
        private List<GameObject> prefab_List;
        [SerializeField, Header("生成的時間")]
        private float time;
        [SerializeField, Header("生成的數量")]
        private int number;
        [Header("與角色的距離")]
        [SerializeField]
        private float from;
        [SerializeField]
        private float to;


        private float timer = 0;
        bool can_ganerate = true;

        private void Start()
        {
            target = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            timer += Time.deltaTime;

            if(timer >= time && can_ganerate)
            {
                for(int i = 0; i < prefab_List.Count; i++) 
                {
                    Enemy_Generate(prefab_List[i], number);
                    can_ganerate = false;
                    Destroy(gameObject);
                }
                
            }
                

        }


        void Enemy_Generate(GameObject prefab_enemy, int number)
        {
            for(int i = 0; i < number; i++)
            {
                float x = Random.Range(0, to);
                if (x < from) x -= to;
                float y = Random.Range(0, to);
                if (y < from) y -= to;
                Vector3 enemy_position = new Vector3(x, y,0);
                Instantiate(prefab_enemy, target.transform.position + enemy_position, Quaternion.identity);
            }
            

        }



    }

}
