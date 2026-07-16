using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCatalogueRow : MonoBehaviour
{
    
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button addToCartButton;

    private IngredientData ingredient;
    private ShopUI shopUI;
    private int quantity = 1;

    public void Setup(IngredientData ingredientData, ShopUI ui)
    {
        ingredient = ingredientData;
        shopUI = ui;
        quantity = 1;

        nameText.text = ingredient.name;
        priceText.text = $"{ingredient.pricePerUnit}c";
        UpdateQuantityText();

        increaseButton.onClick.AddListener(Increase);
        decreaseButton.onClick.AddListener(Decrease);
        addToCartButton.onClick.AddListener(AddToCart);
    }

    private void Increase()
    {
        quantity++;
        UpdateQuantityText();
    }

    private void Decrease()
    {
        quantity = Mathf.Max(1, quantity - 1);
        UpdateQuantityText();
    }

    private void UpdateQuantityText()
    {
        quantityText.text = quantity.ToString();
    }

    private void AddToCart()
    {
        shopUI.AddToCart(ingredient, quantity);
    }
}
