using UnityEngine;
using Fungus;
using magic;

public class OpenFungus : MonoBehaviour
{
    Enemy_Base enemy_base;
    private void Start()
    {
        enemy_base = GetComponent<Enemy_Base>();
    }
    private void OnDisable()
    {
        if(enemy_base.hp <=0)
        Flowchart.BroadcastFungusMessage("OpenFungus2");
    }
}
