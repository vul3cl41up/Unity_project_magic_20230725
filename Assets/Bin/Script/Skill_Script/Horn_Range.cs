using magic;
using System.Threading;
using UnityEngine;

public class Horn_Range : MonoBehaviour
{
    [SerializeField, Header("惡魔角預置物")]
    private GameObject horn_effect;
    [SerializeField, Header("技能資料")]
    Skill_Data skill_data;
    private float radius;

    float timer = 1.2f;
    bool can_attack = true;
    

    private void Start()
    {
        radius = skill_data.scale;
        transform.localScale = Vector3.one * radius * 2;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <=0 && can_attack)
        {
            can_attack = false;
            StartAttack();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius-0.3f);
    }
    void StartAttack()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, radius-0.3f);
        if (cols != null)
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].CompareTag("Enemy")) //cols[i].gameObject.GetComponent<Enemy_Base_Control>().Take_Damage(role_control, skill_type);
                    Instantiate(horn_effect, cols[i].transform.position, Quaternion.identity, cols[i].transform);
            }
        Destroy(gameObject);
    }
}
