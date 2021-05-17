using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public string eventName = "Not Set";
    private float workTarget = 500f;
    private float workDone = 0f;
    private bool workVisible = true;
    private int workPerSec = 0;
    private float elapsed = 0f;
    private float elapsed_total = 0f;
    public float failure_time_trigger = 300f;
    private Divisions work_type = Divisions.Command;

    void Start()
    {
        GameManager.instance.AddEventToList(this);

        //base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //elapsed += Time.deltaTime;
        UpdateElapsed();

        if (elapsed >= 1f)
        {
            if (workDone >= workTarget)
            {
                EventSuccess();
            }
            else if (failure_time_trigger > 0f && elapsed_total > failure_time_trigger)
            {
                EventFailure();
            }
            else if (workPerSec == 0)
            {
                Debug.Log("No work");
                elapsed = 0f;
            }
            else
            {
                DoWork();
            }
            Debug.Log("Time remaining: " + (failure_time_trigger - elapsed_total));
        }
    }

    void UpdateElapsed()
    {
        elapsed += Time.deltaTime;
        elapsed_total += Time.deltaTime;
    }

    public void DoWork()
    {
        Debug.Log("Doing Work " + workPerSec);
        workDone += workPerSec * elapsed;
        elapsed = 0f;
        Debug.Log("Remaing Work " + (workTarget - workDone));
    }

    public float WorkRemaining()
    {
        // only display 
        if (workVisible == true)
        {
            return workTarget;
        }
        else
        {
            return -1f;
        }
    } 

    public void UpdateActiveWork()
    {
        Debug.Log("UpdateActiveWork");
        workPerSec = 0;
        Crew[] crewList;

        crewList = GetComponentsInChildren<Crew>();
        foreach(Crew crw in crewList)
        {
            workPerSec += crw.GiveWork(work_type);
        }
        Debug.Log("New workPerSec " + workPerSec);
    }

    public void EventSuccess()
    {
        // TODO: event has succeeded, award ship/crew
        Debug.Log("Event Success! :D");
    }

    public void EventFailure()
    {
        // TODO: event has failed, cause consiquences
        Debug.Log("Event Failure! ;_;");
    }

    public void EventClear()
    {
        // TODO: event resolves without success/failure
    }
}
