using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CommandUI : MonoBehaviour
{
    private Player player;
    private Command selectCommand;

    private RectTransform pos;
    private Vector2 mouseOne;
    private Vector2 mouseTwo;
    bool moved;
    
    private void Awake()
    {
        pos = GetComponent<RectTransform>();
        player = Player.Instance;
    }
    void Start()
    {
        selectCommand = null;
        moved = false;
    }

    // Update is called once per frame
    void Update()
    {
        //최초 마우스 클릭시
        if (Input.GetMouseButtonDown(0))
        {
            moved = false;
            mouseOne = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseOne, Vector2.zero);

            if (hit.collider)
                selectCommand = hit.collider.GetComponent<Command>();
            else
                selectCommand = null;
        }

        if (selectCommand)
        {
            //LbuttonDown
            if (Input.GetMouseButton(0))
            {
                mouseTwo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Debug.Log("Mouse Position: " + mouseOne);
                //Debug.Log("Mouse Position: " + mouseTwo);
                if (mouseTwo != mouseOne && selectCommand.canMove)
                {
                    moved = true;
                    Vector2 pos = mouseTwo - mouseOne;
                    selectCommand.Move(new Vector3(pos.x, pos.y, 0));
                    mouseOne = mouseTwo;
                }
            }

            //LButtonUp
            if (Input.GetMouseButtonUp(0) && moved == false)
            {
                selectCommand.ExecuteCustom(player);
                selectCommand = null;
            }
        }
    }
}
