using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum Divisions
{
    Command,
    Operations,
    Science
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    

    public int shipHealth = 100;
    public int crewHealth = 100;
    
    public int shipHealthMax = 100;
    public int crewHealthMax = 100;

    //These hold spites, will instantiate later
    public GameObject[] crewTiles;
    public GameObject[] eventTiles;

    //just a parent for instantiated objects
    private Transform boardHolder;

    //Lists of objects present on the board
    private List<Crew> crewList = new List<Crew>();
    private List<Event> eventList = new List<Event>();

    //Populated as Crew are generated to prevent repeated names
    public List<string> usedNames = new List<string>();

    // probably don't need this, or at least in this fashion
    private List<Vector3> positions = new List<Vector3>();

    void InitializeList()
    {
        positions.Clear();
    }

    void boardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        //instantiate crew
        //2 of each division
        
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        InitGame();
    }

    void InitGame()
    {
        crewList.Clear();
        eventList.Clear();
        usedNames.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(NameGenerator.generate_name());
    }

    public void AddCrewToList(Crew c)
    {
        crewList.Add(c);
    }

    public void AddEventToList(Event e)
    {
        eventList.Add(e);
    }
}
