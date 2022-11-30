using UnityEngine;

public class HintButtons : MonoBehaviour
{
    [SerializeField] GameObject defaultButtons, tamaButton;
    
    public void ShowTamaButton()
    {
        tamaButton.SetActive(true);
    }

    public void HideAllButtons()
    {
        defaultButtons.SetActive(false);
        tamaButton.SetActive(false);
    }
}