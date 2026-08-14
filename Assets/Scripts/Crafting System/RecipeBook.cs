using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RecipeBook : MonoBehaviour , IMenu
{
    
    public InputActionReference toggleAction;
    public Recipe[] recipes;
    [SerializeField] private GameObject recipeDisplayPrefab;
    [SerializeField] private Transform container;


    void OnEnable()
    {
        toggleAction.action.performed += ToggleShow;

        toggleAction.action.Enable();
    }

    void OnDisable()
    {
      toggleAction.action.performed -= ToggleShow;  
    }

    void Start()
    {
        //populate recipe book
        foreach(Recipe recipe in recipes)
        {
            if(recipe.requiredIngredients.Count <= 0) continue;
            
            RecipeDisplayRow recipeRow = Instantiate(recipeDisplayPrefab, container).GetComponentInChildren<RecipeDisplayRow>();
            
            recipeRow.SetRecipe(recipe);
        }
    }

    private bool shown;
    private void ToggleShow(InputAction.CallbackContext context)
    {

        if(MenuManager.I.IsActiveMenu(this as IMenu))
        {
            MenuManager.I.TryHideMenu(this);
        }else
        {
            MenuManager.I.TryShowMenu(this);
        }

    }

    public void OnHide()
    {

        container.gameObject.SetActive(false);
        Tooltip.SetActive(false);
        //enable player control
        FindAnyObjectByType<PlayerController>().SetControl(true);
    }

    public void OnShow()
    {
        container.gameObject.SetActive(true);
        //disable player control
        FindAnyObjectByType<PlayerController>().SetControl(false);
    }
}
