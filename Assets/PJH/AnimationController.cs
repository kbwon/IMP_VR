using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Controls walking animation state and speed based on player movement input
public class AnimationController : MonoBehaviour
{
    public InputActionReference move;
    public Animator animator;

    // Registers and unregisters movement input callbacks
    void OnEnable()
    {
        move.action.started += OnMoveStarted;
        move.action.canceled += OnMoveCanceled;
    }

    void OnDisable()
    {
        move.action.started -= OnMoveStarted;
        move.action.canceled -= OnMoveCanceled;
    }

    // Handles movement start and stop to update animation parameters
    void OnMoveStarted(InputAction.CallbackContext context)
    {
        float moveInputY = context.ReadValue<Vector2>().y;

        bool isMovingForward = moveInputY > 0;
        float animationSpeed = isMovingForward ? 1 : -1;

        SetAnimationParameters(true, animationSpeed);
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        SetAnimationParameters(false, 0);
    }

    // Sets animator parameters for walking state and speed
    void SetAnimationParameters(bool isWalking, float animSpeed)
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetFloat("animSpeed", animSpeed);
    }
}