using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model_scene_1
{
    public class Grass_control : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject grass;
        public Transform parent;
        Vector3 position;
        int num = 20;

        private void Start()
        {
            position = new Vector3(Random.Range(-20.0f, 20.0f), Random.Range(-20.0f, 20.0f), 0.0f);
            do
            {
                Instantiate(grass, position, Quaternion.identity, parent);
                num--;
                position.x = Random.Range(-20.0f, 20.0f);
                position.y = Random.Range(-20.0f, 20.0f);
            } while (num > 0);

        }
    }
}

