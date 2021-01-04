using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Components
    private Animator animator;
    private PlayerInput input;

    // Constants
    public float crouchingSpeed = 1.75F;
    public float runningSpeed = 4.5F;
    public float slidingSpeed = 5.0F;
    public float walkingSpeed = 2.25F;

    // Variables
    public Vector2 facing;
    public Vector2 motion;

    public void AerialAttack(InputAction.CallbackContext ctx)
    {

    }

    public void Crouch(InputAction.CallbackContext ctx)
    {
        if(!animator.GetBool("Running") && !animator.GetBool("Sliding"))
            animator.SetBool("Crouching", !ctx.canceled);
    }

    public void Flip(InputAction.CallbackContext ctx)
    {
        float direction = ctx.ReadValue<float>();

        if (ctx.started && !animator.GetBool("Sliding"))
            transform.Rotate(0, this.facing.x != direction ? 180 : 0, 0);

        if (!ctx.canceled)
            this.facing = new Vector2(direction, 0);

    }

    public void HeavyAttack(InputAction.CallbackContext ctx)
    {

    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if (!animator.GetBool("Sliding"))
            this.motion = ctx.ReadValue<Vector2>();

        if (!animator.GetBool("Sliding"))
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
        if(ctx.performed && !animator.GetBool("Crouching"))
        {
            animator.SetBool("Running", true);
            animator.SetBool("Walking", false);

            if (!animator.GetBool("Sliding"))
                this.motion = ctx.ReadValue<Vector2>();

        } else if(ctx.canceled)
            animator.SetBool("Running", false);
    }

    public void SpecialAttack(InputAction.CallbackContext ctx)
    {

    }

    public void Slide(InputAction.CallbackContext ctx)
    {

        if (ctx.performed && animator.GetBool("Running"))
        {
            animator.SetBool("Sliding", true);
            animator.SetBool("Running", false);
            animator.SetBool("Walking", false);
            animator.SetInteger("Slide Timer", 24);
        }

    }

    void Start()
    {
        this.facing = new Vector2(1, 0);

        this.animator = this.GetComponent<Animator>();
        this.input = this.GetComponent<PlayerInput>();
    }

    float GetSpeed()
    {

        if (animator.GetBool("Crouching"))
            return this.crouchingSpeed;
        else if (animator.GetBool("Running"))
            return this.runningSpeed;
        else if (animator.GetBool("Sliding"))
            return this.slidingSpeed;
        else if (animator.GetBool("Walking"))
            return this.walkingSpeed;
        else
            return 0;
    
    }

    void FixedUpdate()
    {
        transform.Translate(this.motion * Time.fixedDeltaTime * this.GetSpeed(), Space.World);

        int slideTimer = animator.GetInteger("Slide Timer");

        if (animator.GetBool("Sliding"))
        {
            animator.SetInteger("Slide Timer", --slideTimer);
            animator.SetBool("Sliding", slideTimer > 0);
        }

    }

}
