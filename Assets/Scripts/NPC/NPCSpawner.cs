using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> npcPrefabs;
    [SerializeField] private Transform[] destinationPoints;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject interactPrompt;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private int npcAmount = 1;
    [SerializeField] private bool spawnOnStart = true;
    [SerializeField] private float overrideInfectionTime = 120f;
    public int spawnDelay;

    private List<GameObject> spawnedNPCs = new List<GameObject>();

    private void Start()
    {
        if (spawnOnStart)
        {
            SpawnNPCs();
        }
    }

    public void SpawnNPCs()
    {
        for (int i = 0; i < npcAmount; i++)
        {
            SpawnNPC();
        }
    }

    public void SpawnNPC()
    {
        if (npcPrefabs.Count == 0)
        {
            Debug.LogWarning("No NPC prefabs assigned!");
            return;
        }

        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        GameObject npcPrefab = npcPrefabs[Random.Range(0, npcPrefabs.Count)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

        GameObject npc = Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation
        );

        NPCMovement move = npc.GetComponent<NPCMovement>();
        InteractText interact = npc.GetComponentInChildren<InteractText>();
        NPCIllness illness = npc.GetComponent<NPCIllness>();
        DialogueTrigger trigger = npc.GetComponentInChildren<DialogueTrigger>();

        if (move != null) 
        {
            move.SetPath(destinationPoints.ToArray());
        }

        if (interact != null)
        {
            if (interactPrompt != null)
            {
                interact.SetInteractText(interactPrompt);
                Debug.Log("interaction text assigned");
            }
            else
            {
                Debug.Log("spawner interact prompt is empty");
            }
            
        }

        if (illness != null)
        {
            illness.SetDialogue(dialogue);
            illness.SetSpawner(this);

            if(overrideInfectionTime > 0)
                illness.infectionRate = 1 / overrideInfectionTime;
        }

        if (trigger != null)
        {
            trigger.SetDialogue(dialogue);
        }

        spawnedNPCs.Add(npc);

        Debug.Log($"Spawned NPC: {npc.name}");
    }

    public void RemoveNPC(GameObject npc)
    {
        if (spawnedNPCs.Contains(npc))
        {
            spawnedNPCs.Remove(npc);
        }

        Destroy(npc);
        Debug.Log("npc destroyed");

        StartCoroutine(SpawnAfterDelay(spawnDelay));
    }

    private IEnumerator SpawnAfterDelay(float delay)
    { 
        yield return new WaitForSeconds(delay);

        SpawnNPC();
    }
}
