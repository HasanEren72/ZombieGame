using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class create : MonoBehaviour
{

    public TMP_InputField kullaniciAdi_IF, sifre_IF, sifreTekrar_IF;
    public Toggle sozlesme;

    panel_gecis pK_Script;

    public static  bool kayitoldu;

    void Start()
    {
         kayitoldu = false;
         pK_Script = GetComponent<panel_gecis>();
    }

    
    void Update()
    {

    }


    public void uyeligiOlustur_Buton()
    {
        if (kullaniciAdi_IF.text.Equals("") || sifre_IF.text.Equals("") || sifreTekrar_IF.text.Equals(""))
        {
            StartCoroutine(pK_Script.hataPanel("Boþ BIRAKMAYINIZ!"));

        }
        else
        {
             
            if (sifre_IF.text.Equals(sifreTekrar_IF.text))
            {
                if (sozlesme.isOn)
                {
                   

                    Debug.Log("Veritabaný Baðlantýsý");
                    StartCoroutine(kayitOl());

                }
                else
                {
                    StartCoroutine(pK_Script.hataPanel("Lütfen Sözleþmeyi Kabul Ediniz!"));
                }
            }
            else
            {
                StartCoroutine(pK_Script.hataPanel("Þifreler Eþleþmiyor!"));
            }
        }
    }



    IEnumerator kayitOl()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity","kayitOlma"); //php dosyasýnda kayitOlma if bloðuna gider

        form.AddField("kullaniciAdi", kullaniciAdi_IF.text); 
        form.AddField("sifre", sifre_IF.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);

            }
            else
            {
                
                if (www.downloadHandler.text.Contains("kayýt baþarýlý"))
                {
                    StartCoroutine(pK_Script.hataPanel(www.downloadHandler.text));  //kayýt baþarýlý olma durumunda sorgu sonucunu hata paneline yazar

                    //PlayerPrefs.DeleteKey("karekter1_kullanbtn"); // oyuna kayýt olduktan sonra karekterlerler ilk baþta siliniyor
                    //PlayerPrefs.DeleteKey("karekter2_kullanbtn");
                    //PlayerPrefs.DeleteKey("karekter3_kullanbtn");
                    StartCoroutine(ÝlkSkorlarKayit());
                    PlayerPrefs.DeleteAll();
                    kayitoldu = true;
                    Debug.Log("karekterler kayýt olunca default olarak silindi!");
                }
                else
                {

                    Debug.Log("lütfen benzersiz kullanýcý adý kullanýnýz!" ); //hata olma durumunda//
                    StartCoroutine(pK_Script.hataPanel("Lütfen benzersiz kullanýcý adý kullanýnýz!"));
                }
            
            }
        }
    }

    IEnumerator ÝlkSkorlarKayit()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "ilk_skorlar_ekleme");
        form.AddField("kullaniciAdi", kullaniciAdi_IF.text);
        form.AddField("sifre", sifre_IF.text);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("llk skorlar ekleme sonucu :" + www.downloadHandler.text);
            }
        }
    }
}
