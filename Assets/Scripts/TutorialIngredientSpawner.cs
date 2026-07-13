using UnityEngine;

public class TutorialIngredientSpawner : MonoBehaviour, IInteractable
{
    public GameObject ingredientBundlePrefab;
    public Transform spawnPoint;
    public void OnInteract(PlayerInteract interactor)
    {
        Instantiate(ingredientBundlePrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
