using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class Event
{
    public Command command = null;
    public string location;
    public float percent = 0;
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] List<Event> randomtList = new List<Event>();

    [SerializeField] private RectTransform commandCanvas;

    [Header("랜덤 생성 거리")]
    public float minDistanceBetweenObjects = 3f; // 최소 중첩 거리

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
        // 확률 합계 계산
        float totalPercent = 0f;

        // 해당 지역에 해당하는 이벤트들의 확률 합계 계산
        foreach (Event randomEvent in randomtList)
        {
            if (randomEvent.location == location)
            {
                totalPercent += randomEvent.percent;
            }
        }

        // 랜덤으로 선택된 확률
        float randomValue = Random.Range(0f, totalPercent);

        float accumulatedPercent = 0f;

        for (int i = 0; i < randomtList.Count; i++)
        {
            if (randomtList[i].location == location)
            {
                accumulatedPercent += randomtList[i].percent;

                if (accumulatedPercent >= randomValue)
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
                        Debug.Log("공간이 없다.");
                    }
                    else
                    {
                        if (randomtList[i].command == null)
                        {
                            Player.Instance.ShowIntroduce("아무것도 발견하지 못했다.");
                        }
                        else
                        {
                            Command b = Instantiate(randomtList[i].command, randomPosition, Quaternion.identity, commandCanvas);
                            list.Add(b);
                            Player.Instance.ShowIntroduce(randomtList[i].command.commandName + "(를) 발견했다.");
                        }
                        return;
                    }
                }
            }
        }
    }

    bool IsOverlapping(Vector3 localPosition)
    {
        int maxAttempts = 100; // 최대 시도 횟수 설정

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
            for (int i = 0; i < list.Count; i++)
            {
                Destroy(list[i].gameObject);
            }
            list.Clear();
        }
    }
}