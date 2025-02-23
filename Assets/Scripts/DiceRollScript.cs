using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceRollScript : MonoBehaviour
{
    Rigidbody rBody;
    Vector3 position;
    [SerializeField] private float maxRandForceVal, startRollForce;
    float forceX, forceY, forceZ;
    public string diceFaceNum;
    public bool isLanded = false;
    public bool firstThrow = false;

    public delegate void DiceLandedHandler(int rolledNumber);
    public event DiceLandedHandler OnDiceLanded;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        rBody = GetComponent<Rigidbody>();
        rBody.isKinematic = true;
        position = transform.position;
        transform.rotation = new Quaternion(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360), 0);
    }

    private void Update()
    {
        if (rBody != null)
        {
            if (Input.GetMouseButtonDown(0) && isLanded || Input.GetMouseButtonDown(0) && !firstThrow)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.gameObject == this.gameObject)
                    {
                        if (!firstThrow)
                            firstThrow = true;

                        RollDice();
                    }
                }
            }
        }
    }

    public void RollDice()
    {
        rBody.isKinematic = false;
        forceX = Random.Range(0, maxRandForceVal);
        forceY = Random.Range(0, maxRandForceVal);
        forceZ = Random.Range(0, maxRandForceVal);
        rBody.AddForce(Vector3.up * Random.Range(800, startRollForce));
        rBody.AddTorque(forceX, forceY, forceZ);
    }

    public void ResetDice()
    {
        rBody.isKinematic = true;
        firstThrow = false;
        isLanded = false;
        transform.position = position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            StartCoroutine(WaitForDiceToSettle());
        }
    }

    private IEnumerator WaitForDiceToSettle()
    {
        yield return new WaitForSeconds(1.0f); // Wait for 1 second to ensure the dice has settled

        if (!string.IsNullOrEmpty(diceFaceNum) && int.TryParse(diceFaceNum, out int rolledNumber))
        {
            Debug.Log("Dice landed with face number: " + diceFaceNum);
            OnDiceLanded?.Invoke(rolledNumber);
        }
        else
        {
            Debug.LogError("Failed to parse diceFaceNum: " + diceFaceNum);
        }
    }
}