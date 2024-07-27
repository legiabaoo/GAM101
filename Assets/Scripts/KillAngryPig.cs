using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAngryPig : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            animator.SetTrigger("Damage");
        }
    }
}
