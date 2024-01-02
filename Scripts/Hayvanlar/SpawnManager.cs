using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] hayvanlar;
    void Start()
    {
     // Invoke("HayvanUret",0.5f);  //("fonk" , kaç saniye sonra çaðýrýlacak) 5 saniye sonra fonk çaðýrýr
        InvokeRepeating("HayvanUret", 3 , 1f); //("fonk" , kaç saniye sonra çaðýrýlacak , kaç saniye aralýklarla çaðýrýlacak)  3 saniye sonra 1 saniye aralýklarla çaðýrýlacak
    } 
      
    void Update()
    {
        
    }
    
    void HayvanUret()
    {
        int rastgeleXkordinat = Random.Range(-37, 37);
        int hayvanindis = Random.Range(0, hayvanlar.Length);

        Instantiate(hayvanlar[hayvanindis], new Vector3(rastgeleXkordinat, 0, 90), hayvanlar[hayvanindis].transform.rotation);
    }
}
