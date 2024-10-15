using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void UpdateAnimation(float x, float y, bool isCrouching)
    {
        //actualozaciones de anim de mov, de agacharse, correr etc
        anim.SetFloat("speedX", x);
        anim.SetFloat("speedY", y);
        anim.SetBool("crouch", isCrouching);
    }
}

