using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class ScenarioManager : MonoBehaviour
{
    [SerializeField] private string[] story;
    [SerializeField] Text storyText;
    [SerializeField] Text nameFrom;
    [SerializeField] GameObject nameObj;
    [SerializeField] GameObject main;
    [SerializeField] Player player;

    private Color color;
    private int count = 0;

    void Start()
    {
        color = Color.white;
        color.a = 0;
        FadeIn();
    }
    public void FadeIn(float _speed = 0.02f)
    {
        storyText.text = story[count++];

        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine(_speed));
    }

    IEnumerator FadeInCoroutine(float _speed)
    {
        while (color.a <= 1f)
        {
            color.a += _speed;
            storyText.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        FadeOut();
    }

    public void FadeOut(float _speed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine(_speed));
    }

    IEnumerator FadeOutCoroutine(float _speed)
    {
        while (color.a >= 0)
        {
            color.a -= _speed;
            storyText.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);

        if (story.Length == count)
            nameObj.SetActive(true);
        else
            FadeIn();
    }

    public void StartGame()
    {
        nameObj.SetActive(false);
        main.SetActive(true);
        player.gameObject.SetActive(true);

        player.name = nameFrom.text;
        player.ShowStatus();
    }
}
