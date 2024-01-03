using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;  //yapay zeka k�t�phanesi  mesh e g�re nesne takip i�in 
using TMPro;
using UnityEngine.UI;

public class zombi : MonoBehaviour
{
    GameObject hedefOyuncu;

    [SerializeField]
    private float mesafe , KovalamaMesafesi , SaldirmaMesafesi;

    [SerializeField]
    private int Zombi_Can = 100;

    public bool zombiOldu;

    [SerializeField]
    private Animator anim;

    NavMeshAgent zombiNavmesh;
   
    AudioSource seskaynak;
    [SerializeField]
    private AudioClip hasarversesi;

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Gradient gradient;//canbar renklendirmek i�in
    [SerializeField]
    private Image fill;

    [SerializeField]
    private GameObject Canbar;

    KarekterKontrol2 karekterKontrol2;

    private void Awake()
    {
        karekterKontrol2 = GameObject.Find("character").GetComponent<KarekterKontrol2>();//her iki s�n�f farkl� objelerde ise find ie arat�p compenentlerine ula��r�z.
        seskaynak = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        anim = this.GetComponent<Animator>();
        hedefOyuncu = GameObject.Find("character");  // hedefoyuncu yu public olarak tan�mlayabilirdik  ve burda ekstradan nesneyi tan�tmayabilirdik
                                                     //ama her zombi nesnesi i�in hedefoyuncu(character) imizi atamam�z gerekecekti
        zombiNavmesh = this.GetComponent<NavMeshAgent>(); //gezinme a�� arac� atad�k  nesne takibi i�in
              
        SetStartCanBar(Zombi_Can); //can bar�n ilk de�eri atan�r.
        Canbar.SetActive(false);
    }

    void Update()
    {
        SetCanBar(Zombi_Can);

        if (Zombi_Can <= 0)
        {
            zombiOldu = true;
            Canbar.SetActive(false);
        }

        if (zombiOldu == true)
        {
            anim.SetBool("die", true);         
            StartCoroutine(Yoketme());  //zombi old�kten sonra yok etme fonk �a�r�s�        
        }
        else     //zombi ya��yorsa  yap�lacakalar
        {
            
            if (karekterKontrol2.hayattaMi == true) //oyuncu hayatta ise
            {
                mesafe = Vector3.Distance(this.transform.position, hedefOyuncu.transform.position);

                if (mesafe <= KovalamaMesafesi && mesafe > SaldirmaMesafesi) // mesafe kovalama mesafesine k���k e�it old. zaman ve 
                {                                                            //mesafenin sald�r� mesafesinden buyuk old zaman

                    zombiNavmesh.SetDestination(hedefOyuncu.transform.position);  //hedef oyuncuyu pozisyonunu navmeshe atad�k hedef oyuncuyu takip edecek
                    zombiNavmesh.isStopped = false;      // takip durdurma kapal�
                    anim.SetBool("run", true); //y�r�me animasyonu
                    anim.SetBool("saldırı", false);
                    if (mesafe <= 10)
                    {
                        Canbar.SetActive(true);
                    }
                    else
                    {
                        Canbar.SetActive(false);
                    }
                }
                else  if (mesafe <= SaldirmaMesafesi)  //sald�rma durumu
                {
                    zombiNavmesh.isStopped = true;
                    anim.SetBool("run", false);//vurma animasyonu
                    this.transform.LookAt(hedefOyuncu.transform.position); //zombi nesnesinin  hedefe bakmas�n� sa�lar.hedefe do�ru d�ner.                        
                    anim.SetBool("saldırı", true);

                    Canbar.SetActive(true);                    
                }
                else    //durma durumu
                {
                    zombiNavmesh.isStopped = true;  // takip durdurur
                    anim.SetBool("run", false); //durma animasyonu
                    anim.SetBool("saldırı", false);
                    Canbar.SetActive(false);
                }                      
            }
            else  //zombi hayatta ve oyuncu �ldu ise 
            {
                StartCoroutine(YemeAnimasyunu());  //yeme animasyonu �a�r�r
            }
        }
    }
    public void SetStartCanBar(int can)
    {
        slider.maxValue = can;
        slider.value = can;
        fill.color = gradient.Evaluate(1f); // fill imagesine gradient 1f yani full de�erinde olan ye�il rengi atad�k
    }
    public void SetCanBar(int can)
    {
        slider.value = can;
        fill.color = gradient.Evaluate(slider.normalizedValue);//fill imagesine gradient�n 
    }                          //sliderin normalize de�eri atad�k boylece slider degeri de�i�ince renkde de�i�ecek

    public void Capsule_Colider_Kapat() //zombi �lunce  �lme animasyonunda bu fonk �a��r�l�r.
    {
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;  // ama� zombi olunce �lme animasyonunda hemen coliderini kapatmak 
    }                                                                    //bu sayede �len zombinin �arp��mas�n� engellemi� oluruz.
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
    public void HasarVer()   //HasarAL fonk �a��rd�k string ifadesi olarakda zombi g�nderdik ona g�re hasar vermesi i�in
    {  
        hedefOyuncu.GetComponent<KarekterKontrol2>().HasarAL("zombi"); 
    }
    public void HasarVerSes() // sald�rma animasyonunda bu fonk �a��r�l�r.
    {
        seskaynak.PlayOneShot(hasarversesi);
    }
}
