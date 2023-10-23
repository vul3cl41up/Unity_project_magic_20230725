using UnityEngine;

namespace magic
{
    public class Skill_ShockWave : Skill_Base
    {
        [SerializeField, Header("²¾°Ê³t«×")]
        private float speed = 10f;
        private void FixedUpdate()
        {
            transform.position += transform.rotation * new Vector3(speed * Time.fixedDeltaTime, 0, 0);
        }
    }
}

