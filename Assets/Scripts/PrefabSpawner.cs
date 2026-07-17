using Unity.Mathematics;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;
    public bool copyRotation;
    
    public void Spawn()
    {
        Debug.Log("spwn");
        Instantiate(prefab, transform.position, copyRotation ? transform.rotation : Quaternion.identity);
    }
}
