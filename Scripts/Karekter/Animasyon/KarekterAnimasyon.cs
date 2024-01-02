using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarekterAnimasyon : MonoBehaviour
{
    private Animator animator;

    void Start() //
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("sarjordegistir", true);
        }
        else
        {
            animator.SetBool("sarjordegistir", false);
        }

        if (Input.GetKey(KeyCode.LeftShift)) //Getkey basýlý tuttuðu zaman çalýþýr
        {
            animator.SetBool("run",true);
        }
        else
        {
            animator.SetBool("run", false);
        }

       

    }
}
//[SerializeField] private GameObject Karakter;  // [Serializefield]  kullanmanýn nedeni private eriþilemez deðiþkeni inspector panelinde kullanablmektir

//Animator karakterAnimator;

//void Start()
//{
//    karakterAnimator = GetComponent<Animator>();
//}


//void Update()
//{
//    Ziplama();
//    Kayma();
//    dusme(); 
//}
//void Ziplama()
//{
//    if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
//    {
//        karakterAnimator.SetTrigger("ZÝPLA");
//    }
//}
//void Kayma()
//{
//    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
//    {
//        karakterAnimator.SetTrigger("KAYMA");
//    }
//}

//void dusme()
//{
//    if (Input.GetKey(KeyCode.G))
//    {
//        karakterAnimator.SetTrigger("DUSME");
//    }

//}