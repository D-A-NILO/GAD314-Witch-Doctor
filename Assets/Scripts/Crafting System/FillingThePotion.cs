using UnityEngine;

public class FillingThePotion : MonoBehaviour
{
    public bool isFilled;

    public PotionResultType potionType;
    public Recipe storedRecipe;

    public void FillFromCauldron(Cauldron cauldron)
    {
        if (isFilled) return;

        potionType = cauldron.resultType;
        storedRecipe = cauldron.resultRecipe;

        isFilled = true;

        Debug.Log($"bottle filled with: {potionType}");

        if (potionType == PotionResultType.SuccessfulMix)
        {
            Debug.Log($"potion: {storedRecipe.recipeName}");
        }
        else
        {
            Debug.Log("failed mixture bottled");
        }

        cauldron.ClearCauldron();
    }
}
