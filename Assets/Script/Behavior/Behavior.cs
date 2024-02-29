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
        if (commandName == "����")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "����")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "���� ����")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "��ȭ��")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "�� ����")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "����")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "����")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "��")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "�Ĵ�")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.GetStatus(LocationManager.Instance.CalculateTotalStatus(Player.Instance.CurrentLocation, commandName));
            Player.Instance.CurrentLocation = commandName;
            GameManager.Instance.RemoveList();
        }
        if (commandName == "�����ϱ�")
        {
            player.GetStatus(status);
            GameManager.Instance.EventStart(Player.Instance.CurrentLocation);
        }
        if (commandName == "��ȭ�ϱ�")
        {
            Player.Instance.ShowIntroduce("NPC�� ��ȭ�� �Ѵ�.");
            move.gameObject.SetActive(false);
        }
        if (commandName == "�����ϱ�")
        {
            Player.Instance.ShowIntroduce("������ ���.");
            move.gameObject.SetActive(false);
        }
        if (commandName == "�޽��ϱ�")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.ShowIntroduce("�޽���");
            Player.Instance.GetStatus(status);
        }
        if (commandName == "���ڱ�")
        {
            parent.gameObject.SetActive(false);
            move.gameObject.SetActive(false);
            Player.Instance.ShowIntroduce("���ڴ���");
            Player.Instance.GetStatus(status);
        }
        if (commandName == "�Ļ��ϱ�")
        {
            parent.gameObject.SetActive(false);
            Player.Instance.ShowIntroduce("�Ļ��Ѵ�");
            Player.Instance.GetStatus(status);
        }

        if (commandName == "�ݴ´�")
        {
            move.gameObject.SetActive(false);
            parent.transform.SetParent(InventoryManager.Instance.transform, false);
            Player.Instance.ShowIntroduce("�ݴ�.");
            Player.Instance.GetStatus(status);
        }
        if (commandName == "������")
        {
            Destroy(parent);
            Player.Instance.ShowIntroduce("������.");
            Player.Instance.GetStatus(status);
        }
    }
}