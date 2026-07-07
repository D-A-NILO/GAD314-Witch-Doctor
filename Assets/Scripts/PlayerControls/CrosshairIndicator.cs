using System.Collections.Generic;
using UnityEngine;

public class CrosshairIndicator : MonoBehaviour
{
    [System.Serializable]
    public class Status
    {
        public string name;
        public Color displayColor;
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

    public Renderer indicatorRend;

    public void SetIndicatorStatus(string status)
    {
        if(!statusDict.ContainsKey(status))
        {
            Debug.Log("Cannot Set indicator Status: " + status + "\nDoes not exist");
        }
        indicatorRend.material.color = statusDict[status].displayColor;
    }
    public void SetIndicatorStatus(int statusIndex)
    {
        if(statusIndex >= statusArray.Length || statusIndex < 0)
        {
            Debug.Log("Cannot Set indicator Status Index: " + statusIndex + "\nDoes not exist");
        }
        indicatorRend.material.color = statusArray[statusIndex].displayColor;
    }
}
