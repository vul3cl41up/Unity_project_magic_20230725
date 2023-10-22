using System.Collections;
using UnityEngine;

namespace magic
{
    public class Skill_icicle : Skill_Base
    {
        bool can_attack = true;
        private float chain_probability = 0.3f;
        [SerializeField, Header("¦BÀ@¹w¸mª«")]
        private GameObject icicle_prefab;
        public int times = 2;
        private void Start()
        {
            Random.InitState(Random.Range(0, int.MaxValue));
            Random.InitState(Random.Range(0, int.MaxValue));
            Random.InitState(Random.Range(0, int.MaxValue));
            Random.InitState(Random.Range(0, int.MaxValue));
        }
        private void FixedUpdate()
        {
            Random.InitState(Random.Range(0, int.MaxValue));
            transform.position +=  transform.rotation * new Vector3(8f * Time.fixedDeltaTime, 0, 0);
        }
        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemy")&&can_attack)
            {
                can_attack = false;
                StartCoroutine(Start_Attack(collision));
            }
        }
        
        protected override IEnumerator Start_Attack(Collider2D collision)
        {
            yield return base.Start_Attack(collision);
            if (Random.Range(0f,1f) <= chain_probability && times >= 0)
            {
                Vector3 generate_point = collision.transform.position;
                GameObject new_object = Instantiate(icicle_prefab, generate_point + new Vector3(1, 0, 0), Quaternion.identity);
                new_object.GetComponent<Skill_icicle>().times--;
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(1, 1, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 45);
                new_object.GetComponent<Skill_icicle>().times--;
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(0, 1, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 90);
                new_object.GetComponent<Skill_icicle>().times--;
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(-1, 1, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 135);
                new_object.GetComponent<Skill_icicle>().times--;
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(-1, 0, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 180);
                new_object.GetComponent<Skill_icicle>().times--;
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(-1, -1, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 225);
                new_object.GetComponent<Skill_icicle>().times--;
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(-1, 0, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 270);
                new_object.GetComponent<Skill_icicle>().times--;
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(1, -1, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 315);
                new_object.GetComponent<Skill_icicle>().times--;
            }
            End();
        }
    }
}


