using UnityEngine;

namespace Model_scene_3
{
    public class Enemy_Generate_Control : MonoBehaviour
    {
        private GameObject target;
        [SerializeField, Header("Rino")]
        private GameObject prefab_rino;

        private float timer = 0;
        private bool can_generated = true;

        private void Start()
        {
            target = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            timer += Time.deltaTime;
            transform.position = target.transform.position;

            if(timer > 20)
            {
                can_generated = true;
                timer = 0;
            }

            if(can_generated) 
            { 
                First_Generate(); 
                can_generated = false;
            }

        }

        void First_Generate()
        {
            Enemy_Generate(prefab_rino, 10);
        }

        void Enemy_Generate(GameObject prefab_enemy, int number)
        {
            for(int i = 0; i < number; i++)
            {
                float x = Random.Range(0, 10);
                if (x < 5) x -= 10;
                float y = Random.Range(0, 10);
                if (y < 5) y -= 10;
                Vector3 enemy_position = new Vector3(x, y,0);
                Instantiate(prefab_enemy, transform.position + enemy_position, Quaternion.identity);
            }
            

        }



    }

}
