using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public PlayersScript playersScript;
    public DiceRollScript diceRollScript;
    private int currentPlayerIndex = 0;

    void Start()
    {
        Debug.Log("TurnManager started");
        StartCoroutine(HandleTurn());
    }

    private IEnumerator HandleTurn()
    {
        while (true)
        {
            Debug.Log("Current player index: " + currentPlayerIndex);
            if (currentPlayerIndex == 0)
            {
                // Player's turn
                Debug.Log("Player's turn");
                yield return new WaitUntil(() => playersScript.HasPlayerThrownDice());
                playersScript.ResetPlayerThrowDice();
            }
            else
            {
                // AI turn
                Debug.Log("AI's turn");
                int diceRoll = Random.Range(1, 7); // Simulate dice throw for AI
                Debug.Log("AI rolled: " + diceRoll);
                playersScript.MoveCharacter(diceRoll);
                yield return new WaitForSeconds(2); // Wait for AI to complete its move
            }

            // Switch to the next player
            currentPlayerIndex = (currentPlayerIndex + 1) % playersScript.GetPlayerCount();
            playersScript.SetCurrentPlayer(currentPlayerIndex);
        }
    }
}