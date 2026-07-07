using System;
using UnityEngine;

public abstract class CraftingTable : MonoBehaviour
{
    public Transform holdPoint;
    [SerializeField] private float removeCooldownTime = 1f;
    protected Ingredient heldIngredient;


    public CraftingUtensil requiredUtensil;
    int craftingInteractionCount;


    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if(other.TryGetComponent(out Ingredient ingredient))
        {
            Debug.Log(cooldown);
            if(cooldown <= 0) // not on cooldown
            {
                PlaceIngredient(ingredient);
            }   
        }  
    }

    public virtual void PlaceIngredient(Ingredient ingredient)
    {
        //snap to holdPoint
        Grabbable grabbable = ingredient.GetComponent<Grabbable>();
        if(grabbable.GetHoldingInteractor() != null)
            grabbable.GetHoldingInteractor().DropItem();
        
        grabbable.Freeze();
        grabbable.transform.position = holdPoint.position;
        grabbable.transform.rotation = holdPoint.rotation;
        heldIngredient = ingredient;
        heldIngredient.heldInTable = this;
        craftingInteractionCount = 0;
    }

    private float cooldown;
    public void RemoveHeldIngredient()
    {
        cooldown = removeCooldownTime;

        heldIngredient.heldInTable = null;

        heldIngredient = null;
    }


    public void IngredientInteracted(Ingredient ingredient, CraftingUtensil utensil)
    {
        if(utensil != requiredUtensil) return;
        if(ingredient.craftInteractionsRequired <= 0)
        {
            Debug.Log("item has 0 crafting requirments");
            return;
        }

        craftingInteractionCount++;
        if(craftingInteractionCount >= heldIngredient.craftInteractionsRequired)
        {
            Ingredient craftedItem = CraftIngredient();
            if(craftedItem)
                PlaceIngredient(craftedItem);
        }
    }

    protected abstract Ingredient CraftIngredient();

    void Update()
    {
      cooldown -= Time.deltaTime;  
    }

}
