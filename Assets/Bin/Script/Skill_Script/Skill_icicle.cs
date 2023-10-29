using System.Collections;
using UnityEngine;

namespace magic
{
    public class Skill_icicle : Skill_Base
    {
        Vector3 origin_pos;
        BoxCollider2D box_collider;
        bool can_attack = true;
        [SerializeField, Header("冰錐預置物")]
        private GameObject icicle_prefab;
        public int times;
        public bool start = true;
        private void OnEnable()
        {
            times = skill_data.times;
            origin_pos = transform.position;
            box_collider = GetComponent<BoxCollider2D>();
        }
        public void judge_action()
        {
            if (start) { box_collider.enabled = true; }
            if (!start)
            {
                box_collider.enabled = false;
                StartCoroutine(open_collider());
            }
            if (skill_data.skill_level == 4 && start)
            {
                StartCoroutine(Combo());
            }
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
        
        IEnumerator open_collider()
        {
            yield return new WaitForSeconds(0.15f);
            box_collider.enabled=true;
        }

        IEnumerator Combo()
        {
            yield return new WaitForSeconds(0.13f);
            GameObject new_object = Instantiate(icicle_prefab, origin_pos, Quaternion.identity);
            new_object.transform.rotation = transform.rotation;
        }

        protected override IEnumerator Start_Attack(Collider2D collision)
        {
            yield return base.Start_Attack(collision);
            if (times > 0)
            {
                Vector3 generate_point = collision.transform.position;
                GameObject new_object = Instantiate(icicle_prefab, generate_point + new Vector3(1.414f, 0, 0), Quaternion.identity);
                new_object.GetComponent<Skill_icicle>().times = times - 1;
                new_object.GetComponent<Skill_icicle>().start = false;
                new_object.GetComponent<Skill_icicle>().judge_action();
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(1, 1, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 45);
                new_object.GetComponent<Skill_icicle>().times = times - 1;
                new_object.GetComponent<Skill_icicle>().start = false;
                new_object.GetComponent<Skill_icicle>().judge_action();
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(0, 1.414f, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 90);
                new_object.GetComponent<Skill_icicle>().times = times - 1;
                new_object.GetComponent<Skill_icicle>().start = false;
                new_object.GetComponent<Skill_icicle>().judge_action();
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(-1, 1, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 135);
                new_object.GetComponent<Skill_icicle>().times = times - 1;
                new_object.GetComponent<Skill_icicle>().start = false;
                new_object.GetComponent<Skill_icicle>().judge_action();
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(-1.414f, 0, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 180);
                new_object.GetComponent<Skill_icicle>().times = times - 1;
                new_object.GetComponent<Skill_icicle>().start = false;
                new_object.GetComponent<Skill_icicle>().judge_action();
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(-1, -1, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 225);
                new_object.GetComponent<Skill_icicle>().times = times - 1;
                new_object.GetComponent<Skill_icicle>().start = false;
                new_object.GetComponent<Skill_icicle>().judge_action();
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(0, -1.414f, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 270);
                new_object.GetComponent<Skill_icicle>().times = times - 1;
                new_object.GetComponent<Skill_icicle>().start = false;
                new_object.GetComponent<Skill_icicle>().judge_action();
                new_object = Instantiate(icicle_prefab, generate_point + new Vector3(1, -1, 0), Quaternion.identity);
                new_object.transform.rotation = Quaternion.Euler(0, 0, 315);
                new_object.GetComponent<Skill_icicle>().times = times - 1;
                new_object.GetComponent<Skill_icicle>().start = false;
                new_object.GetComponent<Skill_icicle>().judge_action();

            }
            End();
        }
    }
}


