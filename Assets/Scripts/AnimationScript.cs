using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void RollDice()
    {
        animator.SetBool("IsRolling", true);
    }

    public void StopRoll()
    {
        animator.SetBool("IsRolling", false);
    }
}
