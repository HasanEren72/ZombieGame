using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dusman : MonoBehaviour
{
    public float hiz=3;
    private Rigidbody dusmanRb;
    private GameObject oyuncu;
   
    void Start()
    {
        oyuncu = GameObject.Find("oyuncu");
        dusmanRb = GetComponent<Rigidbody>();     
    }
    private bool calisti = false;
    
    void Update()
    {
        Vector3 yon = (oyuncu.transform.position - transform.position).normalized;  //normalized sadece vectorun buyukluðunu 1 e düþürür
        dusmanRb.AddForce(yon*hiz);
   
        if (transform.position.y < -5 && calisti ==false)
        {
            Destroy(this.gameObject);        
            calisti = true;
        }      
    }
}
