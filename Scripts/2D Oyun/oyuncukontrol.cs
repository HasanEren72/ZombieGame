using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class oyuncukontrol : MonoBehaviour
{
    public float ZıplamaUzunlugu = 3;
    Rigidbody rb;
    bool zeminde = true;

    public Animator anim;

    public ParticleSystem TozEfekti;
    public ParticleSystem patlamaEfekti;

    private AudioSource sesKaynak;
    public AudioClip ziplamaSesi;
    public AudioClip patlamaSesi;

    public AudioSource arkaplanSesi;
    public GameObject GameOverPaneli;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = Physics.gravity * 1.3f; //yerçekmi artırdık
        anim = gameObject.GetComponent<Animator>();
        patlamaEfekti.Stop();
        sesKaynak = gameObject.GetComponent<AudioSource>();
        arkaplanSesi = GameObject.Find("SpawnManager").GetComponent<AudioSource>(); // nesnesinin  audio source sine ulaştık 
                                                                                    //sesi kapatmak için.  Eğer tüm sesleri kapatmak istiyorsak 
                                                                                    // kameranın AudioListener  bileşenini kullanmak gerek

        GameOverPaneli.SetActive(false);
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && zeminde == true) //zıplamak için
        {
            //transform.Translate(Vector3.up*ZıplamaUzunlugu);
            rb.AddForce(Vector3.up * 650, ForceMode.Impulse);  //aynı şeyler 
            // rb.velocity = Vector3.up * 300*Time.deltaTime;
            anim.SetBool("jump", true);
            sesKaynak.PlayOneShot(ziplamaSesi, 1.0f); //sesin value değerini ayarlayabiliriz
            zeminde = false;
            TozEfekti.Stop();
        }
        if (Input.GetKeyDown(KeyCode.S))// kaymak için
        {
            anim.SetTrigger("slide");
        }
    }

    private void OnCollisionEnter(Collision collision) //çarpışma old zaman
    {
        if (collision.gameObject.tag == "ground") // çarptığı nesne tagı ground ise 
        {
            zeminde = true;
            anim.SetBool("jump", false);
            TozEfekti.Play();
        }
        else if (collision.gameObject.tag == "engel")// çarptığı nesne engel ground ise
        {
            sesKaynak.PlayOneShot(patlamaSesi);
            anim.SetBool("die", true);
            Invoke("zamanıdurdur", 1.6f);  //1.6 saniye sonra çağırır.
            Debug.Log("Game Over");
            TozEfekti.Stop();
            patlamaEfekti.Play();

            arkaplanSesi.Stop(); //kameranın audio source sini kapattık dolayısıyla tum sesleri kapatır.
            GameOverPaneli.SetActive(true);
        }
    }
    //private void OnTriggerEnter(Collider other)  //nesneler çarpıştığı zaman triger ı tetitkliyor yani nesnelerden birinin istriger ı açık 
    //{                                            //ise bu nesneler birbirinin içine girecek  bu durumda bu fonsiyon çalışır.
    //   
    //}
    public void zamanıdurdur()
    {
        Time.timeScale = 0;
    }

    public void Restart()
    {
        GameOverPaneli.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("Not3");
    }
    public void EXİT()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
