using UnityEngine;

public class NPCLookAtPlayer : MonoBehaviour
{
    public float lerpSpeed;
    private Transform player;

    void Start()
    {
      player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if(player == null) return;

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
