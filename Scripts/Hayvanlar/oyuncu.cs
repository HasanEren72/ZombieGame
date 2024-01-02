using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyuncu : MonoBehaviour
{
    public GameObject elma;
  
    void Start()
    {
        
    }

    void Update()
    {
        float sagsol = Input.GetAxis("Horizontal");

        transform.Translate(new Vector3(1,0,0)* sagsol);
        //transform.Translate(Vector3.right * sagsol); ayný þey
        //transform.Translate(sagsol,0,0);


        if (transform.position.x>40)
        {
            transform.position = new Vector3(40,0,0);
        }
        if(transform.position.x<-40)
        {
            transform.position = new Vector3(-40,0,0);
        }
       

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(elma,new Vector3(transform.position.x,2,transform.position.z), elma.transform.rotation);
        }
     
    }
}
