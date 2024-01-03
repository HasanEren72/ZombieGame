using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI (user interface) kullan�cu aray�z� k�t�phanesi
using TMPro; //Text mesh pro k�t�phanesi  
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Oyun1Canvas : MonoBehaviour
{
    public TextMeshProUGUI CanText;
    public TextMeshProUGUI MermiText;
   
    public GameObject OyunDurduPaneli;
    public GameObject ZombileriOldurBtn;
    GameObject oyuncu;

    public int olusayisi = 0;
    public int nesnesayisi = 0;
    public TextMeshProUGUI olu_text;

    public GameObject[] ClonZombiler1;  //her bir dizide farkl� �zellikte zombiler olacak;
    public GameObject[] ClonZombiler2;
    public GameObject[] ClonZombiler3;
    public GameObject   BolumCanavari;

    public TextMeshProUGUI dalgaText;
    public GameObject dalgatextobjesi;

    public GameObject karekterKamerasi;
    public GameObject DurduKamerasi;

    [SerializeField]
    private int altin = 0; //veriler i�in bu de�i�kenler kullan�l�r
    [SerializeField]
    private int elmas = 0;
    [SerializeField]
    private int puan = 0;

    public int Altin_Kayit; //playeprefabs()  sistemi i�in verileri oyunda tutmak i�in herhangibir s�n�ftan direk eri�ebiliriz ama verinin en son haline
    public int Elmas_Kayit;//veritaban�na verileri kaydetmek istemez isek playeprefabs() a bu alt�n elmas de�i�kenleri kaydedip sonra
    public int Puan_Kayit; //bu de�i�kenlere atayabiliriz.

    public static string kullaniciadi_str, sifre_str, puan_str, toplamAltin_str, elmas_str; // verileri veritaban�na kaydetmek i�in de�i�kenler
    // alt�n ,elmas gibi de�i�kenleri direk atayabilirz bu de�i�kenlere yada playeprefabs() ile oyunda kaydedilmi� verileri bu degiskenlere 
    //atayabiliriz.
   
    public float sayi;
    bool pausePanelinde=false;
    public GameObject complatedSes;

    void Start()
    {
        complatedSes.SetActive(false);

        oyuncu = GameObject.Find("character"); //character nesnesini arat�p private nesneye atad�k  karekterkontrol2 ve atesetme2 s�n�flar�na ula�mak i�in

        dalgaText.text = "1.Dalga Ba�l�yor .....";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        for (int i = 0; i < 10; i++)  //1.Dalga rasgele 10 zombi
        {   
            float xpozisyon=0f;
            float zpozisyon=0f;
            int rastgeleBolge = Random.Range(1,4);
            if (rastgeleBolge==1) //1,b�lge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(9, 13);
            }
            else if (rastgeleBolge == 2) //2.b�lge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(-70, 6);
            }
            else if (rastgeleBolge == 3) //3.b�lge
            {
                 xpozisyon = Random.Range(-111, -90);
                 zpozisyon = Random.Range(-20, -16);

            }   

            float RastgeleRotasyonY = Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

            int zomb�ndis = Random.Range(0, ClonZombiler1.Length);
            GameObject nesne = Instantiate(ClonZombiler1[zomb�ndis], new Vector3(xpozisyon, 0, zpozisyon), randomRotation);
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

    public void Dalga2()    //2.Dalga farkl� �zelliklere sahip rasgele 20 zombi
    {
        dalgaText.text = "2.Dalga Ba�l�yor ... Dikatli olun evrim ge�irmi�ler art�k daha g��l�ler !!!";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        for (int i = 0; i < 20; i++)  //1.Dalga rasgele 10 zombi
        {
            float xpozisyon = 0f;
            float zpozisyon = 0f;
            int rastgeleBolge = Random.Range(1, 4);
            if (rastgeleBolge == 1) //1,b�lge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(9, 13);
            }
            else if (rastgeleBolge == 2) //2.b�lge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(-70, 6);
            }
            else if (rastgeleBolge == 3) //3.b�lge
            {
                xpozisyon = Random.Range(-111, -90);
                zpozisyon = Random.Range(-20, -16);

            }

            float RastgeleRotasyonY = Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

            int zomb�ndis = Random.Range(0, ClonZombiler2.Length);
            GameObject nesne = Instantiate(ClonZombiler2[zomb�ndis], new Vector3(xpozisyon, 0, zpozisyon), randomRotation);
            nesnesayisi++;
        }
    }
    public void Dalga3()    //3.Dalga farkl� �zelliklere sahip rasgele 30 zombi
    {
        dalgaText.text = "3.Dalga Ba�l�yor ...  Dikatli olun evrim ge�irmi�ler art�k daha g��l�ler !!!";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        for (int i = 0; i < 40; i++)  //1.Dalga rasgele 10 zombi
        {
            float xpozisyon = 0f;
            float zpozisyon = 0f;
            int rastgeleBolge = Random.Range(1, 4);
            if (rastgeleBolge == 1) //1,b�lge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(9, 13);
            }
            else if (rastgeleBolge == 2) //2.b�lge
            {
                xpozisyon = Random.Range(-111, -106);
                zpozisyon = Random.Range(-70, 6);
            }
            else if (rastgeleBolge == 3) //3.b�lge
            {
                xpozisyon = Random.Range(-111, -90);
                zpozisyon = Random.Range(-20, -16);

            }

            float RastgeleRotasyonY = Random.Range(0, 360);
            Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

            int zomb�ndis = Random.Range(0, ClonZombiler3.Length);
            GameObject nesne = Instantiate(ClonZombiler3[zomb�ndis], new Vector3(xpozisyon, 0, zpozisyon), randomRotation);
            nesnesayisi++;
        }
    }
    public void BolumSonuCanavari()
    {
        dalgaText.text = "Dikkat B�l�m Sonu Canavar� Geliyor , �ok Tehlikeli Ve  G��l� Savunmaya G�c�ne Sahip !!!";
        dalgatextobjesi.SetActive(true);
        StartCoroutine(dalgaTextKapat());

        float xpozisyon = 0f;
        float zpozisyon = 0f;
        int rastgeleBolge = Random.Range(1, 4);
        if (rastgeleBolge == 1) //1,b�lge
        {
            xpozisyon = Random.Range(-111, -106);
            zpozisyon = Random.Range(9, 13);
        }
        else if (rastgeleBolge == 2) //2.b�lge
        {
            xpozisyon = Random.Range(-111, -106);
            zpozisyon = Random.Range(-70, 6);
        }
        else if (rastgeleBolge == 3) //3.b�lge
        {
            xpozisyon = Random.Range(-111, -90);
            zpozisyon = Random.Range(-20, -16);

        }

        float RastgeleRotasyonY = Random.Range(0, 360);
        Quaternion randomRotation = Quaternion.Euler(0f, RastgeleRotasyonY, 0f);

       
        GameObject nesne = Instantiate(BolumCanavari, new Vector3(xpozisyon, 0, zpozisyon), randomRotation);
        nesnesayisi++;

    }
    void AzaltNesneSayisi()  //zombi �l� say�lar�n� tutabilmek i�in.
    {
        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul

        int aktifZombiSayisi = zombiNesneleri.Length;

       
        if (aktifZombiSayisi < nesnesayisi && pausePanelinde == false) //  akif zombi azald��� zaman  ve pause paneli kapal� ise
        {
            nesnesayisi = aktifZombiSayisi;
            olusayisi += 1;
            olu_text.text = "�l� Sayisi:" + olusayisi.ToString();

            altin += 100;           
            PlayerPrefs.SetInt("Altin_verisi", altin);
            Altin_Kayit = PlayerPrefs.GetInt("Altin_verisi");
            
            puan += 200;
            PlayerPrefs.SetInt("Puan_verisi", puan);
            Puan_Kayit = PlayerPrefs.GetInt("Puan_verisi");
        }
        if (aktifZombiSayisi==0 && olusayisi ==10) //2.dalga
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
        else if (aktifZombiSayisi == 0 && olusayisi == 70) //bolum sonu canavar� �a�r�
        {
            BolumSonuCanavari();
            elmas += 50;
            PlayerPrefs.SetInt("Elmas_verisi", elmas);
            Elmas_Kayit = PlayerPrefs.GetInt("Elmas_verisi");
        }
        else if (aktifZombiSayisi == 0 && olusayisi == 72)//kazanmak
        {    
            dalgaText.text = "*** Tebrikler Kazand�n�z *** Sonraki b�l�me ge�mek i�in AnaMenuye d�n�n";
            Time.timeScale = 0;
            complatedSes.SetActive(true);
            karekterKamerasi.SetActive(false);
            DurduKamerasi.SetActive(true);          
            Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini kald�r�r.
            StartCoroutine(YeniBomumKilitAc());
            dalgatextobjesi.SetActive(true);
            StartCoroutine(dalgaTextKapat());
            StartCoroutine(skorlar_ekleme());//skorlar� veritaban�na ekler
        } 
        
    }           
    public  void Zombilerioldur()  //ZombileriOldurBtn butona bas�l�rsa �a��r�lacak
    {                             //tag� zombi olan tum nesneleri bir diziye att�k sonra for ile tek tek sildik �ld�rd�k
        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul
        int aktifZombiSayisi = zombiNesneleri.Length;

        for (int i = 0; i < zombiNesneleri.Length; i++)
        {
           Destroy(zombiNesneleri[i]);
           olusayisi += 1;
        }
        olusayisi -= 1; // AzaltNesneSayisi fonksiyonunda 1 tane fazla art�rd��� i�in 1 azalt�k
        ZombileriOldurBtn.SetActive(false);
    }

    void Update()    // MermiText textine oyuncu nesnesin kompenentlerine ula��p  AtesEtme2 s�n�f�ndan  GetSarjor() ve GetCephane() 
    {                //fonk �a��r�p stinge d�b��t�rerek atad�k

        MermiText.text = oyuncu.GetComponent<AtesEtme2>().GetSarjor().ToString() + "/" + oyuncu.GetComponent<AtesEtme2>().GetCephane().ToString();
        CanText.text = "Can :" + oyuncu.GetComponent<KarekterKontrol2>().GetKarekerCan().ToString();

        if (Input.GetKey(KeyCode.Escape)) // esc ye bas�nca oyunu durdurur.
        {
            OyunuDurdur();
        }
        if (Input.GetKey(KeyCode.K)) //k ye bas�ld��� zaman pause panelinde ZombileriOldurBtn aktif eder.
        {
            ZombileriOldurBtn.SetActive(true);
        }
        AzaltNesneSayisi();


        puan_str = Puan_Kayit.ToString();  //Burada s�rekli g�ncellenen skorlar  bu de�i�kene atan�r 
        toplamAltin_str = Altin_Kayit.ToString();
        elmas_str = Elmas_Kayit.ToString();
        
    }
    // skor ekleme i�lemleri

    public IEnumerator skorlar_ekleme() // veritaban�na g�ncellenmi� �ekilde skorlar� g�nceller
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
                
            }
        }
    }

    public IEnumerator YeniBomumKilitAc()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "YeniBolumKilitAcma");
        form.AddField("kullaniciAdi", kullaniciadi_str);
        form.AddField("sifre", sifre_str);
 
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                
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

        //zombi seslerini a�mak i�in
        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul diziye at

        for (int i = 0; i < zombiNesneleri.Length; i++)   // oyun devam ederse " t�m zombilerin SESLER�N� A�MAK ���N" 
        {
            AudioSource zombiSesi = zombiNesneleri[i].GetComponent<AudioSource>();
            if (zombiSesi != null)  // Zombi nesnesinde ses bile�eni varsa
            {
                zombiSesi.enabled = true;  // Zombi sesini etkinle�tir
            }
        }

        //mutant seslerini a�mak i�in
        GameObject[] MutantNesneleri = GameObject.FindGameObjectsWithTag("Mutant"); // "Mutant" etiketine sahip nesneleri bul diziye at

        for (int i = 0; i < MutantNesneleri.Length; i++) //oyun durmu�sa " t�m Mutant SESLER�N� KESMEK ���N" 
        {
            AudioSource mutantSesi = MutantNesneleri[i].GetComponent<AudioSource>();
            if (mutantSesi != null)  // Mutant nesnesinde ses bile�eni varsa
            {
                mutantSesi.enabled = true;  // mutant sesini kapat�r
            }
        }
        
        Physics.queriesHitTriggers = true; // Raycast i�lemini aktif hale getirir.

        pausePanelinde = false;
    }


    public void OyunuDurdur()
    {
        pausePanelinde = true;
        Time.timeScale = 0; //oyunu durdurur.
        OyunDurduPaneli.SetActive(true);  //devam et butonunu aktif yapar
        Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini kald�r�r.
        DurduKamerasi.SetActive(true);
        karekterKamerasi.SetActive(false);

        //zombie seslerini kapatmak i�in
        GameObject[] zombiNesneleri = GameObject.FindGameObjectsWithTag("Zombi"); // "Zombi" etiketine sahip nesneleri bul diziye at

        for (int i = 0; i < zombiNesneleri.Length; i++) //oyun durmu�sa " t�m zombilerin SESLER�N� KESMEK ���N" 
        {
            AudioSource zombiSesi = zombiNesneleri[i].GetComponent<AudioSource>();
            if (zombiSesi != null)  // Zombi nesnesinde ses bile�eni varsa
            {
                zombiSesi.enabled = false;  // Zombi sesini kapat�r
            }
        }

         //mutant seslerini kapatmak i�in
        GameObject[] MutantNesneleri = GameObject.FindGameObjectsWithTag("Mutant"); // "Mutant" etiketine sahip nesneleri bul diziye at

        for (int i = 0; i < MutantNesneleri.Length; i++) //oyun durmu�sa " t�m Mutant SESLER�N� KESMEK ���N" 
        {
            AudioSource mutantSesi = MutantNesneleri[i].GetComponent<AudioSource>();
            if (mutantSesi != null)  // Mutant nesnesinde ses bile�eni varsa
            {
                mutantSesi.enabled = false;  // mutant sesini kapat�r
            }
        }
      
        Physics.queriesHitTriggers = false; // Raycast i�lemini devre d��� b�rak�r.
    }

    public void Home()
    {
        SceneManager.LoadScene("Menu");
        Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini
        StartCoroutine(skorlar_ekleme());//skorlar� veritaban�na ekler
    }
      
    public void Exit()
    {
        Application.Quit();  //uygulamadan ��k�� yapar
        StartCoroutine(skorlar_ekleme());//skorlar� veritaban�na ekler
    }
    
}
