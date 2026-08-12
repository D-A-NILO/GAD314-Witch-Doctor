using UnityEngine;
using UnityEngine.UI;

public class recipeMenuItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private UIHoverTooltip hovertoolTip;

    public void setIngredient(IngredientData data)
    {
        hovertoolTip.hoverText = data.name;
        if(data.sprite != null)
            image.sprite = data.sprite;
    }
}
