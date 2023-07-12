using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  //yapay zeka k�t�phanesi  mesh e g�re nesne takip i�in 
using TMPro;

public class mutant : MonoBehaviour
{
    public float Zombi_Can = 100f;
    public Animator anim;
    public bool zombiOldu;

    GameObject hedefOyuncu;
    // public GameObject hedefOyuncu; //Ayn� �ey
    public float mesafe;
    public float KovalamaMesafesi;
    public float SaldirmaMesafesi;
    NavMeshAgent zombiNavmesh;

    KarekterKontrol2 nesne;
    AudioSource seskaynak;
    public AudioClip SolVurusSesi;
    public AudioClip SagVurusSesi;

    Oyun1Canvas nesne2;
    
    int rastgelevurus;
    void Start()
    {
        anim = this.GetComponent<Animator>();
        hedefOyuncu = GameObject.Find("character");  // hedefoyuncu yu public olarak tan�mlayabilirdik  ve burda ekstradan nesneyi tan�tmayabilirdik
                                                     //ama her zombi nesnesi i�in hedefoyuncu(character) imizi atamam�z gerekecekti
        zombiNavmesh = this.GetComponent<NavMeshAgent>(); //gezinme a�� arac� atad�k  nesne takibi i�in
                                                          // nesne = this.gameObject.GetComponent<KarekterKontrol2>(); // her iki s�n�fda ayn� obje i�inde ise
        nesne = GameObject.Find("character").GetComponent<KarekterKontrol2>();//her iki s�n�f farkl� objelerde ise find ie arat�p compenentlerine ula��r�z.
        nesne2 = GameObject.Find("GameManager").GetComponent<Oyun1Canvas>();
      
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
            StartCoroutine(Yoketme());  //zombi old�kten sonra yok etme fonk �a�r�s�        
        }
        else     //zombi ya��yorsa  yap�lacakalar
        {

            if (nesne.hayattaMi == true) //oyuncu hayatta ise
            {
                mesafe = Vector3.Distance(this.transform.position, hedefOyuncu.transform.position);

                if (mesafe <= KovalamaMesafesi && mesafe > SaldirmaMesafesi) // mesafe kovalama mesafesine k���k e�it old. zaman ve 
                {                                                            //mesafenin sald�r� mesafesinden buyuk old zaman

                    zombiNavmesh.SetDestination(hedefOyuncu.transform.position);  //hedef oyuncuyu pozisyonunu navmeshe atad�k hedef oyuncuyu takip edecek
                    zombiNavmesh.isStopped = false;      // takip durdurma kapal�
                    anim.SetBool("run", true); //y�r�me animasyonu
                    anim.SetBool("SolSald�r�", false);
                    anim.SetBool("SagSald�r�", false);
                }
                else if (mesafe <= SaldirmaMesafesi)  //sald�rma durumu
                {
                    zombiNavmesh.isStopped = true;
                    anim.SetBool("run", false);
                    this.transform.LookAt(hedefOyuncu.transform.position); //zombi nesnesinin  hedefe bakmas�n� sa�lar.hedefe do�ru d�ner.

                    rastgelevurus = Random.Range(0,2);
                    if (rastgelevurus==0)
                    {
                        anim.SetBool("SolSald�r�", false);//vurma animasyonu
                        anim.SetBool("SagSald�r�", true);//vurma animasyonu                       
                    }
                    else
                    {
                        anim.SetBool("SagSald�r�", false);//vurma animasyonu
                        anim.SetBool("SolSald�r�", true);//vurma animasyonu
                    }                                                      
                    
                }
                else    //durma durumu
                {
                    zombiNavmesh.isStopped = true;  // takip durdurur
                    anim.SetBool("run", false); //durma animasyonu
                    anim.SetBool("sald�r�", false);
                }
            }
            else  //zombi hayatta ve oyuncu �ldu ise 
            {
                StartCoroutine(YemeAnimasyunu());  //yeme animasyonu �a�r�r
            }
        }
    }

    public void Capsule_Colider_Kapat() //zombi �lunce  �lme animasyonunda bu fonk �a��r�l�r.
    {
        this.gameObject.GetComponent<CapsuleCollider>().enabled=false; // ama� zombi olunce �lme animasyonunda hemen coliderini kapatmak 
    }                                                                  //bu sayede �len zombinin �arp��mas�n� engellemi� oluruz.
    IEnumerator YemeAnimasyunu()
    {
        yield return new WaitForSeconds(3); //3 saniye bekler
        anim.SetBool("oyuncuYeme", true);
    }
    IEnumerator Yoketme()
    {       
        yield return new WaitForSeconds(5); //5 saniye bekler   
        Destroy(this.gameObject);       // bu nesneyi(zombiyi) siler        
        nesne2.olusayisi += 1;
    }
    

    public void HasarAl()
    {
        Zombi_Can -= Random.Range(15, 25);
    }
    public void HasarVer() //HasarAL fonk �a��rd�k string ifadesi olarakda mutant g�nderdik ona g�re hasar vermesi i�in
    {
        hedefOyuncu.GetComponent<KarekterKontrol2>().HasarAL("mutant");
    }
    public void SolHasarVerSes() // sald�rma animasyonunda bu fonk �a��r�l�r.
    {
        seskaynak.PlayOneShot(SolVurusSesi);
    }
    public void SagHasarVerSes() // sald�rma animasyonunda bu fonk �a��r�l�r.
    {
        seskaynak.PlayOneShot(SagVurusSesi);
    }
}
