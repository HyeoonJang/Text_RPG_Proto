using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservationBehavior : Command
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Execute(Player player)
    {
        player.GetStatus(status);
        GameManager.Instance.EventStart(Player.Instance.CurrentLocation);
    }
}
