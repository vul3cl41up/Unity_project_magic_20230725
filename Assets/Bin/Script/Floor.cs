using System.Collections;
using UnityEngine;
namespace magic
{
    public class Floor : MonoBehaviour
    {
        GameObject target;
        void Start()
        {
            target = GameObject.FindWithTag("Player");
            StartCoroutine(judge_action());
        }

        IEnumerator judge_action()
        {
            yield return new WaitForSeconds(4);
            if ((target.transform.position.x - transform.position.x) >= 30)
                transform.position += new Vector3(60,0,0);
            else if ((target.transform.position.x - transform.position.x) <= -30)
                transform.position -= new Vector3(60, 0, 0);
            if ((target.transform.position.y - transform.position.y) >= 30)
                transform.position += new Vector3(0, 60, 0);
            else if ((target.transform.position.y - transform.position.y) <= -30)
                transform.position -= new Vector3(0, 60, 0);
            StartCoroutine(judge_action());
        }


    }

}
