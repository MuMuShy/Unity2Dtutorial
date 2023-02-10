using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float hp;
    private Animator ani;

    private void Start()
    {
        ani = GetComponent<Animator>();
        hp = 30.0f;
        Debug.Log("hi i am enemy my hp is:" + hp);
    }

    //³Q§ðÀ»ªºfunction
    public void onDamage(float damage)
    {
        hp = hp - damage;
        ani.SetTrigger("onDamage");
        Debug.Log("now hp:" + hp);

        if(hp <= 0)
        {
            ani.SetBool("Death", true);
        }
    }


}
