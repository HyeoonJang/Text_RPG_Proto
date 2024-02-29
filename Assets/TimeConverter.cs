using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class TimeConverter : MonoBehaviour
{
    // ó�� ���� �ð� (06:00)
    int initialHours = 6;
    int initialMinutes = 0;

    // Ŭ���� ��� ������ hours�� minutes �߰�
    int hours;
    int minutes;

    public Text t;

    // ���ڸ� �޾� �ð��� �߰��ϴ� �Լ�
    public void AddTime(float value)
    {
        Player.Instance.Hp += value / 10;

        // �Էµ� ���ڸ� �ð��� ������ ��ȯ
        int additionalHours = Mathf.FloorToInt(value / 60);
        int additionalMinutes = Mathf.FloorToInt(value % 60);

        // �ð��� ���� ���ϱ�
        hours += additionalHours;
        minutes += additionalMinutes;

        // �ð��� 24�� �Ѿ�� �ٽ� 0���� ����
        hours = hours % 24;

        // ���� 60�� �Ѿ�� �ð��� �߰��ϰ� ���� 0���� �ʱ�ȭ
        if (minutes >= 60)
        {
            hours += Mathf.FloorToInt(minutes / 60);
            minutes = minutes % 60;
        }

        // �ð��� ���� ���ڿ��� ����
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

    // ����: ��ũ��Ʈ�� ��� ����ϴ��� �����ִ� �κ�
    void Start()
    {
        AddTime(0);
        // �ʱ� �ð� ����
        hours = initialHours;
        minutes = initialMinutes;
        Debug.Log($"���� �ð�: {hours:D2}:{minutes:D2}");
    }
}