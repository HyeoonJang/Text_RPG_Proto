using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Behavior : Command
{
    public GameObject parent;
    public GameObject inventory;
    public GameObject move;

    protected override void Execute(Player player)
    {
        if (commandName == "마을")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "시장")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "무기 상점")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "잡화점")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "방어구 상점")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "광장")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "여관")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "방")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "식당")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "관찰하기")
        {
            player.GetStatus(status);
            GameManager.Instance.EventStart(Player.Instance.CurrentLocation);
        }
        if (commandName == "대화하기")
        {
            Player.Instance.ShowIntroduce("NPC와 대화를 한다.");
            move.gameObject.SetActive(false);
        }
        if (commandName == "구매하기")
        {
            Player.Instance.ShowIntroduce("투구를 샀다.");
            move.gameObject.SetActive(false);
        }
        if (commandName == "휴식하기")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.ShowIntroduce("휴식중");
            Player.Instance.GetStatus(status);
        }
        if (commandName == "잠자기")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.ShowIntroduce("잠자는중");
            Player.Instance.GetStatus(status);
        }
        if (commandName == "식사하기")
        {
            parent.gameObject.SetActive(false);
            Player.Instance.ShowIntroduce("식사한다");
            Player.Instance.GetStatus(status);
        }

        if (commandName == "줍는다")
        {
            move.gameObject.SetActive(false);
            parent.transform.SetParent(InventoryManager.Instance.transform, false);
            Player.Instance.ShowIntroduce("줍다.");
            Player.Instance.GetStatus(status);
        }
        if (commandName == "버린다")
        {
            Destroy(parent);
            Player.Instance.ShowIntroduce("버린다.");
            Player.Instance.GetStatus(status);
        }
    }
}