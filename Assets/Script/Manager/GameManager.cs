using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
class Event
{
    public Command command = null;
    public Transform ToPosition = null;
    public string location = null;
    public float percent = 0;
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] List<Event> randomtList = new List<Event>();

    [SerializeField] private RectTransform commandCanvas;

    [Header("���� ���� �Ÿ�")]
    public float minDistanceBetweenObjects = 3f; // �ּ� ��ø �Ÿ�

    public List<Command> list;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();

            return instance;
        }
    }

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

    public void EventStart(string location)
    {
        bool nNotEvent = true;
        for (int i = 0; i < randomtList.Count; i++)
        {
            if (randomtList[i].location == location)
            {
                nNotEvent = false;
                float randomValue = Random.Range(0f, 100);

                if (randomValue < randomtList[i].percent)
                {
                    float minX = commandCanvas.rect.xMin * commandCanvas.localScale.x;
                    float maxX = commandCanvas.rect.xMax * commandCanvas.localScale.x;
                    float minY = commandCanvas.rect.yMin * commandCanvas.localScale.y;
                    float maxY = commandCanvas.rect.yMax * commandCanvas.localScale.y;

                    Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 1f);

                    // Adjust the position to fit within the RectTransform bounds
                    randomPosition.x = Mathf.Clamp(randomPosition.x, minX, maxX);
                    randomPosition.y = Mathf.Clamp(randomPosition.y, minY, maxY);

                    if (IsOverlapping(randomPosition))
                    {
                        Debug.Log("������ ����.");
                    }
                    else
                    {
                        if (randomtList[i].ToPosition)
                        {
                            randomtList[i].command.active = true;
                            Player.Instance.ShowIntroduce(randomtList[i].command.commandName + "������ �߰��ߴ�.");
                            randomtList.RemoveAt(i);
                        }
                        else
                        {

                            Command b = Instantiate(randomtList[i].command, randomPosition, Quaternion.identity, commandCanvas);
                            list.Add(b);
                            Player.Instance.ShowIntroduce(randomtList[i].command.commandName + "(��) �߰��ߴ�.");
                        }
                        return;
                    }
                }
                else
                {
                    Player.Instance.ShowIntroduce("�ƹ��͵� �߰����� ���ߴ�.");       
                }
            }
        }
        if (nNotEvent)
        {
            Player.Instance.ShowIntroduce("�̺�Ʈ ����Ʈ ����");
        }
    }
    


    bool IsOverlapping(Vector3 localPosition)
    {
        int maxAttempts = 100; // �ִ� �õ� Ƚ�� ����

        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            bool overlap = false;

            foreach (Transform childTransform in transform)
            {
                float distance = Vector3.Distance(localPosition, childTransform.localPosition);

                if (distance < minDistanceBetweenObjects)
                {
                    overlap = true;
                    break;
                }
            }

            if (!overlap)
                return false;

            localPosition = new Vector3(Random.Range(commandCanvas.rect.xMin, commandCanvas.rect.xMax), Random.Range(commandCanvas.rect.yMin, commandCanvas.rect.yMax), 1f);
        }

        return true;
    }

    public void RemoveList()
    {


        if (list != null)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Destroy(list[i].gameObject);
                list.RemoveAt(i);
            }
        }
    }
}