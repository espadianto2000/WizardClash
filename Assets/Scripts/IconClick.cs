using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject borderHover;
    public GameObject borderClick;
    public int buttonValue;
    public bool clicked = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach(IconClick button in InGameManager.instance.pieceButtons)
        {
            button.borderHover.SetActive(false);
        }
        borderHover.SetActive(true);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        borderHover?.SetActive(false);
    }

    public void click()
    {
        foreach (IconClick button in InGameManager.instance.pieceButtons)
        {
            button.borderClick.SetActive(false);
        }
        Debug.Log("codigo: " + buttonValue);
        if (!clicked)
        {
            clicked = true;
            this.borderClick.SetActive(true);
        }
        else
        {
            clicked = false;
        }
    }
}
