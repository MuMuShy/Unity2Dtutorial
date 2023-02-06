using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform groundpoint;
    public Transform attackPoint;
    public float attackRange = 3f;
    private float inputX;

    private Rigidbody2D rig;
    private Animator ani;

    private bool isFlip = false;
    private bool isGrounded = false;
    private bool canAttack = true;

    public LayerMask groundmask, enemymask;

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
        canAttack = isGrounded;
        
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

    public void Attack(InputAction.CallbackContext context)
    {
        //檢查玩家 是否可以攻擊
        if (canAttack)
        {
            ani.SetBool("attack", true);
        }
        
    }

    private void CheckAttackHit()
    {
        //宣告 打到的目標物件 => 有可能是空的 如果是空的=> 沒打到東西 , 如果有 => 傳送指令給被攻擊者
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemymask);

        foreach(Collider2D collider in detectedObjects)
        {
            Debug.Log(collider.gameObject.name);
        }
    }

    public void EndAttack()
    {
        ani.SetBool("attack", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundpoint.position, .2f);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
