using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("Catalogue")]
    [SerializeField] private IngredientData[] catalogue;
    [SerializeField] private Transform catalogueContainer;
    [SerializeField] private ShopCatalogueRow catalogueRowPrefab;

    [Header("Cart")]
    [SerializeField] private Transform cartContainer;
    [SerializeField] private ShopCartRow cartRowPrefab;
    [SerializeField] private TMP_Text totalText;
    [SerializeField] private TMP_Text balanceText;
    [SerializeField] private Button orderButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private int totalCost;
    

    [Header("World")]
    [SerializeField] private GameObject panel;
    [SerializeField] private IngredientSpawner spawner;

    private readonly Dictionary<IngredientData, int> cart = new();
    private PlayerController playerController;

    void Awake()
    {
        panel.SetActive(false);
        orderButton.onClick.AddListener(PlaceOrder);
        closeButton.onClick.AddListener(Close);
        PopulateCatalogue();
    }

    void OnEnable()
    {
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.OnBalanceChanged += OnBalanceChanged;
    }

    void OnDisable()
    {
        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.OnBalanceChanged -= OnBalanceChanged;
    }

    private void PopulateCatalogue()
    {
        foreach (IngredientData ingredient in catalogue)
        {
            ShopCatalogueRow row = Instantiate(catalogueRowPrefab, catalogueContainer);
            row.Setup(ingredient, this);
        }
    }

    public void Open(PlayerController controller)
    {
        playerController = controller;
        panel.SetActive(true);
        RefreshBalance();
        RefreshCart();
    }

    public void Close()
    {
        panel.SetActive(false);

        if (playerController != null)
        {
            playerController.SetControl(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void AddToCart(IngredientData ingredient, int quantity)
    {
        cart.TryGetValue(ingredient, out int existing);
        cart[ingredient] = existing + quantity;
        RefreshCart();
    }

    public void RemoveFromCart(IngredientData ingredient)
    {
        cart.Remove(ingredient);
        RefreshCart();
    }

    private void RefreshCart()
    {
        foreach (Transform child in cartContainer)
            Destroy(child.gameObject);

        totalCost = 0;
        foreach (KeyValuePair<IngredientData, int> entry in cart)
        {
            ShopCartRow row = Instantiate(cartRowPrefab, cartContainer);
            row.Setup(entry.Key, entry.Value, this);
            totalCost += entry.Key.pricePerUnit * entry.Value;
        }

        totalText.text = $"Total: {totalCost}c";

    }

    private void RefreshBalance()
    {
            balanceText.text = $"Coin: {CurrencyManager.Instance.Balance}c";
    }

    private void OnBalanceChanged(int newBalance)
    {
        balanceText.text = $"Coin: {newBalance}c";
    }

    private void PlaceOrder()
    {
        spawner.SpawnOrder(cart);
        cart.Clear();
        Close();
    }
}
