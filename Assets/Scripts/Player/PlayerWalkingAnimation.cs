using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingAnimations : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S);

        animator.SetBool("Stop", !isWalking);
    
    }
}