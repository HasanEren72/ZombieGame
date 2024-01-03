using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kameradondurme : MonoBehaviour
{
    public float dondurmeHizi = 5;
   
    void Start()
    {
       
    }

    void Update()
    {
        float yatayInput = Input.GetAxis("Horizontal");       
        transform.Rotate(Vector3.up, dondurmeHizi*Time.deltaTime * yatayInput);      
    }
}
