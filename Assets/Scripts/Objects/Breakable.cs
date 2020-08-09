using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    Animator anim;
    BoxCollider2D boxCollider;
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Break()
    {
        anim.SetBool("isBroken", true);
        boxCollider.enabled = false;
    }
}
