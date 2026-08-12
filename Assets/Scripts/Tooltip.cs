using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    
    public static Tooltip I;
    [SerializeField] private Canvas playerCanvas;
    private RectTransform targetRect;
    [SerializeField] private TMP_Text tooltipTMP;

    void Awake()
    {
        if(I == null)
        {
            I = this;
        } 
        else
        {
            Destroy(gameObject);
        }
        targetRect = playerCanvas.GetComponent<RectTransform>();
    }
    public static void SetActive(bool active)
    {
        if(I != null && I.tooltipTMP != null)
        {
            I.tooltipTMP.transform.parent.gameObject.SetActive(active);
        }
    }
    public static void SetText(string text)
    {
        if(I != null && I.tooltipTMP != null)
        {
            I.tooltipTMP.text = text;
        }
    }

    void Update()
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(targetRect, Input.mousePosition, null, out Vector2 localPoint))
        {
            transform.localPosition = localPoint;
        }
    }
}
