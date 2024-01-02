using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class create : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField kullaniciAdi_IF, sifre_IF, sifreTekrar_IF;

    [SerializeField]
    private Toggle sozlesme;

    UI_Control uiControl;

    public static bool kayitoldu;

    void Start()
    {
        kayitoldu = false;
        uiControl = GetComponent<UI_Control>();
    }

    public void uyeligiOlustur_Buton()
    {
        if (kullaniciAdi_IF.text.Equals("") || sifre_IF.text.Equals("") || sifreTekrar_IF.text.Equals(""))
        {
            StartCoroutine(uiControl.hataPanel("Boş BIRAKMAYINIZ!"));
        }
        else
        {
            if (sifre_IF.text.Equals(sifreTekrar_IF.text))
            {
                if (sozlesme.isOn)
                {
                    Debug.Log("Veritabanı Bağlantısı");
                    StartCoroutine(kayitOl());
                }
                else
                {
                    StartCoroutine(uiControl.hataPanel("Lütfen Sözleşmeyi Kabul Ediniz!"));
                }
            }
            else
            {
                StartCoroutine(uiControl.hataPanel("şifreler Eşleşmiyor!"));
            }
        }
    }

    IEnumerator kayitOl()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "kayitOlma"); //php dosyas�nda kayitOlma if blo�una gider

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
                if (www.downloadHandler.text.Contains("kayıt başarılı"))
                {
                    StartCoroutine(uiControl.hataPanel(www.downloadHandler.text));  //kay�t ba�ar�l� olma durumunda sorgu sonucunu hata paneline yazar

                    //PlayerPrefs.DeleteKey("karekter1_kullanbtn"); // oyuna kay�t olduktan sonra karekterlerler ilk ba�ta siliniyor
                    //PlayerPrefs.DeleteKey("karekter2_kullanbtn");
                    //PlayerPrefs.DeleteKey("karekter3_kullanbtn");
                    StartCoroutine(ilkSkorlarKayit());
                    PlayerPrefs.DeleteAll();
                    kayitoldu = true;
                    Debug.Log("karekterler kayıt olunca default olarak silindi!");
                }
                else
                {
                    Debug.Log("lütfen benzersiz kullanıcı adı kullanınız!"); //hata olma durumunda//
                    StartCoroutine(uiControl.hataPanel("Lütfen benzersiz kullanıcı adı kullanınız!"));
                }
            }
        }
    }

    IEnumerator ilkSkorlarKayit()
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
                Debug.Log("ilk skorlar ekleme sonucu :" + www.downloadHandler.text);
            }
        }
    }
}
