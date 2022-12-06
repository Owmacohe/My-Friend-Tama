using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setMouseActive : MonoBehaviour
{
    public void setCursorActive()
    {
        Cursor.visible = true;
    }

    public void setCursorDisable()
    {
        Cursor.visible = false;
    }
}
