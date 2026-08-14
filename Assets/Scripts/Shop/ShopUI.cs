using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour , IMenu
{
    [Header("Catalogue")]
    [SerializeField] private ShopItemData[] catalogue;
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



    [SerializeField] private Animator bookAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSO openShopSFX;
    [SerializeField] private AudioSO closeShopSFX;
    [SerializeField] private AudioSO purchaseSFX;
    [SerializeField] private ParticleSystem purchaseVFX;

    private readonly Dictionary<ShopItemData, int> cart = new();
    private PlayerController playerController;

    void Awake()
    {
        //panel.SetActive(false);
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

    void Update()
    {
        if (panel.activeSelf && Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Close();
        }
    }

    private void PopulateCatalogue()
    {
        foreach (ShopItemData item in catalogue)
        {
            ShopCatalogueRow row = Instantiate(catalogueRowPrefab, catalogueContainer);
            row.Setup(item, this);
        }
    }

    public void Open(PlayerController controller)
    {
        
        playerController = controller;
        panel.SetActive(true);
        RefreshBalance();
        RefreshCart();

        Debug.Log("open sound");
        playFromSource.PlayAudio(openShopSFX);

        MenuManager.I.TryShowMenu(this);
    }

    public void Close()
    {
        Debug.Log("close sound");
        playFromSource.PlayAudio(closeShopSFX);

        panel.SetActive(false);


        
        MenuManager.I.TryHideMenu(this);;

    }

    public void OnShow()
    {
        RefreshBalance();
        RefreshCart();

        bookAnimator.SetBool("open", true);
        openShopSFX.PlayFromSource(audioSource);
        panel.SetActive(true);
    }

    public void OnHide()
    {
        bookAnimator.SetBool("open", false);
        closeShopSFX.PlayFromSource(audioSource);

        if (playerController != null)
        {
            
            playerController.SetControl(true);
        }
        panel.SetActive(false);
    }

    public void AddToCart(ShopItemData item, int quantity)
    {
        cart.TryGetValue(item, out int existing);
        cart[item] = existing + quantity;
        RefreshCart();
    }

    public void RemoveFromCart(ShopItemData item)
    {
        cart.Remove(item);
        RefreshCart();
    }

    private void RefreshCart()
    {
        foreach (Transform child in cartContainer)
            Destroy(child.gameObject);

        totalCost = 0;
        foreach (KeyValuePair<ShopItemData, int> entry in cart)
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
       

        if (CurrencyManager.Instance == null || !CurrencyManager.Instance.Spend(totalCost))
            return;

        Debug.Log("purchase sound");
        playFromSource.PlayAudio(purchaseSFX);
        spawner.SpawnOrder(cart);
        Instantiate(purchaseVFX, orderButton.transform.position, Quaternion.identity);
        
        spawner.SpawnOrder(cart);
        purchaseSFX.PlayGlobal();
        cart.Clear();
        Close();
    }
}
