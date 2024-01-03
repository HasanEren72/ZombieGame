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

        if (transform.position.x < basPozisyon.x - 40) //arkaplan resminin boyutunun yar�s� kadar ��kar�r�z.
        {                                              //resme  box colider ekleyerek  burada box colider� size.x ine ula��p float de�i�kene atay�p
            transform.position = basPozisyon;          //sonra yar�s�n� al�ncada baslang�� pozisonundan bu de�eri ��kard���m�z zaman  hal olur. 
        }
    }
}
