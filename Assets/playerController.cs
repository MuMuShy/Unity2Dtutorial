using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform groundpoint;
    private float inputX;

    private Rigidbody2D rig;
    private Animator ani;

    private bool isFlip = false;
    private bool isGrounded = false;

    public LayerMask groundmask;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        rig.velocity = new Vector2(moveSpeed * inputX, rig.velocity.y);

        ani.SetBool("isRun", Mathf.Abs(rig.velocity.x) > 0);
        ani.SetBool("isGrounded", isGrounded);
        ani.SetFloat("yVelocity", rig.velocity.y);


        if (!isFlip)
        {
            if(rig.velocity.x < 0)
            {
                isFlip = true;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }
        else
        {
            if(rig.velocity.x > 0)
            {
                isFlip = false;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }

        isGrounded = Physics2D.OverlapCircle(groundpoint.position, .2f, groundmask);
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext contex)
    {
        if (isGrounded)
        {
            rig.velocity = new Vector2(rig.velocity.x, 7);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundpoint.position, .2f);
    }
}
