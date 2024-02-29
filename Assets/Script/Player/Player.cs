using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

[System.Serializable]
public class Status
{
    public float hp;
    public float fatigue;
    public float hungry;
    public float money;
    public float time;
}

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
                instance = new Player();

            return instance;
        }
    }

    [SerializeField] private Text statusText;
    [SerializeField] private Text nameText;
    [SerializeField] private Text locationText;
    [SerializeField] private Text introduceText;
    [SerializeField] TimeConverter timeText;

    [SerializeField] Status playerStatus;
    public float Hp
    {
        get { return playerStatus.hp; }
        set
        {
            playerStatus.hp = value;

            if (playerStatus.hp > 100)
                playerStatus.hp = 100;
            
            if (playerStatus.hp <= 0)
            {
                Debug.Log("�÷��̾� ���");
            }
        }
    }

    public float Fatigue
    {
        get { return playerStatus.fatigue; }
        set
        {
            playerStatus.fatigue = value;
            if (playerStatus.fatigue > 100)
                playerStatus.fatigue = 100;
            if (playerStatus.fatigue <= debuff1)
            {
                ShowIntroduce("8�ð� ����");
                timeText.AddTime(480);
                Hp = 100;
                Fatigue = 100;
                Hungry -= 50;
            }
            else if (playerStatus.fatigue <= debuff2)
            {
                ShowIntroduce("����� �ܰ�1");
            }
            if (playerStatus.fatigue <= debuff4)
            {
                ShowIntroduce("����� �ܰ�3 ");
            }
        }
    }

    public bool bOn = false;
    public float Hungry
    {
        get { return playerStatus.hungry; }
        set
        {
            playerStatus.hungry = value;
            if (playerStatus.hungry > 100)
                playerStatus.hungry = 100;

            if (bOn)
            {
                if (playerStatus.hungry > 0)
                    Hp += playerStatus.hungry / fullRate;
                else
                    Hp += playerStatus.hungry / hungerRate;
            }
        }
    }


    private string currnetLocation;
    public string playerName;

    [Header("���� �����")]
    public float debuff1;
    public float debuff2;
    public float debuff3;
    public float debuff4;
    [Header("������")]
    [Range(0, 100)]
    public float fullRate;
    public float hungerRate;

    public string CurrentLocation
    {
        get { return currnetLocation; }
        set 
        {
            currnetLocation = value;
            locationText.text = "(" + currnetLocation + ")";
            if (currnetLocation == "�Ĵ�")
                eat.gameObject.SetActive(true);
            else
                eat.gameObject.SetActive(false);

            if (currnetLocation == "��")
                sleep.gameObject.SetActive(true);
            else
                sleep.gameObject.SetActive(false);

            if (currnetLocation == "��ȭ��" || currnetLocation == "���� ����" || currnetLocation == "�� ����")
                npc.gameObject.SetActive(true);
            else
                npc.gameObject.SetActive(false);
        }

    }

    public GameObject eat;
    public GameObject sleep;
    public GameObject npc;

    // Awake �޼��带 ����Ͽ� �ʱ�ȭ
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        CurrentLocation = "����"; ;

        ShowStatus();
    }

    public void GetStatus(Status status)
    {
        Debug.Log("�ǰ� : " + status.hp + "�Ƿ� : " + status.fatigue + "��� : " + status.hungry + "�ð� : " + status.time);
        Hp += status.hp;
        Fatigue += status.fatigue;
        Hungry += status.hungry;
        playerStatus.money += status.money;
        timeText.AddTime(status.time);

        ShowStatus();
    }

    public void ShowStatus()
    {
        statusText.text = "�ǰ� : " + playerStatus.hp + "\n" + "�Ƿ� : " + playerStatus.fatigue + "\n" + "��� : " + playerStatus.hungry;
        CurrentLocation = CurrentLocation;
        nameText.text = playerName + "/" + "���谡���";
    }

    public void ShowIntroduce(string introduce)
    {
        introduceText.text = introduce;
        Color color = Color.white;

        StartCoroutine(TextFade(color));
    }

    IEnumerator TextFade(Color color, float _speed = 0.02f)
    {
        while (introduceText.color.a <= 1f)
        {
            color.a += _speed;
            introduceText.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);

        while (color.a >= 0)
        {
            color.a -= _speed;
            introduceText.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
    }
}
