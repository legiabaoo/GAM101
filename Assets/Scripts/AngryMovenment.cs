using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryMovenment : MonoBehaviour
{
    private SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        float dirX = gameObject.transform.position.x;
        if(dirX < 0f)
        {
            sprite.flipX = false;
        }
        else if(dirX > 0f)
        {
            sprite.flipX = true;
        }
    }
}
