using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class firlat : MonoBehaviour
{
    Rigidbody nesneRb;
    public ParticleSystem PatlamaEffectPrefab;
    AudioSource kaynak;
    public AudioClip patlamaSesi;
    public int puan ;
    Gamemanager nesne;

    void Start()
    {

        kaynak =GameObject.Find("GAMEMANAGER").GetComponent<AudioSource>(); //nesne silindi�i i�in kayna�� ba�ka nese olarak atad�m
        nesne = GameObject.Find("GAMEMANAGER").GetComponent<Gamemanager>();

        transform.position = new Vector3(Random.Range(-3,3), -5 ,transform.position.z);
        nesneRb = gameObject.GetComponent<Rigidbody>();
        nesneRb.AddForce(Vector3.up* Random.Range(10,16) ,ForceMode.Impulse);
        nesneRb.AddTorque(new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)), ForceMode.Impulse);
    }

    private void OnMouseDown() //fare t�klama olay�
    {        
        nesne.puanGuncelle(puan);
        Destroy(gameObject);
        kaynak.PlayOneShot(patlamaSesi);
        ParticleSystem patlamaEfekti = Instantiate(PatlamaEffectPrefab, transform.position, transform.rotation);
        Destroy(patlamaEfekti.gameObject, patlamaEfekti.main.duration); //efekt bir kere �al��t�ktan sonra hemen siler
    }
    void Update()
    {
        if (transform.position.y<-10)  
        {
            Destroy(gameObject);
        }
    } //2.yol
    //private void OnTriggerEnter(Collider other) // nesnelerimiz herhangibir nesnenin i�ine girdi�i zaman siler. altta bir tane plane tan�mlar�z
    //{                                          // sonra colliderinin is trigerini a�mam�z yeterli
    //    Destroy(gameObject);
    //}
}
