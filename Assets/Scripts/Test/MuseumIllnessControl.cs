using UnityEngine;

public class MuseumIllnessControl : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float severity;
    private NPCIllness[] illnesses;
    void Start()
    {
        illnesses = FindObjectsByType<NPCIllness>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }
    void Update()
    {
        foreach (var illnesse in illnesses)
        {
            illnesse.illnessSeverity = severity;   
        }
    }
}
