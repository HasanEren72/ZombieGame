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

    RaycastHit hit;  //ateþ etmek için ýþýn
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

    RaycastHit hit2;  //mermi toplamak için ýþýn
    public float mermiKutusuMesafesi;
    public GameObject crosshair;
    public GameObject Etext;

    public GameObject ak47;  
    public GameObject m16a4;
    public GameObject ump45;
    public GameObject M416;

    GameObject [] Silahlar =new GameObject[1]; //silahlarý bu diziye atýcaz
    
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
                    Debug.Log("a çalýþtý");
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
                    Debug.Log("b çalýþtý");
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
                    Debug.Log("c çalýþtý");
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

            if (Input.GetMouseButton(0) && Time.time > GunTimer) //mause sol týk basýldýðýnda
            {   
                if (Sarjor>0)  //sarjorda mermi varsa
                {
                    GunTimer = Time.time + TaramaHizi;
                    AtesEtme();
                    anim.SetBool("atesEt", true);  //atesEt animasyonu aktif ediyoruz animasyonda AtesEtme fonksinu zaten çaýrýldýðý için burada çaðýrmaya gerek yok
                    
                    seskaynak.Play();
                    if (ak47.activeSelf)//ak47 secili ise true dondurur
                    {
                        seskaynak.clip = ak47AtesSesi;  //ates sesi ak47AtesSesi yapýyoruz
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
                if(Sarjor<=0)  //þarjor boþ ise
                {
                    anim.SetBool("atesEt", false); //animasyonu durdurur.                  
                }                              
            } 
            
            if (Input.GetMouseButtonUp(0)) //mause sol týk kaldýrýldýðýnda
            {
                anim.SetBool("atesEt", false); //animasyonu durdurur.
            }

            if (Sarjor < Sarjorkapasitesi  && Cephane > 0 ) // sarjor da mermi azalmýþsa ve cephane de mermi varsa
            {
                if (Input.GetKeyDown(KeyCode.R))  //R tuþuna basýlýrsa  SarjorDegistirme animasyonu çalýþtýr  bu animasyonun içinde
                {                                 //SarjorDegistirme() fonksiyonu çaðýrýlýr.
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

    public void SliahDegistir() //silah deðiþtirme iþlemleri
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            seskaynak.PlayOneShot(SilahDegistirmeSesi);//1 kere sesi çalar
                   
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
        for (int i = 0; i < Silahlar.Length; i++) //tüm silahlarýn set aktifini false yapar
        {
            Silahlar[i].SetActive(false);
        }
        Silahlar[number].SetActive(true);
        index++;
    }

    public void MermiTopla()   //yerden mermi toplamak için
    {
        if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit2, mermiKutusuMesafesi))
        {
            if ( hit2.collider.gameObject.CompareTag("MermiKutusu")) // Çarptýðý nesnenin tag'ý "MermiKutusu" ise
            {
                crosshair.GetComponent<Image>().material.color = Color.red;  //crosshair rengini deðiþtirir
                Etext.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))  // e tusuna basýldýysa  cephane artýrýr ve o nesneyi siler.
                {
                    Cephane += 100;
                    seskaynak.PlayOneShot(MermiToplamaSesi);
                    Destroy(hit2.collider.gameObject);
                }
            }
            else // Çarptýðý nesnenin tag'ý "MermiKutusu" deðil ise
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
           // Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // ýþýnýn yönünü  kameranýn orta noktasýnda (croshair)  noktasýnda gösterir

           // RaycastHit hit;   // ýþýn vuruþu

            //if (Physics.Raycast(ray, out hit, 100f, zombiKatman))  // (ýþýn , çýkýþ vuruþu hedef  , mesafe (mathf.infinity =sonsuz)  , hangi katman (nesne) )
            //{

            //    hit.collider.gameObject.GetComponent<zombi>().HasarAl(); // ýþýn vuruþunun çarptýðý nesnenin colliderýnýn  gameobject 'inden compenentlerine ulaþtýk 
            //                                                            // zombi sýnýfýnýn HasarAl fonk çaðýrdýk
            //}
            if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit, Menzil, zombiKatman))
            {                                                       //katmaný zombi ise farklý efektler
                Muzzleflash.Play();
                Instantiate(mermiefekti, hit.point, Quaternion.LookRotation(hit.normal)); //mermi izi efekti
                Debug.Log(hit.transform.name);

                if (hit.collider.gameObject.tag=="Zombi") //ýþýnýn çarptýðý nesne tagý zombi ise zombi scripti hasar al fonk çaðýrýr
                {
                    hit.collider.gameObject.GetComponent<zombi>().HasarAl(); // ýþýn vuruþunun çarptýðý nesnenin colliderýnýn  gameobject'inden compenentlerine ulaþtýk                                                                          
                }
                else if (hit.collider.gameObject.tag == "Mutant") //ýþýnýn çarptýðý nesne tagý Mutant ise mutant scripti hasar al fonk çaðýrýr
                {
                    hit.collider.gameObject.GetComponent<mutant>().HasarAl();
                  
                }                            
            }

            else if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit, Menzil, NesneKatmani))
            {                                                     //katmaný nesne ise farklý efektler
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
        seskaynak.PlayOneShot(SarjorDegistirSesi); //þarjor deðiþtirme animasyonu içinde çaðýrýlýr.      
    }

    public float GetSarjor()  // private deðiþkenleri baþka sýnýflardan  çaðýrýp kullanabilmek için 
    {
        return Sarjor;
    }
    public float GetCephane()// private deðiþkenleri baþka sýnýflardan  çaðýrýp kullanabilmek için
    {
        return Cephane;
    }


}
