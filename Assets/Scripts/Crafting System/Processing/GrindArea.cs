using UnityEngine;

public class CGrindArea : MonoBehaviour
{
    public MortarPestle mortarPestle;

    public void RegisterCut()
    { 
        mortarPestle.RegisterCut();
    }
}
