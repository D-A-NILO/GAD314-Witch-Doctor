using UnityEngine;

public class CoinBag : Grabbable
{
    [SerializeField] private int value = 10;
    [SerializeField] private PlayFromSource playFromSource;
    [SerializeField] private AudioSO coinSFX;

    public int Value => value;

    public void SetValue(int amount)
    {
        value = amount;
    }

    public override void OnInteract(PlayerInteract playerInteract) 
    {
        base.OnInteract(playerInteract);
        playFromSource.PlayAudio(coinSFX);
    }
}
