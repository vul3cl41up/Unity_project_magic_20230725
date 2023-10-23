using UnityEngine;

namespace magic
{
    public class Skill_ShockWave : Skill_Base
    {
        [SerializeField, Header("���ʳt��")]
        private float speed = 10f;
        private void FixedUpdate()
        {
            transform.position += transform.rotation * new Vector3(speed * Time.fixedDeltaTime, 0, 0);
        }
    }
}

