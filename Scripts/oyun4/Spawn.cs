using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject dusman;
    public GameObject GucNesnesi;
    private float dusmansayisi=0;
    public int zorluk=1;
    
    void Start()
    {
        dusmanUret(zorluk);
        InvokeRepeating("GucNesneleriSil", 2,6);
    }
    void Update()
    {
        dusmansayisi = FindObjectsOfType<Dusman>().Length; //dusman scriptinin sayýsý yani dusman sayisi
        if (dusmansayisi==0)
        {
            dusmanUret(zorluk++);
            GucNesnesiUret();
        }
    }
    public void dusmanUret(int dusmansayisi)
    {
        for (int i = 0; i < dusmansayisi; i++)
        {
            Instantiate(dusman, RastgeleKonum(), dusman.transform.rotation);
        }
    }
    public void GucNesnesiUret()
    {
        Instantiate(GucNesnesi, RastgeleKonum(), new Quaternion(dusman.transform.rotation.x + 180, dusman.transform.rotation.y, dusman.transform.rotation.z, 0));
    }                                          //burada güç nesnesi yamuk olduðu için 180 x ekseninde döndürülmüþ þekilde rotasyon ayarladýk

    public void GucNesneleriSil()
    {
        GameObject[] nesneler = GameObject.FindGameObjectsWithTag("guc");
        for (int i = 0; i < nesneler.Length; i++)
        {
            Destroy(nesneler[i]);
        }
    }
    
    private Vector3 RastgeleKonum()
    {
        float xpos = Random.Range(-20.19f, -10.15f);
        float zpos = Random.Range(-19.4f, -10.15f);
        return new Vector3(xpos, 0, zpos);       
    }   
}
