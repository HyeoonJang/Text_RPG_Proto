using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private bool canMove = false;
    public bool active = true;

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
        if (transform.parent != null)
        {
            ListMenu parentMenu = transform.parent.GetComponent<ListMenu>();
            if (parentMenu != null)
            {
                int nCount = parentMenu.transform.childCount;
                for (int i = 0; i < nCount; i++)
                {
                    GameObject d = parentMenu.transform.GetChild(i).gameObject;
                    if (this.gameObject != d)
                    {
                        ListMenu child = d.GetComponentInChildren<ListMenu>();
                        if (child)
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }

        if (listMenu.transform.childCount > 0)
        {
            listMenu.gameObject.SetActive(!listMenu.gameObject.activeSelf);
        }
        else
            Execute(player);
    }
    
    protected abstract void Execute(Player player);

    public virtual void RemoveChild(Command child)
    {
        if (childList.Contains(child))
        {
            childList.Remove(child);
            Destroy(child.gameObject);
        }
    }

    public void Move(Vector3 vector)
    {
        if (canMove)
            trans.Translate(vector);
    }

    public void Active(bool on)
    {
        active = on;
    }
}