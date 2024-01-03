using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField kullaniciAdi_IF, sifre_IF;

    public static string kuladi;
    public static string sifre;

    public static string V_kullanici_degeri;
    public static string V_sifre_degeri;

    UI_Control uiControl;

    private void Awake()
    {
        uiControl = GetComponent<UI_Control>();
    }
    void Start()
    {
        PlayerPrefs.DeleteAll();
    }

    public void kuladi_vesifre_atama_fonk()
    {
        kuladi = kullaniciAdi_IF.text;
        sifre = sifre_IF.text;
    }

    public void girisYap_B()
    {
        if (kullaniciAdi_IF.text.Equals("") || sifre_IF.text.Equals(""))
        {
            StartCoroutine(uiControl.hataPanel("Boş BIRAKMAYINIZ!"));
        }
        else
        {           
            StartCoroutine((uiControl.hataPanel("Giris Basarili")));
            //veritaban�
            StartCoroutine(girisYap());
        }
    }

    IEnumerator girisYap()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity","girisYapma");
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
               Debug.Log("Sorgu Sonucu:" + www.downloadHandler.text);
                
                if (www.downloadHandler.text.Contains("giriş başarılı"))
                {                    
                    kuladi_vesifre_atama_fonk();
                    PlayerPrefs.SetString("kullaniciadi_Kayit", kuladi); // set etme i�lemi
                    PlayerPrefs.SetString("sifre_Kayit", sifre);

                    V_kullanici_degeri = PlayerPrefs.GetString("kullaniciadi_Kayit");
                    V_sifre_degeri = PlayerPrefs.GetString("sifre_Kayit");
                  
                    SceneManager.LoadScene("Menu", LoadSceneMode.Single);  // veriyi g�ndermek i�in sahneyi y�kledik                
                }
                else
                {
                    Debug.Log("Sorgu Sonucu:" + www.downloadHandler.text);
                    StartCoroutine(uiControl.hataPanel(www.downloadHandler.text));                  
                }                  
            }
        }
    }
}
