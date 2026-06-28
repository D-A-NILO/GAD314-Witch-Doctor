using UnityEngine;
using UnityEngine.UI;

public class HotBarUI : MonoBehaviour
{
    public EquipItem equipItem;
    public Image[] slotImages;
    public Color activeColor = Color.yellow;
    public Color inactiveColor = Color.white;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < slotImages.Length; i++)
        { 
            slotImages[i].color = (i == equipItem.activeSlot) ? activeColor : inactiveColor;
        }
    }
}
