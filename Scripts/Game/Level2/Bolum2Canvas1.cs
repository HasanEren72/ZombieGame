using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI (user interface) kullanıcu arayüzü kütüphanesi
using TMPro; //Text mesh pro kütüphanesi  
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Bolum2Canvas1 : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI CanText, MermiText, olu_text, dalgaText;

    [SerializeField]
    private GameObject OyunDurduPaneli;

    [SerializeField]
    private GameObject ZombileriOldurBtn;

    GameObject oyuncu;

    public int olusayisi = 0;
    public int nesnesayisi = 0;

    [SerializeField]
    private GameObject[] ClonZombiler1, ClonZombiler2, ClonZombiler3;  //her bir dizide farklı özellikte zombiler olacak;

    [SerializeField]
    private GameObject BolumCanavari;

    [SerializeField]
    private GameObject dalgatextobjesi;

    [SerializeField]
    private GameObject karekterKamerasi, DurduKamerasi;

    [SerializeField]
    private int altin = 0, elmas = 0, puan = 0; //veriler için bu değişkenler kullanılır

    [SerializeField]
    private int Altin_Kayit, Elmas_Kayit, Puan_Kayit; //playeprefabs()  sistemi için verileri oyunda tutmak için herhangibir sınıftan direk erişebiliriz ama verinin en son haline
                                                      //veritabanına verileri kaydetmek istemez isek playeprefabs() a bu altın elmas değişkenleri kaydedip sonra bu değişkenlere atayabiliriz.
    public static string kullaniciadi_str, sifre_str, puan_str, toplamAltin_str, elmas_str;  // verileri veritabanına kaydetmek için değişkenler  // altın ,elmas gibi değişkenleri direk atayabilirz bu değişkenlere yada playeprefabs() ile oyunda kaydedilmiş verileri bu degiskenlere 
                                                                                             //atayabiliriz.  
    bool pausePanelinde = false;
    bool OyunBittimi;

    AudioSource audiosource;
    [SerializeField]
    private AudioClip oyunBittiSesi;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }

    void Start()
    {
        oyuncu = GameObject.Find("character"); //character nesnesini aratıp private nesneye atadık  karekterkontrol2 ve atesetme2 sınıflarına ulaşmak için

        dalgaText.text = "1.Dalga Başlıyor .....";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        for (int i = 0; i < 10; i++)  //1.Dalga rasgele 10 zombi
        {
            float xpozisyon = 0f;
            float zpozisyon = 0f;
            int rastgeleBolge = Random.Range(1, 4);
            if (rastgeleBolge == 1) //1,bölge
            {
                xpozisyon = Random.Range(-13, 14);
                zpozisyon = Random.Range(9, 18);
            }
            else if (rastgeleBolge == 2) //2.bölge
            {
                xpozisyon = Random.Range(-13, 14);
                zpozisyon = Random.Range(9, 18);
            }
            else if (rastgeleBolge == 3) //3.bölge
            {
                xpozisyon = Random.Range(-13, 14);
                zpozisyon = Random.Range(9, 18);
            }

            float RastgeleRotasyonY = Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

            int zombİndis = Random.Range(0, ClonZombiler1.Length);
            GameObject nesne = Instantiate(ClonZombiler1[zombİndis], new Vector3(xpozisyon, -2f, zpozisyon), randomRotation);
            nesnesayisi++;
        }

        kullaniciadi_str = PlayerPrefs.GetString("kullaniciadi_Kayit");
        sifre_str = PlayerPrefs.GetString("sifre_Kayit");
        // InvokeRepeating("AzaltNesneSayisi", 1f, 0.5f);
    }

    IEnumerator dalgaTextKapat()
    {
        yield return new WaitForSeconds(3); //3 saniye bekler
        dalgatextobjesi.SetActive(false);
    }

    public void Dalga2()    //2.Dalga farklı özelliklere sahip rasgele 20 zombi
    {
        dalgaText.text = "2.Dalga Başlıyor ... Dikatli olun evrim geçirmişler artık daha güçlüler !!!";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        for (int i = 0; i < 20; i++)  //1.Dalga rasgele 10 zombi
        {
            float xpozisyon = 0f;
            float zpozisyon = 0f;
            int rastgeleBolge = Random.Range(1, 4);
            if (rastgeleBolge == 1) //1,bölge
            {
                xpozisyon = Random.Range(-13, 14);
                zpozisyon = Random.Range(9, 18);
            }
            else if (rastgeleBolge == 2) //2.bölge
            {
                xpozisyon = Random.Range(-13, 14);
                zpozisyon = Random.Range(9, 18);
            }
            else if (rastgeleBolge == 3) //3.bölge
            {
                xpozisyon = Random.Range(-13, 14);
                zpozisyon = Random.Range(9, 18);
            }

            float RastgeleRotasyonY = Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

            int zombİndis = Random.Range(0, ClonZombiler2.Length);
            GameObject nesne = Instantiate(ClonZombiler2[zombİndis], new Vector3(xpozisyon, -2f, zpozisyon), randomRotation);
            nesnesayisi++;
        }
    }
    public void Dalga3()    //3.Dalga farklı özelliklere sahip rasgele 30 zombi
    {
        dalgaText.text = "3.Dalga Başlıyor ...  Dikatli olun evrim geçirmişler artık daha güçlüler !!!";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        for (int i = 0; i < 40; i++)  //1.Dalga rasgele 10 zombi
        {
            float xpozisyon = 0f;
            float zpozisyon = 0f;
            int rastgeleBolge = Random.Range(1, 4);
            if (rastgeleBolge == 1) //1,bölge
            {
                xpozisyon = Random.Range(-13, 14);
                zpozisyon = Random.Range(9, 18);
            }
            else if (rastgeleBolge == 2) //2.bölge
            {
                xpozisyon = Random.Range(-13, 14);
                zpozisyon = Random.Range(9, 18);
            }
            else if (rastgeleBolge == 3) //3.bölge
            {
                xpozisyon = Random.Range(-13, 14);
                zpozisyon = Random.Range(9, 18);
            }

            float RastgeleRotasyonY = Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

            int zombİndis = Random.Range(0, ClonZombiler3.Length);
            GameObject nesne = Instantiate(ClonZombiler3[zombİndis], new Vector3(xpozisyon, -2f, zpozisyon), randomRotation);
            nesnesayisi++;
        }
    }
    public void BolumSonuCanavari()
    {
        dalgaText.text = "Dikkat Bölüm Sonu Canavarı Geliyor , Çok Tehlikeli Ve  Güçlü Savunmaya Gücüne Sahip !!!";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        float xpozisyon = 0f;
        float zpozisyon = 0f;
        int rastgeleBolge = Random.Range(1, 4);
        if (rastgeleBolge == 1) //1,bölge
        {
            xpozisyon = Random.Range(-13, 14);
            zpozisyon = Random.Range(9, 18);
        }
        else if (rastgeleBolge == 2) //2.bölge
        {
            xpozisyon = Random.Range(-13, 14);
            zpozisyon = Random.Range(9, 18);
        }
        else if (rastgeleBolge == 3) //3.bölge
        {
            xpozisyon = Random.Range(-13, 14);
            zpozisyon = Random.Range(9, 18);
        }

        float RastgeleRotasyonY = Random.Range(0, 360);
        Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

        GameObject nesne = Instantiate(BolumCanavari, new Vector3(xpozisyon, -2f, zpozisyon), randomRotation);
        nesnesayisi++;
    }
    void AzaltNesneSayisi()  //zombi ölü sayılarını tutabilmek için.
    {
        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul

        int aktifZombiSayisi = zombiNesneleri.Length;

        if (aktifZombiSayisi < nesnesayisi && pausePanelinde == false) //  akif zombi azaldığı zaman  ve pause paneli kapalı ise
        {
            nesnesayisi = aktifZombiSayisi;
            olusayisi += 1;
            olu_text.text = "Ölü Sayisi:" + olusayisi.ToString();

            altin += 100;
            PlayerPrefs.SetInt("Altin_verisi", altin);
            Altin_Kayit = PlayerPrefs.GetInt("Altin_verisi");

            puan += 200;
            PlayerPrefs.SetInt("Puan_verisi", puan);
            Puan_Kayit = PlayerPrefs.GetInt("Puan_verisi");
        }
        if (aktifZombiSayisi == 0 && olusayisi == 10) //2.dalga
        {
            Dalga2();
            elmas += 5;
            PlayerPrefs.SetInt("Elmas_verisi", elmas);
            Elmas_Kayit = PlayerPrefs.GetInt("Elmas_verisi");
        }
        else if (aktifZombiSayisi == 0 && olusayisi == 30) //3.dalga
        {
            Dalga3();
            elmas += 10;
            PlayerPrefs.SetInt("Elmas_verisi", elmas);
            Elmas_Kayit = PlayerPrefs.GetInt("Elmas_verisi");
        }
        else if (aktifZombiSayisi == 0 && olusayisi == 70) //bolum sonu canavarı çağrı
        {
            BolumSonuCanavari();
            elmas += 50;
            PlayerPrefs.SetInt("Elmas_verisi", elmas);
            Elmas_Kayit = PlayerPrefs.GetInt("Elmas_verisi");
        }
        else if (aktifZombiSayisi == 0 && olusayisi == 72)//kazanmak
        {
            dalgaText.text = "*** Tebrikler Kazandınız *** Sonraki bölüme geçmek için AnaMenuye dönün";
            audiosource.PlayOneShot(oyunBittiSesi);
            karekterKamerasi.SetActive(false);
            DurduKamerasi.SetActive(true);
            Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini kaldırır.
            dalgatextobjesi.SetActive(true);
            StartCoroutine(dalgaTextKapat());
            StartCoroutine(skorlar_ekleme());//skorları veritabanına ekler
            Time.timeScale = 0;
        }
    }
    public void Zombilerioldur()  //ZombileriOldurBtn butona basılırsa çağırılacak
    {                             //tagı zombi olan tum nesneleri bir diziye attık sonra for ile tek tek sildik öldürdük
        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul
        int aktifZombiSayisi = zombiNesneleri.Length;

        for (int i = 0; i < zombiNesneleri.Length; i++)
        {
            Destroy(zombiNesneleri[i]);
            olusayisi += 1;
        }
        olusayisi -= 1; // AzaltNesneSayisi fonksiyonunda 1 tane fazla artırdığı için 1 azaltık
        ZombileriOldurBtn.SetActive(false);
    }

    void Update()    // MermiText textine oyuncu nesnesin kompenentlerine ulaşıp  AtesEtme2 sınıfından  GetSarjor() ve GetCephane() 
    {                //fonk çağırıp stinge döbüştürerek atadık
        MermiText.text = oyuncu.GetComponent<AtesEtme2>().GetSarjor().ToString() + "/" + oyuncu.GetComponent<AtesEtme2>().GetCephane().ToString();
        CanText.text = "Can: %" + oyuncu.GetComponent<KarekterKontrol2>().GetKarekerCan().ToString();

        if (Input.GetKey(KeyCode.Escape)) // esc ye basınca oyunu durdurur.
        {
            OyunuDurdur();
        }
        if (Input.GetKey(KeyCode.K)) //k ye basıldığı zaman pause panelinde ZombileriOldurBtn aktif eder.
        {
            ZombileriOldurBtn.SetActive(true);
        }
        AzaltNesneSayisi();

        puan_str = Puan_Kayit.ToString();  //Burada sürekli güncellenen skorlar  bu değişkene atanır 
        toplamAltin_str = Altin_Kayit.ToString();
        elmas_str = Elmas_Kayit.ToString();
    }
    // skor ekleme işlemleri

    public IEnumerator skorlar_ekleme() // veritabanına güncellenmiş şekilde skorları günceller
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "skorlar_ekleme");
        form.AddField("kullaniciAdi", kullaniciadi_str);
        form.AddField("sifre", sifre_str);
        form.AddField("puan", puan_str);
        form.AddField("toplamAltin", toplamAltin_str);
        form.AddField("top_elmas", elmas_str);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Skorlar kayıt Sonucu başarılı:" + www.downloadHandler.text);
            }
        }
    }

    public void OyunuDevamEt()
    {
        Time.timeScale = 1; //oyunu devam ettirir.
        OyunDurduPaneli.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;// fare imlecini kilitler
        DurduKamerasi.SetActive(false);
        karekterKamerasi.SetActive(true);

        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul diziye at

        for (int i = 0; i < zombiNesneleri.Length; i++)   // oyun devam ederse " tüm zombilerin SESLERİNİ AÇMAK İÇİN" 
        {
            AudioSource zombiSesi = zombiNesneleri[i].GetComponent<AudioSource>();
            if (zombiSesi != null)  // Zombi nesnesinde ses bileşeni varsa
            {
                zombiSesi.enabled = true;  // Zombi sesini etkinleştir
            }
        }
        // Raycast işlemini aktif hale getirir.
        Physics.queriesHitTriggers = true;

        pausePanelinde = false;
    }

    public void OyunuDurdur()
    {
        pausePanelinde = true;
        Time.timeScale = 0; //oyunu durdurur.
        OyunDurduPaneli.SetActive(true);  //devam et butonunu aktif yapar
        Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini kaldırır.
        DurduKamerasi.SetActive(true);
        karekterKamerasi.SetActive(false);

        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul diziye at

        for (int i = 0; i < zombiNesneleri.Length; i++) //oyun durmuşsa " tüm zombilerin SESLERİNİ KESMEK İÇİN" 
        {
            AudioSource zombiSesi = zombiNesneleri[i].GetComponent<AudioSource>();
            if (zombiSesi != null)  // Zombi nesnesinde ses bileşeni varsa
            {
                zombiSesi.enabled = false;  // Zombi sesini kapatır
            }
        }
        // Raycast işlemini devre dışı bırakır.
        Physics.queriesHitTriggers = false;
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu");
        Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini
        StartCoroutine(skorlar_ekleme());//skorları veritabanına ekler
    }

    public void Exit()
    {
        Application.Quit();  //uygulamadan çıkış yapar
        StartCoroutine(skorlar_ekleme());//skorları veritabanına ekler
    }
}

