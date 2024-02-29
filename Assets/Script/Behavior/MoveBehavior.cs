using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using static UnityEditor.FilePathAttribute;

public class MoveBehavior : Command
{
    [SerializeField] Move movePrefab;
    protected override void Start()
    {
        DD();
    }

    public void DD()
    {
        canMove = false;
        text.text = commandName;
    }

    public override void ExecuteCustom(Player player)
    {
        if (childList.Count > 0)
        {
            childList.Clear();
            listMenu.DestoryCommand();
            DD();

            listMenu.gameObject.SetActive(!listMenu.gameObject.activeSelf);

            if (listMenu.gameObject.activeSelf)
                listMenu.ShowList(childList.Count);
        }
        else
            Execute(player);
    }


    protected override void Execute(Player player)
    {
        player.CurrentLocation = commandName;
        player.GetStatus(status);
    }
}
