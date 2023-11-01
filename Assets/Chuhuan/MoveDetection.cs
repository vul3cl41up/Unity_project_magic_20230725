using Fungus;
using UnityEngine;

public class MoveDetection : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(rigidbody2D.velocity!=Vector2.zero)
        Flowchart.BroadcastFungusMessage("OpenFungus1");
    }
}
