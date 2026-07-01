using UnityEditor.MPE;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientData ingredientData;
    public IngredientState ingredientState;
    public GameObject currentModel;
    private IngredientState lastAppliedState;

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
        if (state == lastAppliedState)
            return;

        lastAppliedState = state;
        ingredientState = state;
        UpdateModel();
    }

    private void UpdateModel()
    {
        Debug.Log($"{gameObject.name} using {ingredientData.ingredientName}");
        Debug.Log("BEFORE DESTROY: " + (currentModel ? currentModel.name : "NULL"));

        if (currentModel != null)
        {
            Destroy(currentModel);
            currentModel = null;
        }

        Debug.Log("AFTER DESTROY CALL");

        GameObject prefabToSpawn = GetPrefab();

        Debug.Log("SPAWNING: " + prefabToSpawn.name);

        if (prefabToSpawn == null)
        {
            Debug.LogError($"Missing prefab for {ingredientState} on {name}");
            return;
        }

        // ALWAYS instantiate — no conditions
        currentModel = Instantiate(prefabToSpawn, transform);

        Debug.Log("NEW MODEL: " + currentModel.name);

        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.identity;
    }

    private GameObject GetPrefab()
    {
        switch (ingredientState)
        {
            case IngredientState.Raw:
                return ingredientData.rawPrefab;

            case IngredientState.Chopped:
                return ingredientData.choppedPrefab;

            case IngredientState.Grinded:
                return ingredientData.grindedPrefab;
        }

        return null;
    }
}
