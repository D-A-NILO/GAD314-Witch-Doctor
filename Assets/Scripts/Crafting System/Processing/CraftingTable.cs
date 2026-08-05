using System;
using UnityEngine;

public abstract class CraftingTable : MonoBehaviour
{
    public Transform holdPoint;
    [SerializeField] private float removeCooldownTime = 1f;
    protected Ingredient heldIngredient;
    [SerializeField] private ParticleSystem ingredientParticles;
    [SerializeField] private PlayFromSource playfromSource;
    [SerializeField] private AudioSO craftSoundEffect;

    public UtensilType requiredUtensil;
    protected int craftingInteractRequirement = 1;
    int craftingInteractionCount;

    private ParticleSystem ingredientParticlesInstance;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if(other.TryGetComponent(out Ingredient ingredient))
        {
            if(cooldown <= 0) // not on cooldown
            {
                PlaceIngredient(ingredient);
            }   
        }  
    }

    public virtual void PlaceIngredient(Ingredient ingredient)
    {
        if (heldIngredient != null) return;
        
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
        if(utensil.type != requiredUtensil) return;
        if(craftingInteractRequirement <= 0)
        {
            Debug.Log("item has 0 crafting interaction requirment [cannot be crafted]");
            return;
        }

        craftingInteractionCount++;

        SpawnParticles();
        playfromSource.PlayAudio(craftSoundEffect);

        if(craftingInteractionCount >= craftingInteractRequirement)
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

    private void SpawnParticles()
    { 
        ingredientParticlesInstance = Instantiate( ingredientParticles, transform.position, transform.rotation );
    }
}
