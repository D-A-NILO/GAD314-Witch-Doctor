using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairIndicator : MonoBehaviour
{
    [System.Serializable]
    public class Status
    {
        public string name;
        public Color displayColor = Color.white;
        public Sprite displaySprite;
    }
    [SerializeField] private Status[] statusArray;
    private Dictionary<string, Status> statusDict;
    private void InitStatusDict()
    {
        statusDict = new();
        foreach(Status status in statusArray)
        {
            statusDict.Add(status.name, status);
        }
    }

    void Awake()
    {
        InitStatusDict();
        SetIndicatorStatus(0);
    }

    /// temporary Indicator solution!!
    /// Replace later

    public Image crosshair;

    public void SetIndicatorStatus(string status)
    {
        if(!statusDict.ContainsKey(status))
        {
            Debug.Log("Cannot Set indicator Status: " + status + "\nDoes not exist");
        }
        SetIndicatorDisplay(statusDict[status].displayColor, statusDict[status].displaySprite);
    }
    public void SetIndicatorStatus(int statusIndex)
    {
        if(statusIndex >= statusArray.Length || statusIndex < 0)
        {
            Debug.Log("Cannot Set indicator Status Index: " + statusIndex + "\nDoes not exist");
            return;
        }
        
        SetIndicatorDisplay(statusArray[statusIndex].displayColor, statusArray[statusIndex].displaySprite);
    }

    private void SetIndicatorDisplay(Color color, Sprite sprite)
    {
        crosshair.color = color;
        if(sprite != null)
            crosshair.sprite = sprite;
    }
}
