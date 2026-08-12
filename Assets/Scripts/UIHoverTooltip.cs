using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string hoverText;


    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("ug");
        Tooltip.SetActive(true);
        Tooltip.SetText(hoverText);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.SetActive(false);
    }

}
