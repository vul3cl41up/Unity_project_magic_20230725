using System.Threading;
using UnityEngine;

public class Horn_Range : MonoBehaviour
{
    [SerializeField, Header("´cÅ]¨¤¹w¸mª«")]
    private GameObject horn_prefab;
    float timer = 2f;
    bool can_attack = true;
    private float radius= 2.5f;

    private void Start()
    {
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
                    Instantiate(horn_prefab, cols[i].transform.position-new Vector3(0,0.8f,0), Quaternion.identity);
            }
        Destroy(gameObject);
    }
}
