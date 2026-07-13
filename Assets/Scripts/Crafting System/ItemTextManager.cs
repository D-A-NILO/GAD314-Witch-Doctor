using TMPro;
using UnityEngine;

public class ItemTextManager : MonoBehaviour
{
    public static ItemTextManager instance;
    public TMP_Text displayTMP;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void SetText(string value)
    {
        instance.displayTMP.text = value;
    }
}
