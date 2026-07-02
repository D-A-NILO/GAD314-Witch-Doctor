using UnityEngine;

public class MortarPestle : MonoBehaviour, IInteractable
{
    public Transform ingredientPoint;
    public int cutsRequired = 5;
    private Ingredient currentIngredient;
    private int currentCuts;
    public EquipItem playerEquip;

    public void OnInteract()
    {
        GameObject heldItem = playerEquip.GetActiveItem();
        Debug.Log($"held item: {heldItem}");

        // CASE 1: Place ingredient
        if (currentIngredient == null && heldItem != null)
        {
            PlaceIngredient(heldItem);
            Debug.Log($"item was placed {heldItem}");
            return;
        }

        // CASE 2: Take ingredient back
        if (currentIngredient != null && heldItem == null)
        {
            TakeIngredient();
            Debug.Log($"item was taken {heldItem}");
            return;
        }

        Debug.Log("Nothing happened (either empty hand or board full)");
    }

    public void PlaceIngredient(GameObject item)
    {
        Ingredient ingredient = item.GetComponent<Ingredient>();

        if (ingredient == null)
        {
            Debug.LogWarning("Item has no Ingredient component!");
            return;
        }

        currentIngredient = ingredient;

        Rigidbody rb = item.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        item.transform.position = ingredientPoint.position;
        item.transform.rotation = ingredientPoint.rotation;

        playerEquip.RemoveActiveItem();

        Debug.Log("Placed: " + item.name);
    }

    public void TakeIngredient()
    {
        if (currentIngredient == null)
            return;

        GameObject obj = currentIngredient.gameObject;

        obj.transform.parent = null;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;

            //reset velocity so it doesn't fall through world
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.WakeUp();
        }

        playerEquip.AddToHotbar(obj);

        currentIngredient = null;
        currentCuts = 0;

        Debug.Log("Ingredient picked up from board");
    }

    public void RegisterCut()
    {
        if (currentIngredient == null)
        { 
            return; 
        }

        if (currentIngredient.ingredientState != IngredientState.Raw)
        {
            return;
        }

        currentCuts++;

        Debug.Log($"grind {currentCuts}");

        if (currentCuts >= cutsRequired)
        {
            FinishChopping();
        }
    }

    public void FinishChopping()
    {
        currentIngredient.ProcessIngredientState(IngredientState.Grinded);
    }

    public bool HasIngredient()
    {
        return currentIngredient != null;
    }
}
