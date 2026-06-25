using UnityEngine;
using UnityEngine.UI;

public class HotBarUI : MonoBehaviour
{
    public EquipItem equipItem;
    public Image[] slotImages;
    public Color activeColor = Color.yellow;
    public Color inactiveColor = Color.white;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < slotImages.Length; i++)
        { 
            slotImages[i].color = (i == equipItem.activeSlot) ? activeColor : inactiveColor;
        }
    }
}
