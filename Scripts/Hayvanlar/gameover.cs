using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameover : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public bool kaybetti = false; // game over if boðu 1 kere çalýþmasý için bool deðiþken tanýmladýk ve kontrol ediyoruz
    void Update()
    {
       
        if (transform.position.z<-9 && kaybetti == false)
        {
            Debug.Log("game over");
            Time.timeScale = 0;
            kaybetti = true;
        }
        
    }
}
