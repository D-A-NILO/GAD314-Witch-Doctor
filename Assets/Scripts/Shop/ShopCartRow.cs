using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCartRow : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private TMP_Text subtotalText;
    [SerializeField] private Button removeButton;

    public void Setup(IngredientData ingredient, int quantity, ShopUI shopUI)
    {
        nameText.text = ingredient.name;
        quantityText.text = $"x{quantity}";
        subtotalText.text = $"{ingredient.pricePerUnit * quantity}c";

        removeButton.onClick.RemoveAllListeners();
        removeButton.onClick.AddListener(() => shopUI.RemoveFromCart(ingredient));
    }
}
