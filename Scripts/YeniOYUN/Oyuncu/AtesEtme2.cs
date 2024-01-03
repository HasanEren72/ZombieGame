using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class AtesEtme2 : MonoBehaviour
{
    
    public Camera camera;
    public LayerMask zombiKatman;
    public LayerMask NesneKatmani;
   
    Animator anim;
    public ParticleSystem Muzzleflash;
    public GameObject mermiefekti;
    public GameObject Tozmermiefekti;
   
    KarekterKontrol2 nesne;

    private float Sarjor = 30;   
    private float Sarjorkapasitesi = 30;
    private float Cephane = 300;

    RaycastHit hit;  //ate� etmek i�in ���n
    float GunTimer;
    public float TaramaHizi;
    public float Menzil;

    AudioSource seskaynak;
    public AudioClip ak47AtesSesi;
    public AudioClip M416AtesSesi;
    public AudioClip m16AtesSesi;
    public AudioClip ump45AtesSesi;
    public AudioClip MermiToplamaSesi;
    public AudioClip SarjorDegistirSesi;
    public AudioClip SilahDegistirmeSesi;

    RaycastHit hit2;  //mermi toplamak i�in ���n
    public float mermiKutusuMesafesi;
    public GameObject crosshair;
    public GameObject Etext;

    public GameObject ak47;  
    public GameObject m16a4;
    public GameObject ump45;
    public GameObject M416;

    GameObject [] Silahlar =new GameObject[1]; //silahlar� bu diziye at�caz
    
    private string a, b, c;

    private string kuladi;
    private string sifre;
    void Start()
    {
        Silahlar[0] = ump45;

        nesne = gameObject.GetComponent<KarekterKontrol2>();
        anim = gameObject.GetComponent<Animator>();
        seskaynak= gameObject.GetComponent<AudioSource>();

        kuladi = PlayerPrefs.GetString("kullaniciadi_Kayit");
        sifre = PlayerPrefs.GetString("sifre_Kayit");
        
        StartCoroutine(Silah1Cekme());
        StartCoroutine(Silah2Cekme());
        StartCoroutine(Silah3Cekme());

    }
    IEnumerator Silah1Cekme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "Silah1Cekme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {         
                a = www.downloadHandler.text;
                if (a == "1")
                {
                    System.Array.Resize(ref Silahlar, Silahlar.Length + 1);
                    Silahlar[Silahlar.Length - 1] = ak47;
                    Debug.Log("a �al��t�");
                }
            }
        }
    }
    IEnumerator Silah2Cekme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "Silah2Cekme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                b = www.downloadHandler.text;
                if (b == "1")
                {
                    System.Array.Resize(ref Silahlar, Silahlar.Length + 1);
                    Silahlar[Silahlar.Length - 1] = M416;
                    Debug.Log("b �al��t�");
                }
            }
        }
    }
    IEnumerator Silah3Cekme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "Silah3Cekme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                c = www.downloadHandler.text;
                if (c == "1")
                {
                    System.Array.Resize(ref Silahlar, Silahlar.Length + 1);
                    Silahlar[Silahlar.Length - 1] = m16a4;
                    Debug.Log("c �al��t�");
                }
            }
        }
    }

    void Update()
    {
        if (nesne.hayattaMi == true)
        {
            MermiTopla();
            SliahDegistir();

            if (Input.GetMouseButton(0) && Time.time > GunTimer) //mause sol t�k bas�ld���nda
            {   
                if (Sarjor>0)  //sarjorda mermi varsa
                {
                    GunTimer = Time.time + TaramaHizi;
                    AtesEtme();
                    anim.SetBool("atesEt", true);  //atesEt animasyonu aktif ediyoruz animasyonda AtesEtme fonksinu zaten �a�r�ld��� i�in burada �a��rmaya gerek yok
                    
                    seskaynak.Play();
                    if (ak47.activeSelf)//ak47 secili ise true dondurur
                    {
                        seskaynak.clip = ak47AtesSesi;  //ates sesi ak47AtesSesi yap�yoruz
                    }
                    else if (m16a4.activeSelf)
                    {
                        seskaynak.clip = m16AtesSesi;
                    }
                    else if (ump45.activeSelf)
                    {
                        seskaynak.clip = ump45AtesSesi;
                    }
                    else if (M416.activeSelf)
                    {
                        seskaynak.clip = M416AtesSesi;
                    }
                    
                }
                if(Sarjor<=0)  //�arjor bo� ise
                {
                    anim.SetBool("atesEt", false); //animasyonu durdurur.                  
                }                              
            } 
            
            if (Input.GetMouseButtonUp(0)) //mause sol t�k kald�r�ld���nda
            {
                anim.SetBool("atesEt", false); //animasyonu durdurur.
            }

            if (Sarjor < Sarjorkapasitesi  && Cephane > 0 ) // sarjor da mermi azalm��sa ve cephane de mermi varsa
            {
                if (Input.GetKeyDown(KeyCode.R))  //R tu�una bas�l�rsa  SarjorDegistirme animasyonu �al��t�r  bu animasyonun i�inde
                {                                 //SarjorDegistirme() fonksiyonu �a��r�l�r.
                    anim.SetBool("SarjorDegistirme", true);
                }

            }
            if ( Sarjor <= 0 &&  Cephane > 0 ) // sarjor da mermi bittiyse ve cephane de mermi varsa
            {
                anim.SetBool("SarjorDegistirme", true);
            }    
        } 
        

    }
 
    int index = 1;

    public void SliahDegistir() //silah de�i�tirme i�lemleri
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            seskaynak.PlayOneShot(SilahDegistirmeSesi);//1 kere sesi �alar
                   
            if (index < Silahlar.Length)
            {
                change(index);             
            }
            else
            {
                index = 0;
                change(index);              
            }           
        }
    }
    public void change(int number)
    {
        for (int i = 0; i < Silahlar.Length; i++) //t�m silahlar�n set aktifini false yapar
        {
            Silahlar[i].SetActive(false);
        }
        Silahlar[number].SetActive(true);
        index++;
    }

    public void MermiTopla()   //yerden mermi toplamak i�in
    {
        if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit2, mermiKutusuMesafesi))
        {
            if ( hit2.collider.gameObject.CompareTag("MermiKutusu")) // �arpt��� nesnenin tag'� "MermiKutusu" ise
            {
                crosshair.GetComponent<Image>().material.color = Color.red;  //crosshair rengini de�i�tirir
                Etext.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))  // e tusuna bas�ld�ysa  cephane art�r�r ve o nesneyi siler.
                {
                    Cephane += 100;
                    seskaynak.PlayOneShot(MermiToplamaSesi);
                    Destroy(hit2.collider.gameObject);
                }
            }
            else // �arpt��� nesnenin tag'� "MermiKutusu" de�il ise
            {
                crosshair.GetComponent<Image>().material.color = Color.white;
                Etext.SetActive(false);
            }
        }
    }


    public void AtesEtme()
    {
        if (Sarjor>0)
        {
           // Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // ���n�n y�n�n�  kameran�n orta noktas�nda (croshair)  noktas�nda g�sterir

           // RaycastHit hit;   // ���n vuru�u

            //if (Physics.Raycast(ray, out hit, 100f, zombiKatman))  // (���n , ��k�� vuru�u hedef  , mesafe (mathf.infinity =sonsuz)  , hangi katman (nesne) )
            //{

            //    hit.collider.gameObject.GetComponent<zombi>().HasarAl(); // ���n vuru�unun �arpt��� nesnenin collider�n�n  gameobject 'inden compenentlerine ula�t�k 
            //                                                            // zombi s�n�f�n�n HasarAl fonk �a��rd�k
            //}
            if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit, Menzil, zombiKatman))
            {                                                       //katman� zombi ise farkl� efektler
                Muzzleflash.Play();
                Instantiate(mermiefekti, hit.point, Quaternion.LookRotation(hit.normal)); //mermi izi efekti
                Debug.Log(hit.transform.name);

                if (hit.collider.gameObject.tag=="Zombi") //���n�n �arpt��� nesne tag� zombi ise zombi scripti hasar al fonk �a��r�r
                {
                    hit.collider.gameObject.GetComponent<zombi>().HasarAl(); // ���n vuru�unun �arpt��� nesnenin collider�n�n  gameobject'inden compenentlerine ula�t�k                                                                          
                }
                else if (hit.collider.gameObject.tag == "Mutant") //���n�n �arpt��� nesne tag� Mutant ise mutant scripti hasar al fonk �a��r�r
                {
                    hit.collider.gameObject.GetComponent<mutant>().HasarAl();
                  
                }                            
            }

            else if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit, Menzil, NesneKatmani))
            {                                                     //katman� nesne ise farkl� efektler
                Muzzleflash.Play();
                Instantiate(Tozmermiefekti, hit.point, Quaternion.LookRotation(hit.normal)); //mermi izi efekti
                Debug.Log(hit.transform.name);
            }
           

            Sarjor--;
        }       
    }
    public void SarjorDegistirme()
    {      
        Cephane -= Sarjorkapasitesi - Sarjor;
        Sarjor = Sarjorkapasitesi;
        anim.SetBool("SarjorDegistirme", false);
    }
    public void SarjorDegistirmeSesiPlay()
    {
        seskaynak.PlayOneShot(SarjorDegistirSesi); //�arjor de�i�tirme animasyonu i�inde �a��r�l�r.      
    }

    public float GetSarjor()  // private de�i�kenleri ba�ka s�n�flardan  �a��r�p kullanabilmek i�in 
    {
        return Sarjor;
    }
    public float GetCephane()// private de�i�kenleri ba�ka s�n�flardan  �a��r�p kullanabilmek i�in
    {
        return Cephane;
    }


}
