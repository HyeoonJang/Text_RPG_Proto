using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Command
{


    protected override void Execute(Player player)
    {
        player.CurrentLocation = commandName;
        player.GetStatus(status);

    }
}
