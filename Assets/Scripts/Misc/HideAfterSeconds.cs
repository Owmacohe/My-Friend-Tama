using System;
using UnityEngine;

public class HideAfterSeconds : MonoBehaviour
{
    [SerializeField] float seconds;

    void Start()
    {
        Invoke(nameof(Hide), seconds);
    }

    void Hide()
    {
        Destroy(gameObject);
    }
}