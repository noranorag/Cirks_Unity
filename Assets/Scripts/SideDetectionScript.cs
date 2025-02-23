using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDetectionScript : MonoBehaviour
{
    DiceRollScript diceRollScript;

    void Awake()
    {
        diceRollScript = FindObjectOfType<DiceRollScript>();
    }

    private void OnTriggerStay(Collider sideCollider)
    {
        if (sideCollider != null)
        {
            if (diceRollScript.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                if (!diceRollScript.isLanded)
                {
                    diceRollScript.isLanded = true;
                    diceRollScript.diceFaceNum = sideCollider.name;
                    Debug.Log("Dice face detected: " + diceRollScript.diceFaceNum);
                }
            }
            else
            {
                diceRollScript.isLanded = false;
            }
        }
        else
        {
            Debug.LogError("DiceRollScript is not found");
        }
    }
}