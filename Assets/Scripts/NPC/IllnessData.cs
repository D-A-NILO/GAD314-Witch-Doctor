using UnityEngine;

[CreateAssetMenu(fileName = "new illness", menuName = "Illnesses")]
public class IllnessData : ScriptableObject
{
    public PotionData curePotion;

    public float startingSeverity = 0.1f;
    public float cureAmount = 1f;
    public float worsenAmount = 0.2f;
    public int coinReward = 10;

    public string[] initialDialogue;
    public string[] curedDialogue;
    public string[] deathDialogue;
}
