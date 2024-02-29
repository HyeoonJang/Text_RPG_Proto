using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Command : MonoBehaviour
{
    protected RectTransform trans;
    protected BoxCollider2D col;
    protected Text text;

    public RectTransform parentTransform;
    public ListMenu listMenu;
    public List<Command> childList;
    public Status status;
    public string commandName;
    public bool canMove = false;


    protected virtual void Awake()
    {
        listMenu = GetComponentInChildren<ListMenu>();
        trans = GetComponent<RectTransform>();
        col = GetComponentInChildren<BoxCollider2D>();
        text = GetComponentInChildren<Text>();
    }

    protected virtual void Start()
    {
    }

    private void OnEnable()
    {
        text.text = commandName;
        if (Player.Instance.CurrentLocation == commandName)
        {
            gameObject.SetActive(false);
         }
    }

    public virtual void ExecuteCustom(Player player)
    {
        if (listMenu.transform.childCount > 0)
        {
            listMenu.gameObject.SetActive(!listMenu.gameObject.activeSelf);
        }
        else
            Execute(player);
    }
    
    protected abstract void Execute(Player player);

    public void Move(Vector3 vector)
    {
        if (canMove)
        {
            trans.Translate(vector);
            //// Get the anchor and offset values of the parentTransform
            //Vector2 anchorMin = parentTransform.anchorMin;
            //Vector2 anchorMax = parentTransform.anchorMax;
            //Vector2 offsetMin = parentTransform.offsetMin;
            //Vector2 offsetMax = parentTransform.offsetMax;

            //// Calculate the bounds based on anchor and offset
            //float minX = anchorMin.x * parentTransform.sizeDelta.x + offsetMin.x;
            //float minY = anchorMin.y * parentTransform.sizeDelta.y + offsetMin.y;
            //float maxX = anchorMax.x * parentTransform.sizeDelta.x + offsetMax.x;
            //float maxY = anchorMax.y * parentTransform.sizeDelta.y + offsetMax.y;

            //Vector3 newPosition = trans.position + vector;

            //// Clamp the new position within the bounds
            //float clampedX = Mathf.Clamp(newPosition.x, parentTransform.offsetMin.x, parentTransform.offsetMax.x);
            //float clampedY = Mathf.Clamp(newPosition.y, minY, maxY);

            //Debug.Log("X : " + clampedX + "Y" + clampedY);

            //// Update the position only if it is within bounds
            //trans.position = new Vector3(clampedX, clampedY, 0f);
        }
    }

    public virtual void RemoveChild(Command child)
    {
        if (childList.Contains(child))
        {
            childList.Remove(child);
            Destroy(child.gameObject);
        }
    }
}