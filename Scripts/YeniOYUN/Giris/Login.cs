using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    panel_gecis pK_Script;

    public AudioSource ses;
    public TMP_InputField kullaniciAdi_IF, sifre_IF;

    public static string kuladi;
    public static string sifre;

    public static string V_kullanici_degeri;
    public static string V_sifre_degeri;

    public void  kuladi_vesifre_atama_fonk()
    {
        kuladi = kullaniciAdi_IF.text;
        sifre = sifre_IF.text;
    }

    public void menuyagit()
    {
        SceneManager.LoadScene("Menu");
    }
  
    void Start()
    {
        pK_Script = GetComponent<panel_gecis>();

       // sahneGecis = GameObject.Find("SahneManager").GetComponent<panel_gecis>();
    }
 
    public void girisYap_B()
    {
        if (kullaniciAdi_IF.text.Equals("") || sifre_IF.text.Equals(""))
        {
            StartCoroutine(pK_Script.hataPanel("Boþ BIRAKMAYINIZ!"));
        }
        else
        {           
            StartCoroutine((pK_Script.hataPanel("Giris Basarili")));
            //veritabaný
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
                
                if (www.downloadHandler.text.Contains("giriþ baþarýlý"))
                {                    
                    kuladi_vesifre_atama_fonk();
                    PlayerPrefs.SetString("kullaniciadi_Kayit", kuladi); // set etme iþlemi
                    PlayerPrefs.SetString("sifre_Kayit", sifre);

                    V_kullanici_degeri = PlayerPrefs.GetString("kullaniciadi_Kayit");
                    V_sifre_degeri = PlayerPrefs.GetString("sifre_Kayit");
                  
                    SceneManager.LoadScene("Menu", LoadSceneMode.Single);  // veriyi göndermek için sahneyi yükledik                
                }
                else
                {
                    Debug.Log("Sorgu Sonucu:" + www.downloadHandler.text);
                    StartCoroutine(pK_Script.hataPanel(www.downloadHandler.text));                  
                }                  
            }
        }
    }
    
    public void seskapat()
    {  if(ses.enabled == false)
        {
            ses.enabled = true;
        }
        else if(ses.enabled == true)
        {
            ses.enabled = false;
        }
    }

    public void exit()
    {
        Application.Quit();
    }
}
