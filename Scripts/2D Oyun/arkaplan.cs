using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arkaplan : MonoBehaviour
{
    //Vector3.left     Vector3(-1,0,0)
    //Vector3.right    Vector3(1,0,0)
    //Vector3.up       Vector3(0,1,0)
    //Vector3.down     Vector3(0,-1,0)
    //Vector3.forward  Vector3(0,0,1)
    //Vector3.back     Vector3(0,0,-1)

    private Vector3 basPozisyon;
    public float speed=3f;
    void Start()
    {
        basPozisyon = transform.position;
    }
  
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed); //-x te hareket

        if (transform.position.x < basPozisyon.x - 40) //arkaplan resminin boyutunun yarýsý kadar çýkarýrýz.
        {                                              //resme  box colider ekleyerek  burada box coliderý size.x ine ulaþýp float deðiþkene atayýp
            transform.position = basPozisyon;          //sonra yarýsýný alýncada baslangýç pozisonundan bu deðeri çýkardýðýmýz zaman  hal olur. 
        }
    }
}
