using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour
{
    public GameObject[] Engeller;
    private List<GameObject> engelListesi = new List<GameObject>();
    int rasgeleindis;

    void Start()
    {
        InvokeRepeating("EngelUret", 1f, 5f);
    }

    void Update()
    {
        for (int i =0 ; i<= engelListesi.Count - 1 ; i++)
        {            
            if (engelListesi[i] != null && engelListesi[i].transform.position.x < -55)//engellistesibdeki i. eleman  boþ deðilse ve -55 ten küçük ise
            {
                Destroy(engelListesi[i]);  //engeli siler
                engelListesi.RemoveAt(i); //engeli listeden de siler           
            }
        }
    }

    public void EngelUret()
    {
        rasgeleindis = Random.Range(0, Engeller.Length);
       
        if (rasgeleindis == 0) 
        {                    //nesneyi üretip listeye ekledik ki gerektiðinde silelim diye
            engelListesi.Add(Instantiate(Engeller[rasgeleindis], new Vector3(-30, 0f, -4.51f), Engeller[rasgeleindis].transform.rotation)); 
        }
        else if (rasgeleindis == 1)
        {
            engelListesi.Add(Instantiate(Engeller[rasgeleindis], new Vector3(-30, 0.2f, -4.87f), Engeller[rasgeleindis].transform.rotation));
        }
        else if (rasgeleindis == 2)
        {
            engelListesi.Add(Instantiate(Engeller[rasgeleindis], new Vector3(-30, -0.26f, -5.03f), Engeller[rasgeleindis].transform.rotation));
        }             
    }
}
