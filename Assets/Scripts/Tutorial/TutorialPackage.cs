using UnityEngine;

public class TutorialPackage : IngredientPackage
{

    private MonoTrigger tutorialTrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    protected override void Start()
    {
        base.Start();
        GameObject obj = GameObject.Find("Open Package");
        if(obj == null) Debug.LogError("Open Package Tutorial Trigger not found");
        else tutorialTrigger = obj.GetComponent<MonoTrigger>();
    }

    public override void Open()
    {
        base.Open();
        if(tutorialTrigger == null) Debug.Log("Open Package does not have trigger");
        else tutorialTrigger?.Trigger();
    }
}
