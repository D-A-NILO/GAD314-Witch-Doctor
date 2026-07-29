using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private IngredientPackage packagePrefab;

    public void SpawnOrder(Dictionary<ShopItemData, int> order)
    {
        if (packagePrefab == null)
        {
            Debug.LogWarning("ingredient spawner has no package prefab assigned");
            return;
        }

        IngredientPackage package = Instantiate(packagePrefab, spawnPoint.position, spawnPoint.rotation);
        package.Initialize(order);
    }
}
