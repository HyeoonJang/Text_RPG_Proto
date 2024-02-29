using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Command
{
    public GameObject ga;
    protected override void Execute(Player player)
    {
        player.GetStatus(status);
        Destroy(ga);
    }
}
