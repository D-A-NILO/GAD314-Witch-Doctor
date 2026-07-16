using UnityEngine;

public class PotionDebugger : MonoBehaviour
{
    public PotionData startPotion;
    private Bottle bottle;

    void Start()
    {
        bottle = GetComponent<Bottle>();
        bottle.Fill(startPotion);
    }
}
