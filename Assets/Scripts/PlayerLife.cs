using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D playerRigidbody2D;
    [SerializeField] private AudioSource dieAudio;
    [SerializeField] private GameObject lose;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("Enermy"))
        {
            Die();
        }
        if (collision.gameObject.CompareTag("DieZone"))
        {
            Die();
        }
        
    }


    private void Die()
    {
        dieAudio.Play();
        animator.SetTrigger("death");
        playerRigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    public void Lose()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 0f;
        lose.SetActive(true);
    }
    public void Restart()
    {
        lose.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
