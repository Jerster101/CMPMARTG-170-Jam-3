using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour 
{
    static Dictionary<string, List<Character>> units = new Dictionary<string, List<Character>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<Character> turnTeam = new Queue<Character>();

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
	}

    static void InitTeamTurnQueue()
    {
        List<Character> teamList = units[turnKey.Peek()];

        foreach (Character unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }

        StartTurn();
    }

    public static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            turnTeam.Peek().BeginTurn();
        }
    }

    public static void EndTurn()
    {
        Debug.Log("beginnig");
        Character unit = turnTeam.Dequeue();
        unit.EndTurn();

Debug.Log("middle");
        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
        Debug.Log("end");
    }

    public static void AddUnit(Character unit)
    {
        List<Character> list;

        if (!units.ContainsKey(unit.tag))
        {
            list = new List<Character>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }
        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }
}
