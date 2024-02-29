using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Status[] status = new Status[6];
    public void one()
    {
        Player.Instance.GetStatus(status[0]);
    }
    public void two()
    {
        Player.Instance.GetStatus(status[1]);
    }
    public void three()
    {
        Player.Instance.GetStatus(status[2]);
    }
    public void four()
    {
        Player.Instance.GetStatus(status[3]);
    }
    public void five()
    {
        Player.Instance.GetStatus(status[4]);
    }
    public void six()
    {
        Player.Instance.GetStatus(status[5]);
    }
}
