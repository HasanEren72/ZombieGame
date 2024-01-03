using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private CharacterController controller;

    
    private Animator anim;

    public float movespeed = 5f;

    private float yatayhareket_Degeri;
    private float dikeyhareket_Degeri;


    void Start()
    {     
        controller = GetComponent<CharacterController>() ;
        anim = GetComponent<Animator>();
    }

    


    void Update()
    {
        yatayhareket_Degeri = Input.GetAxis("Horizontal")  * movespeed * Time.deltaTime;
        dikeyhareket_Degeri = Input.GetAxis("Vertical")  * movespeed *Time.deltaTime;


        run();
        wolk();     
        jump();
    }

    void wolk()
    {
        if (yatayhareket_Degeri != 0 || dikeyhareket_Degeri != 0)
        {
            anim.SetBool("WolkForward", true);

        }
        else
        {
            anim.SetBool("WolkForward", false);
        }
    }
    void run()
    {
        if (yatayhareket_Degeri != 0 && Input.GetKeyDown(KeyCode.LeftShift) || dikeyhareket_Degeri != 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("isFastrun", true);

        }
        else
        {
            anim.SetBool("isFastrun", false);
        }
    }
    void jump()
    {
        if (Input.GetButtonDown("Jump") )
        {
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }
    }
}
