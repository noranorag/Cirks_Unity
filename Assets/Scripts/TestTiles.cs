using UnityEngine;

public class TestTiles : MonoBehaviour
{
    public PlayersScript playersScript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playersScript.SetCharacterToTile(3); // Set character to tile 4 (index 3)
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playersScript.SetCharacterToTile(2); // Set character to tile 3 (index 2)
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playersScript.SetCharacterToTile(18); // Set character to tile 20 (index 19)
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playersScript.SetCharacterToTile(20); // Set character to tile 21 (index 20)
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            playersScript.SetCharacterToTile(24); // Set character to tile 24 (index 23)
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            playersScript.SetCharacterToTile(30); // Set character to tile 30 (index 29)
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            playersScript.SetCharacterToTile(40); // Set character to tile 41 (index 40)
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            playersScript.SetCharacterToTile(8); // Set character to tile 9 (index 8)
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            playersScript.SetCharacterToTile(14); // Set character to tile 15 (index 14)
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            playersScript.SetCharacterToTile(36); // Set character to tile 37 (index 36)
        }
    }
}