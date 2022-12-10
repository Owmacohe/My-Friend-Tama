using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSwitcher : MonoBehaviour
{
    public static void SetActive()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static void SetInactive()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
