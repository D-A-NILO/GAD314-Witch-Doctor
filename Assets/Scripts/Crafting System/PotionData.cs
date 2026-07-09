using UnityEngine;

[CreateAssetMenu(fileName = "PotionData", menuName = "Crafting/Potion")]
public class PotionData : ScriptableObject
{
    public PotionID potionID;
    public Color color;
}
