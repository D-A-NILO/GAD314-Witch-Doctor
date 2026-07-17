using System.Collections;
using UnityEngine;

public class TimerTrigger : MonoTrigger
{

    public float timerLength = 10f;
    bool started;
    
    
    public void StartTimer()
    {
        if(started) return; // do not start if already started
        
        started = true;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(timerLength);
        started = false;
        Trigger();
    }

    
}
