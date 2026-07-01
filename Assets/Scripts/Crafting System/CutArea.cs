using UnityEngine;

public class CutArea : MonoBehaviour
{
    public ChoppingBoard choppingBoard;

    public void RegisterCut()
    { 
        choppingBoard.RegisterCut();
    }
}
