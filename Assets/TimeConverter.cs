using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class TimeConverter : MonoBehaviour
{
    // 처음 기준 시간 (06:00)
    int initialHours = 6;
    int initialMinutes = 0;

    // 클래스 멤버 변수로 hours와 minutes 추가
    int hours;
    int minutes;

    public Text t;

    // 숫자를 받아 시간을 추가하는 함수
    public void AddTime(float value)
    {
        Player.Instance.Hp += value / 10;

        // 입력된 숫자를 시간과 분으로 변환
        int additionalHours = Mathf.FloorToInt(value / 60);
        int additionalMinutes = Mathf.FloorToInt(value % 60);

        // 시간과 분을 더하기
        hours += additionalHours;
        minutes += additionalMinutes;

        // 시간이 24를 넘어가면 다시 0부터 시작
        hours = hours % 24;

        // 분이 60을 넘어가면 시간에 추가하고 분을 0으로 초기화
        if (minutes >= 60)
        {
            hours += Mathf.FloorToInt(minutes / 60);
            minutes = minutes % 60;
        }

        // 시간과 분을 문자열로 조합
        string timeString;

        // Check if the minutes are multiples of 180
        if (minutes % 180 == 0)
        {
            // If multiple of 180, format as HH:00
            timeString = string.Format("{0:D2}:00", hours);
        }
        else
        {
            // Otherwise, format as HH:MM
            timeString = string.Format("{0:D2}:{1:D2}", hours, minutes);
        }

        t.text = timeString;
    }

    // 예시: 스크립트를 어떻게 사용하는지 보여주는 부분
    void Start()
    {
        AddTime(0);
        // 초기 시간 설정
        hours = initialHours;
        minutes = initialMinutes;
        Debug.Log($"현재 시간: {hours:D2}:{minutes:D2}");
    }
}