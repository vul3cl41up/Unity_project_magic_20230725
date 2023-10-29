using UnityEngine;

namespace magic
{
    public class Skill_ShockWave : Skill_Base
    {
        [SerializeField, Header("預置物")]
        GameObject prefab;
        [SerializeField, Header("移動速度")]
        private float speed = 10f;
        private void FixedUpdate()
        {
            transform.position += transform.rotation * new Vector3(speed * Time.fixedDeltaTime, 0, 0);
        }
        public void Judge_Action()
        {
            if (skill_data.skill_level >= 1)
            {
                Instantiate(prefab, transform.position + transform.rotation * new Vector3(1.8f, -0.2f, 0), transform.rotation * Quaternion.Euler(0, 0, 180f));
            }
            if (skill_data.skill_level >= 2)
            {
                Instantiate(prefab, transform.position + transform.rotation * new Vector3(0.9f, 0, 0), transform.rotation * Quaternion.Euler(0, 0, 90f));
                Instantiate(prefab, transform.position + transform.rotation * new Vector3(0.9f, 0, 0), transform.rotation * Quaternion.Euler(0, 0, -90f));
            }
            if (skill_data.skill_level >= 3)
            {
                Instantiate(prefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, 45f));
                Instantiate(prefab, transform.position, transform.rotation * Quaternion.Euler(0, 0, -45f));
            }
            if (skill_data.skill_level >= 4)
            {
                Instantiate(prefab, transform.position + transform.rotation * new Vector3(1.8f, -0.2f, 0), transform.rotation * Quaternion.Euler(0, 0, 135f));
                Instantiate(prefab, transform.position + transform.rotation * new Vector3(1.8f, -0.2f, 0), transform.rotation * Quaternion.Euler(0, 0, -135f));
            }
            
            
            
        }
    }
}

