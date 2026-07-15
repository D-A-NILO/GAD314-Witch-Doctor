using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float scatterRadius = 1.5f;

    public void SpawnOrder(Dictionary<IngredientData, int> order)
    {
        foreach (KeyValuePair<IngredientData, int> entry in order)
        {
            if (entry.Key.worldPickupPrefab == null)
            {
                Debug.LogWarning($"IngredientSpawner: {entry.Key.name} has no worldPickupPrefab assigned");
                continue;
            }

            for (int i = 0; i < entry.Value; i++)
            {
                Vector2 offset = Random.insideUnitCircle * scatterRadius;
                Vector3 position = spawnPoint.position + new Vector3(offset.x, 0f, offset.y);
                Instantiate(entry.Key.worldPickupPrefab, position, spawnPoint.rotation);
            }
        }
    }
}
