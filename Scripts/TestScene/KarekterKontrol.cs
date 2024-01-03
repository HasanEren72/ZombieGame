using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class KarekterKontrol : MonoBehaviour
 {

    private CharacterController controller;

    private Animator anim;
    [SerializeField]
    private float movespeed;

    private float horValue, verValue;

   
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        horValue = Input.GetAxis("Horizontal") * Time.deltaTime * movespeed ;//yatay hareket
        verValue = Input.GetAxis("Vertical") * Time.deltaTime * movespeed ; //dikey hareket
        
        controller.Move(new Vector3(horValue, 0, verValue));

        if (horValue != 0 || verValue != 0)
        {
            anim.SetBool("ismoving", true);
        }
        else
        {
            anim.SetBool("ismoving", false);
        }
    }
}



