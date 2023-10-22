using System.Collections.Generic;
using UnityEngine;

namespace magic
{
    public class Enemy_Generate : MonoBehaviour
    {
        private GameObject target;
        [SerializeField, Header("產生的怪物")]
        private GameObject enemy_prefab;
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

            if (timer >= time && can_ganerate)
            {
                can_ganerate = false;
                for (int i = 0; i < number; i++)
                {
                    float x = Random.Range(0, to);
                    if (x < from) x -= to;
                    float y = Random.Range(0, to);
                    if (y < from) y -= to;
                    Vector3 enemy_position = new Vector3(x, y, 0);
                    Instantiate(enemy_prefab, target.transform.position + enemy_position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }



    }
}

