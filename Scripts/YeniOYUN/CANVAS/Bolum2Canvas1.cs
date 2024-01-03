using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI (user interface) kullanýcu arayüzü kütüphanesi
using TMPro; //Text mesh pro kütüphanesi  
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Bolum2Canvas1 : MonoBehaviour
{
    public TextMeshProUGUI CanText;
    public TextMeshProUGUI MermiText;

    public GameObject OyunDurduPaneli;
    public GameObject ZombileriOldurBtn;
    GameObject oyuncu;

    public int olusayisi = 0;
    public int nesnesayisi = 0;
    public TextMeshProUGUI olu_text;

    public GameObject[] ClonZombiler1;  //her bir dizide farklý özellikte zombiler olacak;
    public GameObject[] ClonZombiler2;
    public GameObject[] ClonZombiler3;
    public GameObject BolumCanavari;

    public TextMeshProUGUI dalgaText;
    public GameObject dalgatextobjesi;

    public GameObject karekterKamerasi;
    public GameObject DurduKamerasi;

    public int altin = 0; //veriler için bu deðiþkenler kullanýlýr
    public int elmas = 0;
    public int puan = 0;

    public int Altin_Kayit; //playeprefabs()  sistemi için verileri oyunda tutmak için herhangibir sýnýftan direk eriþebiliriz ama verinin en son haline
    public int Elmas_Kayit;//veritabanýna verileri kaydetmek istemez isek playeprefabs() a bu altýn elmas deðiþkenleri kaydedip sonra
    public int Puan_Kayit; //bu deðiþkenlere atayabiliriz.

    public static string kullaniciadi_str, sifre_str, puan_str, toplamAltin_str, elmas_str; // verileri veritabanýna kaydetmek için deðiþkenler
    // altýn ,elmas gibi deðiþkenleri direk atayabilirz bu deðiþkenlere yada playeprefabs() ile oyunda kaydedilmiþ verileri bu degiskenlere 
    //atayabiliriz.
    public float sayi;

    bool pausePanelinde = false;
    public GameObject complatedSes;
    void Start()
    {
        complatedSes.SetActive(false);
        oyuncu = GameObject.Find("character"); //character nesnesini aratýp private nesneye atadýk  karekterkontrol2 ve atesetme2 sýnýflarýna ulaþmak için

        dalgaText.text = "1.Dalga Baþlýyor .....";
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

            int zombÝndis = Random.Range(0, ClonZombiler1.Length);
            GameObject nesne = Instantiate(ClonZombiler1[zombÝndis], new Vector3(xpozisyon, -2f, zpozisyon), randomRotation);
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

    public void Dalga2()    //2.Dalga farklý özelliklere sahip rasgele 20 zombi
    {
        dalgaText.text = "2.Dalga Baþlýyor ... Dikatli olun evrim geçirmiþler artýk daha güçlüler !!!";
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

            int zombÝndis = Random.Range(0, ClonZombiler2.Length);
            GameObject nesne = Instantiate(ClonZombiler2[zombÝndis], new Vector3(xpozisyon, -2f, zpozisyon), randomRotation);
            nesnesayisi++;
        }
    }
    public void Dalga3()    //3.Dalga farklý özelliklere sahip rasgele 30 zombi
    {
        dalgaText.text = "3.Dalga Baþlýyor ...  Dikatli olun evrim geçirmiþler artýk daha güçlüler !!!";
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

            int zombÝndis = Random.Range(0, ClonZombiler3.Length);
            GameObject nesne = Instantiate(ClonZombiler3[zombÝndis], new Vector3(xpozisyon, -2f, zpozisyon), randomRotation);
            nesnesayisi++;
        }
    }
    public void BolumSonuCanavari()
    {
        dalgaText.text = "Dikkat Bölüm Sonu Canavarý Geliyor , Çok Tehlikeli Ve  Güçlü Savunmaya Gücüne Sahip !!!";
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
    void AzaltNesneSayisi()  //zombi ölü sayýlarýný tutabilmek için.
    {
        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul

        int aktifZombiSayisi = zombiNesneleri.Length;


        if (aktifZombiSayisi < nesnesayisi && pausePanelinde == false) //  akif zombi azaldýðý zaman  ve pause paneli kapalý ise
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
        else if (aktifZombiSayisi == 0 && olusayisi == 70) //bolum sonu canavarý çaðrý
        {
            BolumSonuCanavari();
            elmas += 50;
            PlayerPrefs.SetInt("Elmas_verisi", elmas);
            Elmas_Kayit = PlayerPrefs.GetInt("Elmas_verisi");
        }
        else if (aktifZombiSayisi == 0 && olusayisi == 72)//kazanmak
        {
            dalgaText.text = "*** Tebrikler Kazandýnýz *** Sonraki bölüme geçmek için AnaMenuye dönün";
            complatedSes.SetActive(true);
            karekterKamerasi.SetActive(false);
            DurduKamerasi.SetActive(true);           
            Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini kaldýrýr.
            dalgatextobjesi.SetActive(true);
            StartCoroutine(dalgaTextKapat());
            StartCoroutine(skorlar_ekleme());//skorlarý veritabanýna ekler
            Time.timeScale = 0;
        }

    }
    public void Zombilerioldur()  //ZombileriOldurBtn butona basýlýrsa çaðýrýlacak
    {                             //tagý zombi olan tum nesneleri bir diziye attýk sonra for ile tek tek sildik öldürdük
        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul
        int aktifZombiSayisi = zombiNesneleri.Length;

        for (int i = 0; i < zombiNesneleri.Length; i++)
        {
            Destroy(zombiNesneleri[i]);
            olusayisi += 1;
        }
        olusayisi -= 1; // AzaltNesneSayisi fonksiyonunda 1 tane fazla artýrdýðý için 1 azaltýk
        ZombileriOldurBtn.SetActive(false);
    }

    void Update()    // MermiText textine oyuncu nesnesin kompenentlerine ulaþýp  AtesEtme2 sýnýfýndan  GetSarjor() ve GetCephane() 
    {                //fonk çaðýrýp stinge döbüþtürerek atadýk

        MermiText.text = oyuncu.GetComponent<AtesEtme2>().GetSarjor().ToString() + "/" + oyuncu.GetComponent<AtesEtme2>().GetCephane().ToString();
        CanText.text = "Can :" + oyuncu.GetComponent<KarekterKontrol2>().GetKarekerCan().ToString();

        if (Input.GetKey(KeyCode.Escape)) // esc ye basýnca oyunu durdurur.
        {
            OyunuDurdur();
        }
        if (Input.GetKey(KeyCode.K)) //k ye basýldýðý zaman pause panelinde ZombileriOldurBtn aktif eder.
        {
            ZombileriOldurBtn.SetActive(true);
        }
        AzaltNesneSayisi();


        puan_str = Puan_Kayit.ToString();  //Burada sürekli güncellenen skorlar  bu deðiþkene atanýr 
        toplamAltin_str = Altin_Kayit.ToString();
        elmas_str = Elmas_Kayit.ToString();
    }
    // skor ekleme iþlemleri

    public IEnumerator skorlar_ekleme() // veritabanýna güncellenmiþ þekilde skorlarý günceller
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
                Debug.Log("Skorlar kayýt Sonucu baþarýlý:" + www.downloadHandler.text);
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

        for (int i = 0; i < zombiNesneleri.Length; i++)   // oyun devam ederse " tüm zombilerin SESLERÝNÝ AÇMAK ÝÇÝN" 
        {
            AudioSource zombiSesi = zombiNesneleri[i].GetComponent<AudioSource>();
            if (zombiSesi != null)  // Zombi nesnesinde ses bileþeni varsa
            {
                zombiSesi.enabled = true;  // Zombi sesini etkinleþtir
            }
        }
        // Raycast iþlemini aktif hale getirir.
        Physics.queriesHitTriggers = true;

        pausePanelinde = false;
    }


    public void OyunuDurdur()
    {
        pausePanelinde = true;
        Time.timeScale = 0; //oyunu durdurur.
        OyunDurduPaneli.SetActive(true);  //devam et butonunu aktif yapar
        Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini kaldýrýr.
        DurduKamerasi.SetActive(true);
        karekterKamerasi.SetActive(false);

        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul diziye at

        for (int i = 0; i < zombiNesneleri.Length; i++) //oyun durmuþsa " tüm zombilerin SESLERÝNÝ KESMEK ÝÇÝN" 
        {
            AudioSource zombiSesi = zombiNesneleri[i].GetComponent<AudioSource>();
            if (zombiSesi != null)  // Zombi nesnesinde ses bileþeni varsa
            {
                zombiSesi.enabled = false;  // Zombi sesini kapatýr
            }
        }
        // Raycast iþlemini devre dýþý býrakýr.
        Physics.queriesHitTriggers = false;
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu");
        Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini
        StartCoroutine(skorlar_ekleme());//skorlarý veritabanýna ekler
    }

    public void Exit()
    {
        Application.Quit();  //uygulamadan çýkýþ yapar
        StartCoroutine(skorlar_ekleme());//skorlarý veritabanýna ekler
    }

}

