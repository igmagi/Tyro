using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public string msg;
    public Sprite sprite;
    public bool displayOnlyOnce;

    public Dialogue(string msg, Sprite sprite, bool displayOnce)
    {
        this.msg = msg;
        this.sprite = sprite;
        this.displayOnlyOnce = displayOnce;
    }

}
