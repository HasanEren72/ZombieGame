using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarKontrol : MonoBehaviour
{
    public float speed=10f;    
    public float highSpeed = 20f;
    public float donmehiz = 20f;

    void Update()
    {
        //transform.Translate(0, 0, speed * Time.deltaTime); //time.deltatime saniyede 0.003 birime eþdeðer

        float tusDegerX= Input.GetAxis("Horizontal"); //1 ile -1 arasýnda deðer alýr (Yatay)
        float tusDegerZ= Input.GetAxis("Vertical") ;  //(Dikey)

        if (Input.GetKey(KeyCode.LeftShift) && tusDegerZ !=0) //shifte  ve ileriye basýlmýþsa yüksek hýz z ekseninde hareket ve x ekseninde dönme hareketi
        {
            transform.Translate(0, 0, tusDegerZ * Time.deltaTime * highSpeed);
            //transform.Translate(Vector3.forward * tusDegerZ  * Time.deltaTime* highSpeed); üstteki ile aynýdýr
            transform.Rotate(0, tusDegerX  * Time.deltaTime * donmehiz, 0);
            //transform.Rotate(Vector3.up * tusDegerX  * Time.deltaTime* donmehiz); üstteki ile aynýdýr
        }
        else if(tusDegerZ != 0)
        {                                                               //hýz z ekseninde hareket ve x ekseninde dönme hareketi
            transform.Translate(0, 0, tusDegerZ  * Time.deltaTime * speed);
            transform.Rotate(0, tusDegerX  * Time.deltaTime * donmehiz, 0);
        }
          
    }
}
