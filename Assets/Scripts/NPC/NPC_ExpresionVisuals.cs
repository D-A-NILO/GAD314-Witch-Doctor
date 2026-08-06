using System.Collections;
using UnityEngine;

public class NPC_ExpresionVisuals : MonoBehaviour
{
    [SerializeField] private Renderer expressionRenderer;
    
    [SerializeField] private Vector2 blinkIntervalMinMax = new Vector2(8f, 14f);
    [SerializeField] private float blinkLength = 0.4f;
    [SerializeField] private Vector2 yapLengthMinMax = new Vector2(0.5f, 1.5f);

    public bool yapping = false;
    void Start()
    {
        StartCoroutine(blinkLoop());
        expressionRenderer.material.SetInt("_Mouth_Open", 0);
        StartCoroutine(yapLoop());  
    }

    public void SetExpression(Expression expression)
    {
        expressionRenderer.material.SetFloat("_Active_Expresion", (float)expression);
    }

    IEnumerator blinkLoop()
    {
        while(enabled)
        {
            expressionRenderer.material.SetInt("_Eyes_Open", 1);
            yield return new WaitForSeconds(Random.Range(blinkIntervalMinMax.x, blinkIntervalMinMax.y));
            expressionRenderer.material.SetInt("_Eyes_Open", 0);
            yield return new WaitForSeconds(blinkLength);
        }
    }
    
    IEnumerator yapLoop()
    {
        while(enabled)
        {
            if(yapping) 
            {

                expressionRenderer.material.SetInt("_Mouth_Open", 1);
                yield return new WaitForSeconds(Random.Range(yapLengthMinMax.x, yapLengthMinMax.y));
                expressionRenderer.material.SetInt("_Mouth_Open", 0);
                yield return new WaitForSeconds(Random.Range(yapLengthMinMax.x, yapLengthMinMax.y));

            }else
            {
                yield return null;
            }
        }
    }

    public enum Expression : int
    {
        NEUTRAL = 0,
        HAPPY = 1,
        ANNOYED = 2,
        SURPRISED = 3,
        ANGRY = 4,
        HURT = 5,
        DIZZY = 6,
        DEAD = 7,
        BOOP = 8,
    }
}
