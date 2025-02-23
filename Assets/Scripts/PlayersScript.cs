using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.IO;

public class PlayersScript : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public GameObject spawnPoint;
    public int[] otherPlayers;
    public int index;

    private const string textFileName = "playerNames";
    private List<GameObject> characters = new List<GameObject>();
    private int currentTileIndex = 0;
    public GameObject[] tiles; // Public array to manually set the order of the tiles
    private bool isMoving = false;
    private int currentPlayerIndex = 0;
    private bool playerHasThrownDice = false;

    void Start()
    {
        int characterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        Debug.Log("SelectedCharacter index: " + characterIndex);

        if (playerPrefabs == null || playerPrefabs.Length == 0)
        {
            Debug.LogError("Player prefabs not assigned in the Inspector.");
            return;
        }

        if (characterIndex < 0 || characterIndex >= playerPrefabs.Length)
        {
            Debug.LogError("Character index out of bounds.");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point not assigned in the Inspector.");
            return;
        }

        GameObject mainCharacter = Instantiate(playerPrefabs[characterIndex], spawnPoint.transform.position, Quaternion.identity);
        if (mainCharacter == null)
        {
            Debug.LogError("Failed to instantiate main character.");
            return;
        }

        mainCharacter.GetComponent<NameScript>().SetPlayerName(PlayerPrefs.GetString("PlayerName"));
        characters.Add(mainCharacter);

        otherPlayers = new int[PlayerPrefs.GetInt("PlayerCount")];
        string[] nameArray = ReadLinesFromFile(textFileName);
        for (int i = 0; i < otherPlayers.Length - 1; i++)
        {
            spawnPoint.transform.position += new Vector3(0.2f, 0, 0.08f);
            index = Random.Range(0, playerPrefabs.Length - 1);
            GameObject character = Instantiate(playerPrefabs[index], spawnPoint.transform.position, Quaternion.identity);
            character.GetComponent<NameScript>().SetPlayerName(nameArray[Random.Range(0, nameArray.Length - 1)]);
            characters.Add(character);
        }

        // Ensure tiles array is set
        if (tiles == null || tiles.Length == 0)
        {
            Debug.LogError("Tiles array is not set in the Inspector.");
        }
        else
        {
            Debug.Log("Tiles array set manually. Total tiles: " + tiles.Length);
            for (int i = 0; i < tiles.Length; i++)
            {
                Debug.Log("Tile " + i + ": " + tiles[i].name + " Position: " + tiles[i].transform.position);
            }
        }

        // Reset currentTileIndex
        currentTileIndex = 0;
        Debug.Log("currentTileIndex reset to 0");
    }

    string[] ReadLinesFromFile(string fName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>(fName);
        if (textAsset != null)
        {
            return textAsset.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else
        {
            Debug.LogError("File not found: " + fName);
            return new string[0];
        }
    }

    public void MoveCharacter(int steps)
    {
        if (!isMoving)
        {
            Debug.Log("MoveCharacter called with steps: " + steps);
            StartCoroutine(MoveCharacterStepByStep(steps));
        }
        else
        {
            Debug.Log("MoveCharacter called but character is already moving.");
        }
    }

    

    private IEnumerator MoveCharacterStepByStep(int steps)
    {
        isMoving = true;
        Debug.Log("MoveCharacterStepByStep started with steps: " + steps);
        Animator animator = characters[currentPlayerIndex].GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetBool("isWalking", true); // Start walking animation
        }

        for (int i = 0; i < steps; i++)
        {
            currentTileIndex++;
            if (currentTileIndex >= tiles.Length)
            {
                currentTileIndex = tiles.Length - 1; // Ensure the character doesn't move beyond the last tile
                break;
            }

            if (tiles != null && tiles.Length > 0)
            {
                Debug.Log("Moving character to tile index: " + currentTileIndex);
                if (characters.Count > 0 && characters[currentPlayerIndex] != null)
                {
                    Vector3 startPosition = characters[currentPlayerIndex].transform.position;
                    Vector3 endPosition = tiles[currentTileIndex].transform.position;
                    endPosition.y = startPosition.y; // Maintain the character's original height
                    float journeyLength = Vector3.Distance(startPosition, endPosition);
                    float startTime = Time.time;

                    while (Vector3.Distance(characters[currentPlayerIndex].transform.position, endPosition) > 0.01f)
                    {
                        float distCovered = (Time.time - startTime) * 2.0f; // Speed of movement
                        float fractionOfJourney = distCovered / journeyLength;
                        characters[currentPlayerIndex].transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);
                        yield return null;
                    }

                    characters[currentPlayerIndex].transform.position = endPosition; // Ensure the character reaches the exact position
                    Debug.Log("Character moved to tile index: " + currentTileIndex + " Position: " + characters[currentPlayerIndex].transform.position);
                }
                else
                {
                    Debug.LogError("Characters list is not initialized or main character is null.");
                    yield break;
                }
            }
            else
            {
                Debug.LogError("Tiles array is not initialized.");
                yield break;
            }

            yield return new WaitForSeconds(0.5f); // Wait for 0.5 seconds before moving to the next tile
        }

        if (animator != null)
        {
            animator.SetBool("isWalking", false); // Stop walking animation
        }

        HandleSpecialTileActions(); // Handle special tile actions after moving

        isMoving = false;
        Debug.Log("MoveCharacterStepByStep completed");
    }

    private void HandleSpecialTileActions()
    {
        switch (currentTileIndex)
        {
            case 3:
                Debug.Log("Special action: Move 2 steps forward from tile 4");
                StartCoroutine(MoveCharacterStepByStep(2));
                break;
            case 2: 
                Debug.Log("Special action: Slide to tile 17 from tile 3");
                currentTileIndex = 14; 
                StartCoroutine(MoveCharacterStepByStep(1)); 
                break;
            case 18: 
                Debug.Log("Special action: Slide to tile 33 from tile 20");
                currentTileIndex = 32; 
                StartCoroutine(MoveCharacterStepByStep(1));
                break;
            case 20: 
                Debug.Log("Special action: Go back to tile 18 from tile 21");
                currentTileIndex = 16; 
                StartCoroutine(MoveCharacterStepByStep(1)); 
                break;
            case 24: 
                Debug.Log("Special action: Move 2 steps forward from tile 24");
                StartCoroutine(MoveCharacterStepByStep(2));
                break;
            case 30: 
                Debug.Log("Special action: Move 2 steps forward from tile 30");
                StartCoroutine(MoveCharacterStepByStep(2));
                break;
            case 40:
                Debug.Log("Special action: Slide down to tile 27 from tile 41");
                currentTileIndex = 25; 
                StartCoroutine(MoveCharacterStepByStep(1)); 
                break;
            case 8: 
            case 14: 
            case 36: 
                Debug.Log("Special action: Stay on the current tile and throw the dice again");
                break;
            default:
                Debug.Log("No special action for this tile");
                break;
        }
    }


    public int GetPlayerCount()
    {
        return characters.Count;
    }

    public void SetCurrentPlayer(int playerIndex)
    {
        currentPlayerIndex = playerIndex;
    }

    public bool HasPlayerThrownDice()
    {
        return playerHasThrownDice;
    }

    public void PlayerThrowDice()
    {
        playerHasThrownDice = true;
    }

    public void ResetPlayerThrowDice()
    {
        playerHasThrownDice = false;
    }

    public int testTileIndex;

    [ContextMenu("Set Character to Tile")]
    public void SetCharacterToTile()
    {
        SetCharacterToTile(testTileIndex);
    }

    // Method to manually set the character's position to a specific tile for testing
    public void SetCharacterToTile(int tileIndex)
    {
        if (tileIndex < 0 || tileIndex >= tiles.Length)
        {
            Debug.LogError("Tile index out of bounds.");
            return;
        }

        currentTileIndex = tileIndex;
        Vector3 targetPosition = tiles[tileIndex].transform.position;
        targetPosition.y = characters[currentPlayerIndex].transform.position.y; // Maintain the character's original height
        characters[currentPlayerIndex].transform.position = targetPosition;
        Debug.Log("Character manually set to tile index: " + tileIndex + " Position: " + characters[currentPlayerIndex].transform.position);

        HandleSpecialTileActions(); // Handle special tile actions after setting the character to the tile
    }
}

