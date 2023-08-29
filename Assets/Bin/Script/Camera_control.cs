using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_control : MonoBehaviour
{
    public GameObject Role;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position - Role.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Role.transform.position + offset;
    }

}
