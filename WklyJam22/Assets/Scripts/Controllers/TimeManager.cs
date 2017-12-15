using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeState
{
    PAUSED, NORMAL, FAST, FASTER
}
public class TimeManager : MonoBehaviour {

	public static TimeManager instance { get; protected set; }
    public float deltaTime { get; protected set; }
    public TimeState timeState { get; protected set; }

    private void Awake()
    {
        instance = this;
        timeState = TimeState.NORMAL;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Pause();

        RunState();

    }

    void RunState()
    {
        switch (timeState)
        {
            case TimeState.PAUSED:
                deltaTime = 0;
                break;
            case TimeState.NORMAL:
                deltaTime = Time.deltaTime;
                break;
            default:
                deltaTime = Time.deltaTime;
                break;
        }
    }

    void Pause()
    {
        if (timeState == TimeState.PAUSED)
            timeState = TimeState.NORMAL;
        else
            timeState = TimeState.PAUSED;
    }
}
