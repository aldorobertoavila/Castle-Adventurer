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
    public float speed = 4.5F;

    // Variables
    public Vector2 facing;
    public Vector2 motion;

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

    public void Move(InputAction.CallbackContext ctx)
    {
        this.motion = ctx.ReadValue<Vector2>();
    }

    public void Run(InputAction.CallbackContext ctx)
    {
        animator.SetBool("Running", !ctx.canceled);

        this.Move(ctx);
    }

    void Start()
    {
        this.facing = new Vector2(1, 0);
    }

    void FixedUpdate()
    {
        if(!this.animator.GetBool("Crouching"))
            transform.Translate(this.motion * Time.fixedDeltaTime * this.speed, Space.World);
    }

}
