using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Diagnostics;
using System;
using Random = UnityEngine.Random;

public class Crew : MonoBehaviour
{
    public string crewName;
    public Divisions division;
    // TODO: isChief is potentially used for some event triggers, likely moved to CrewManager
    //public bool isChief = false;

    public int statCmd = 0;
    public int statOps = 0;
    public int statSci = 0;

    private Vector2 startingPosition;
    private List<Transform> touchingTiles;
    private Transform myParent;

    private void Awake()
    {
        Debug.Log("Crew Awake");
        // TODO: loop here to check against a list of names in use, something like GameManager.instance.usedNames.Contains(to_return)
        crewName = NameGenerator.generate_name();

        // TODO: Decide division in some manner to influence work stats
        statCmd = 1 + (int)Math.Round(Random.value * 10f, 0);
        statOps = 1 + (int)Math.Round(Random.value * 10f, 0);
        statSci = 1 + (int)Math.Round(Random.value * 10f, 0);

        Debug.Log(String.Format("Created Crew Member {0}! Stats - Cmd: {1}, Ops: {2}, Sci: {3}", crewName, statCmd, statOps, statSci));

        startingPosition = transform.position;
        touchingTiles = new List<Transform>();
        myParent = transform.parent;
    }

    public int GiveWork(Divisions work_type)
    {
        switch (work_type)
        {
            case Divisions.Command:
                return statCmd;
            case Divisions.Operations:
                return statOps;
            case Divisions.Science:
                return statSci;
            default:
                return 0;
        }
    }

    public void PickUp()
    {
        Debug.Log("PickUp");
        transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }

    public void Drop()
    {
        Debug.Log("Drop");
        transform.localScale = new Vector3(2, 2, 2);
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;

        Vector2 newPosition;
        if (touchingTiles.Count == 0)
        {
            transform.position = startingPosition;
            transform.parent = myParent;
            UpdateWorkOnEvents();
            return;
        }

        var currentEvent = touchingTiles[0];
        if (touchingTiles.Count == 1)
        {
            newPosition = currentEvent.position;
        }
        else
        {
            var distance = Vector2.Distance(transform.position, touchingTiles[0].position);

            foreach (Transform evnt in touchingTiles)
            {
                if (Vector2.Distance(transform.position, evnt.position) < distance)
                {
                    currentEvent = evnt;
                    distance = Vector2.Distance(transform.position, evnt.position);
                }
            }
            newPosition = currentEvent.position;
        }
        if (currentEvent.childCount != 0)
        {
            transform.position = startingPosition;
            transform.parent = myParent;
        }
        else
        {
            transform.parent = currentEvent;
            StartCoroutine(SlotIntoPlace(transform.position, newPosition));
        }
        UpdateWorkOnEvents();
    }


    void UpdateWorkOnEvents()
    {
        GameObject[] goEvents = GameObject.FindGameObjectsWithTag("Event");
        
        foreach (GameObject goE in goEvents)
        {
            Event eComp = goE.GetComponent<Event>();
            if (eComp != null)
            {
                eComp.UpdateActiveWork();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        if (other.tag != "Event") return;
        if (!touchingTiles.Contains(other.transform))
        {
            touchingTiles.Add(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");
        if (other.tag != "Event") return;
        if (touchingTiles.Contains(other.transform))
        {
            touchingTiles.Remove(other.transform);
        }
    }

    IEnumerator SlotIntoPlace(Vector2 startingPos, Vector2 endingPos)
    {
        float duration = 0.1f;
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(startingPos, endingPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = endingPos;
    }
}