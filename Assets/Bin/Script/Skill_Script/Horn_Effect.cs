using UnityEngine;

namespace magic
{
    public class Horn_Effect : MonoBehaviour
    {
        [SerializeField, Header("預置物")]
        GameObject prefab;
        [SerializeField, Header("持續時間")]
        float last_time = 5.3f;
        

        float attack_interval = 1;

        float attack_timer = 0f;
        float timer = 0f;

        private void Update()
        {
            timer += Time.deltaTime;
            if(timer > last_time) { Destroy(gameObject); }
            attack_timer += Time.deltaTime;
            if(attack_timer >= attack_interval)
            {
                Instantiate(prefab, transform.position + new Vector3(0,-0.8f,0), Quaternion.identity, transform);
                attack_timer = 0f;
            }
        }

    }
}

