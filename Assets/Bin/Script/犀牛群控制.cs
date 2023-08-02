using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 犀牛群控制 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Rino;
    public Transform parent;
    Vector3 position;
    int num = 3;

    private void Start()
    {
        position = new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f), 0.0f);
        do
        {
            Instantiate(Rino, position, Quaternion.identity, parent);
            num--;
            position.x = Random.Range(-20.0f, 20.0f);
            position.y = Random.Range(-20.0f, 20.0f);
        } while (num > 0);

    }

}
