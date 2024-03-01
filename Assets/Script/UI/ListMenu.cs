using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ListMenu : MonoBehaviour
{
    private RectTransform rectTransform;
    Command[] list; 
    [SerializeField] private float width;
    [SerializeField] private float height;

    private void Awake()
    {
        list = GetComponentsInChildren<Command>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        for (int i = 0; i < list.Length; i++)
        {
            list[i].gameObject.SetActive(list[i].active);
        }

    }

    public void ShowList(int nCount = 0)
    {
        StartCoroutine(AnimateSizeChangeCoroutine(width, nCount * 30f, 0.5f));
    }

    private IEnumerator AnimateSizeChangeCoroutine(float width, float height, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newWidth = Mathf.Lerp(0, width, elapsedTime / duration);
            float newHeight = Mathf.Lerp(0, height, elapsedTime / duration);

            rectTransform.sizeDelta = new Vector2(newWidth, newHeight);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 최종적으로 목표 높이로 설정
        rectTransform.sizeDelta = new Vector2(width, height);
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
    }

    public void DestoryCommand()
    {
        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        // 모든 자식 오브젝트를 삭제합니다.
        foreach (Transform child in children)
        {
            Destroy(child.gameObject);
        }

        Debug.Log(transform.childCount);
    }
}

