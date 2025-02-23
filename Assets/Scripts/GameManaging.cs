using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManaging : MonoBehaviour
{
    public DiceRollScript diceRollScript;
    public PlayersScript playersScript;

    void Start()
    {
        if (diceRollScript == null)
        {
            diceRollScript = FindObjectOfType<DiceRollScript>();
        }

        if (playersScript == null)
        {
            playersScript = FindObjectOfType<PlayersScript>();
        }

        if (diceRollScript != null)
        {
            diceRollScript.OnDiceLanded += HandleDiceLanded;
        }
    }

    void HandleDiceLanded(int rolledNumber)
    {
        Debug.Log("HandleDiceLanded called with rolledNumber: " + rolledNumber);
        if (playersScript != null)
        {
            playersScript.MoveCharacter(rolledNumber);
        }
    }
}