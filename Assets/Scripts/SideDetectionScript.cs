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

    // Update is called once per frame
    private void OnTriggerStay(Collider sideCollider)
    {
        if (sideCollider != null)
        {
            if (diceRollScript.GetComponent<Rigidbody>().velocity == Vector3.zero)
            {
                diceRollScript.isLanded = true;
                diceRollScript.diceFaceNum = sideCollider.name;
            }
            else
                diceRollScript.isLanded = false;


        }
        else
            Debug.LogError("DiceRollScript is not found");
        }
    }

