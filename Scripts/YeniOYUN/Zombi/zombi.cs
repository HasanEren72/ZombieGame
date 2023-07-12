using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  //yapay zeka kütüphanesi  mesh e göre nesne takip için 
using TMPro;

public class zombi : MonoBehaviour
{
    public float Zombi_Can = 100f;
    public Animator anim;
    public bool zombiOldu;

    GameObject hedefOyuncu;
 // public GameObject hedefOyuncu; //Ayný þey
    public float mesafe;
    public float KovalamaMesafesi;
    public float SaldirmaMesafesi;
    NavMeshAgent zombiNavmesh;

    KarekterKontrol2 nesne;
    AudioSource seskaynak;
    public AudioClip hasarversesi;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        hedefOyuncu = GameObject.Find("character");  // hedefoyuncu yu public olarak tanýmlayabilirdik  ve burda ekstradan nesneyi tanýtmayabilirdik
                                                     //ama her zombi nesnesi için hedefoyuncu(character) imizi atamamýz gerekecekti
        zombiNavmesh = this.GetComponent<NavMeshAgent>(); //gezinme aðý aracý atadýk  nesne takibi için
     // nesne = this.gameObject.GetComponent<KarekterKontrol2>(); // her iki sýnýfda ayný obje içinde ise
        nesne = GameObject.Find("character").GetComponent<KarekterKontrol2>();//her iki sýnýf farklý objelerde ise find ie aratýp compenentlerine ulaþýrýz.
        seskaynak = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Zombi_Can <= 0)
        {
            zombiOldu = true;
        }

        if (zombiOldu == true)
        {
            anim.SetBool("die", true);         
            StartCoroutine(Yoketme());  //zombi oldükten sonra yok etme fonk çaðrýsý        
        }
        else     //zombi yaþýyorsa  yapýlacakalar
        {
            
            if (nesne.hayattaMi == true) //oyuncu hayatta ise
            {
                mesafe = Vector3.Distance(this.transform.position, hedefOyuncu.transform.position);

                if (mesafe <= KovalamaMesafesi && mesafe > SaldirmaMesafesi) // mesafe kovalama mesafesine küçük eþit old. zaman ve 
                {                                                            //mesafenin saldýrý mesafesinden buyuk old zaman

                    zombiNavmesh.SetDestination(hedefOyuncu.transform.position);  //hedef oyuncuyu pozisyonunu navmeshe atadýk hedef oyuncuyu takip edecek
                    zombiNavmesh.isStopped = false;      // takip durdurma kapalý
                    anim.SetBool("run", true); //yürüme animasyonu
                    anim.SetBool("saldýrý", false);                           
                }
                else  if (mesafe <= SaldirmaMesafesi)  //saldýrma durumu
                {
                    zombiNavmesh.isStopped = true;
                    anim.SetBool("run", false);//vurma animasyonu
                    this.transform.LookAt(hedefOyuncu.transform.position); //zombi nesnesinin  hedefe bakmasýný saðlar.hedefe doðru döner.                        
                    anim.SetBool("saldýrý", true);                                 
                }
                else    //durma durumu
                {
                    zombiNavmesh.isStopped = true;  // takip durdurur
                    anim.SetBool("run", false); //durma animasyonu
                    anim.SetBool("saldýrý", false);
                }                      
            }
            else  //zombi hayatta ve oyuncu Öldu ise 
            {
                StartCoroutine(YemeAnimasyunu());  //yeme animasyonu çaýrýr
            }
        }
    }
    public void Capsule_Colider_Kapat() //zombi ölunce  ölme animasyonunda bu fonk çaðýrýlýr.
    {
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;  // amaç zombi olunce ölme animasyonunda hemen coliderini kapatmak 
    }                                                                    //bu sayede ölen zombinin çarpýþmasýný engellemiþ oluruz.
    IEnumerator YemeAnimasyunu()
    {
        yield return new WaitForSeconds(3); //3 saniye bekler
        anim.SetBool("oyuncuYeme", true);
    }
    IEnumerator Yoketme()
    {
        yield return new WaitForSeconds(5); //5 saniye bekler
        Destroy(this.gameObject);       // bu nesneyi(zombiyi) siler
    }
   
    public void HasarAl()
    {
        Zombi_Can -= Random.Range(15, 25);      
    }
    public void HasarVer()   //HasarAL fonk çaðýrdýk string ifadesi olarakda zombi gönderdik ona göre hasar vermesi için
    {  
        hedefOyuncu.GetComponent<KarekterKontrol2>().HasarAL("zombi"); 
    }
    public void HasarVerSes() // saldýrma animasyonunda bu fonk çaðýrýlýr.
    {
        seskaynak.PlayOneShot(hasarversesi);
    }
}
