using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public PlayersScript playersScript;
    public DiceRollScript diceRollScript;
    private int currentPlayerIndex = 0;
    private bool isPlayerTurn = true;

    void Start()
    {
        StartCoroutine(HandleTurn());
    }

    private IEnumerator HandleTurn()
    {
        while (true)
        {
            if (isPlayerTurn)
            {
                // Wait for the player to throw the dice
                yield return new WaitUntil(() => playersScript.HasPlayerThrownDice());
                playersScript.ResetPlayerThrowDice();
                isPlayerTurn = false;
            }
            else
            {
                // AI turn
                int diceRoll = Random.Range(1, 7); // Simulate dice throw for AI
                playersScript.MoveCharacter(diceRoll);
                yield return new WaitForSeconds(2); // Wait for AI to complete its move
                isPlayerTurn = true;
            }

            // Switch to the next player
            currentPlayerIndex = (currentPlayerIndex + 1) % playersScript.GetPlayerCount();
            playersScript.SetCurrentPlayer(currentPlayerIndex);
        }
    }
}