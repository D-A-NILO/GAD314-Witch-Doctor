using System.Collections.Generic;
using UnityEngine;

public class IngredientPackage : Grabbable
{
    [SerializeField] private float scatterRadius = 1f;

    private Dictionary<IngredientData, int> contents;
    private bool isOpen;

    public void Initialize(Dictionary<IngredientData, int> order)
    {
        contents = new Dictionary<IngredientData, int>(order);
    }


    public void Open()
    {
        if (isOpen || contents == null) return;
        isOpen = true;

        foreach (KeyValuePair<IngredientData, int> entry in contents)
        {
            if (entry.Key.worldPickupPrefab == null)
            {
                Debug.LogWarning($"ingredient pacakge: {entry.Key.name} has no world pickup prefab assigned");
                continue;
            }
            for (int i = 0; i < entry.Value; i++)
            {
                Vector2 offset = Random.insideUnitCircle * scatterRadius;
                Vector3 position = transform.position + new Vector3(offset.x, 0f, offset.y);
                Instantiate(entry.Key.worldPickupPrefab, position, transform.rotation);
            }
        }


        Destroy(gameObject, 0.1f);
    }
}
