using UnityEngine;

public class TutorialArrowController : MonoBehaviour
{
    [Header("Arrow Visual Reference")]
    
    public GameObject arrowVisual;

    [Header("Optional Offset")]
    public Vector3 offset = new Vector3(0, 1.5f, 0);

    private void Start()
    {
        HideArrow();
    }

    
    public void PointAt(Transform target)
    {
        if (target == null)
        {
            HideArrow();
            return;
        }

        transform.position = target.position + offset;
        transform.rotation = target.rotation;

        if (arrowVisual != null)
            arrowVisual.SetActive(true);
    }

    
    public void HideArrow()
    {
        if (arrowVisual != null)
            arrowVisual.SetActive(false);
    }
}
