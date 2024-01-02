using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hareket : MonoBehaviour
{
    public float hiz = 10;
     
    void Start()
    {
        
    }

    void Update()
    {
        float rastgele = Random.Range(1, 5); 

      //  transform.position = new Vector3(rastgele,0,0);

        transform.Translate(Vector3.forward * Time.deltaTime *hiz); //ileri z ekseninde hareket

        if (transform.position.z > 90) // scriptin  bulunduðu nesne z de 30 ve -30 zu aþtýðýnda bu nesneyi siler
        {
            Destroy(gameObject);
        }
        if (transform.position.z < -10)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "elma")
        {
            Destroy(gameObject);     // nesneyi hayvaný siler
            Destroy(other.gameObject);//elmayý siler
        }
    }
}
