using System;
using CometEngine;

class Timer : CometBehaviour
{
    private float start_timer_in_ms;
    public float wait_time_in_seconds;
    public bool loop = false;
    [Serialize] //trampa para que sea publica
    private CometEvent SendEvent;
    public event CometDelegate SendEventScript;

    public void Start()
	{
       enabled = false;
	}

    public void StartTimer()
	{
        start_timer_in_ms = CometEngine.Time.gameTime;
        enabled = true;
	}

	// Called Every Frame
	public void Update()
	{
        if (CometEngine.Time.gameTime - start_timer_in_ms > wait_time_in_seconds)
        {
            emit_signal();
            start_timer_in_ms = CometEngine.Time.gameTime;
        } 
    }

    private void emit_signal()
    {
        if (SendEvent != null)
            SendEvent.Invoke();
        if (SendEventScript != null)
            SendEventScript.Invoke();
        if (!loop)
            enabled = false;
    }
}