using UnityEngine;

[CreateAssetMenu(fileName = "new illness", menuName = "Illnesses")]
public class IllnessData : ScriptableObject
{
    public PotionData curePotion;

    public int startingSeverity = 50;

    public int cureAmount = 50;

    public int worsenAmount = 25;
}
