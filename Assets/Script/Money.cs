using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : Command
{
    public Text ui;
    public int money = 100;

    protected override void Execute(Player player)
    {

    }

    private void OnEnable()
    {
        ui.text = "µ· : " + money;
    }
    private void Update()
    {
    }
}
