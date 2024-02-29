using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagCommand : Command
{
    public GameObject ac;

    public GameObject dd;
    protected override void Execute(Player player)
    {
        ac.gameObject.SetActive(true);
        dd.gameObject.SetActive(false);
    }
}
