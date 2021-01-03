using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Components
    public Animator animator;

    // Constants
    public float crouchingSpeed = 1.75F;
    public float runningSpeed = 4.5F;
    public float walkingSpeed = 2.25F;

    // Variables
    public Vector2 facing;
    public Vector2 motion;

    public void AerialAttack(InputAction.CallbackContext ctx)
    {

    }

    public void Crouch(InputAction.CallbackContext ctx)
    {
        animator.SetBool("Crouching", !ctx.canceled);
    }

    public void Flip(InputAction.CallbackContext ctx)
    {
        float direction = ctx.ReadValue<float>();

        if (ctx.started)
            transform.Rotate(0, this.facing.x != direction ? 180 : 0, 0);

        if (!ctx.canceled)
            this.facing = new Vector2(direction, 0);

    }

    public void HeavyAttack(InputAction.CallbackContext ctx)
    {

    }

    public void Move(InputAction.CallbackContext ctx)
    {
        this.motion = ctx.ReadValue<Vector2>();

        animator.SetBool("Walking", !ctx.canceled);
    }

    public void QuickAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) {
            animator.SetTrigger("Unsheathe");

            // TODO Reset Trigger of Quick Attack when no entity can be hit
            animator.SetTrigger("Quick Attack");
        }

    }

    public void Run(InputAction.CallbackContext ctx)
    {
        this.Move(ctx);

        if(ctx.performed && !animator.GetBool("Crouching"))
        {
            animator.SetBool("Running", true);
            animator.SetBool("Walking", false);
        } else if(ctx.canceled)
            animator.SetBool("Running", false);
    }

    public void SpecialAttack(InputAction.CallbackContext ctx)
    {

    }

    public void Slide(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && animator.GetBool("Running"))
            animator.SetTrigger("Slide");
        else if (ctx.canceled) // when trigger has ended
        {
            animator.SetBool("Running", false);
            animator.SetBool("Walking", true);
        }

    }

    void Start()
    {
        this.facing = new Vector2(1, 0);
    }

    float GetSpeed()
    {

        if (animator.GetBool("Crouching"))
            return this.crouchingSpeed;
        else if (animator.GetBool("Running"))
            return this.runningSpeed;
        else if (animator.GetBool("Walking"))
            return this.walkingSpeed;
        else
            return 0;
    
    }

    void FixedUpdate()
    {
        transform.Translate(this.motion * Time.fixedDeltaTime * this.GetSpeed(), Space.World);
    }

}
