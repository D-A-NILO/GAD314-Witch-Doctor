using System.Collections.Generic;
using UnityEngine;

public class IngredientPackage : Grabbable
{
    [SerializeField] private float scatterRadius = 1f;
    [SerializeField] private GameObject packageParticle;

    private GameObject packageParticleInstance;

    private Dictionary<ShopItemData, int> contents;
    private bool isOpen;

    public void Initialize(Dictionary<ShopItemData, int> order)
    {
        contents = new Dictionary<ShopItemData, int>(order);
    }


    public virtual void Open()
    {
        if (isOpen || contents == null) return;
        isOpen = true;

        foreach (KeyValuePair<ShopItemData, int> entry in contents)
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
        SpawnParticle();

        Destroy(gameObject, 0.1f);
    }

    void SpawnParticle()
    {
        packageParticleInstance = Instantiate(packageParticle, transform.position, Quaternion.identity);
    }
}
