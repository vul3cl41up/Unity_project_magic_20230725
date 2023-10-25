using System.Collections;
using UnityEngine;

namespace magic
{
    public class Skill_icicle : Skill_Base
    {
        bool can_attack = true;
        private int chain_probability = 300;
        [SerializeField, Header("冰錐預置物")]
        private GameObject icicle_prefab;
        public int times = 2;
        private void OnEnable()
        {
            Random.InitState(Random.Range(0, int.MaxValue));
        }
        private void FixedUpdate()
        {
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
            if (Random.Range(0, 1000) <= chain_probability && times >= 0)
            {
                Vector3 generate_point = collision.transform.position;
                GameObject new_object = Instantiate(icicle_prefab, generate_point + new Vector3(1.414f, 0, 0), Quaternion.identity);
                new_object.GetComponent<Skill_icicle>().times--;
                print(new_object.transform.position);
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(1, 1, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 45);
                new_object.GetComponent<Skill_icicle>().times--;
                print(new_object.transform.position);
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(0, 1.414f, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 90);
                new_object.GetComponent<Skill_icicle>().times--;
                print(new_object.transform.position);
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(1, 1, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 135);
                new_object.GetComponent<Skill_icicle>().times--;
                print(new_object.transform.position);
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(-1.414f, 0, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 180);
                new_object.GetComponent<Skill_icicle>().times--;
                print(new_object.transform.position);
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(-1, -1, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 225);
                new_object.GetComponent<Skill_icicle>().times--;
                print(new_object.transform.position);
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(-1.414f, 0, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 270);
                new_object.GetComponent<Skill_icicle>().times--;
                print(new_object.transform.position);
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(1, -1, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 315);
                new_object.GetComponent<Skill_icicle>().times--;
                print(new_object.transform.position);
            }
            End();
        }
    }
}


