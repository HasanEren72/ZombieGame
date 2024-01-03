using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWheel : MonoBehaviour
{
    public Animator anim;
    
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

 
    void Update()
    {

        float tusDegerX = Input.GetAxis("Horizontal")*Time.deltaTime*10f; //1 ile -1 arasýnda deðer alýr (Yatay)
        float tusDegerZ = Input.GetAxis("Vertical")*Time.deltaTime*10f;  //(Dikey)


        if (tusDegerX != 0 || tusDegerZ !=0)
        {
            anim.SetBool("tekerlek", true);
           
        }
        else
        {
            anim.SetBool("tekerlek", false);
        }
    }
}
