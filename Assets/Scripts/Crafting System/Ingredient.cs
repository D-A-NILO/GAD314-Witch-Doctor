using UnityEditor.MPE;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientData ingredientData;
    public IngredientState ingredientState = IngredientState.Raw;
    public GameObject currentModel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateModel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProcessIngredientState(IngredientState state)
    {
        
        ingredientState = state;
        UpdateModel();
    }

    private void UpdateModel()
    {
        Debug.Log($"{gameObject.name} using {ingredientData.ingredientName} | " + $"Raw:{ingredientData.rawPrefab}");
        if (currentModel != null)
        {
            Destroy(currentModel);
            currentModel = null;
        }

        GameObject prefab = null;

        switch (ingredientState)
        { 
            case IngredientState.Raw:
                currentModel = Instantiate(ingredientData.rawPrefab, transform);
                break;
            case IngredientState.Chopped:
                currentModel = Instantiate(ingredientData.choppedPrefab, transform);
                break;
            case IngredientState.Grinded:
                currentModel = Instantiate(ingredientData.grindedPrefab, transform);
                break;
        }

        if (prefab == null)
        {
            Debug.LogWarning("Missing prefab for state: " + ingredientState);
            return;
        }

        currentModel = Instantiate(prefab, transform);

        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.identity;
    }
}
