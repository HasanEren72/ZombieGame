using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxControl : MonoBehaviour
{
    public GameObject yagmurEfekti;
    public GameObject KarEfekti;
    public GameObject yagmurSesi;

    public Material matGunesli;
    public Material matYagmurlu;
    public Material matKarli;

    public void HavaDurumuDegistir(int deger)
    {
        if (deger == 0) //default güneþli
        {
            yagmurSesi.SetActive(false);
            RenderSettings.skybox = matGunesli;
        }
        else if (deger == 1)  //güneþli
        {
            yagmurEfekti.SetActive(false);
            KarEfekti.SetActive(false);
            yagmurSesi.SetActive(false);            
            RenderSettings.skybox = matGunesli;
        }
        else if (deger == 2) //yaðmurlu
        {
            yagmurEfekti.SetActive(true);
            yagmurSesi.SetActive(true);
            KarEfekti.SetActive(false);
            RenderSettings.skybox = matYagmurlu;
        }
        else if(deger == 3) //karlý
        {
            KarEfekti.SetActive(true);
            yagmurSesi.SetActive(false);
            yagmurEfekti.SetActive(false);
            RenderSettings.skybox = matKarli;
        }

    }
    void Start()
    {
        
    }
 
    void Update()
    {
        
    }
}
