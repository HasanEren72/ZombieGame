using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AtesEtme : MonoBehaviour
{
    
    // public GameObject MermiCikisNoktasi;
    public GeriTepme recoil;//
    public bool AtesEdebilir;

    RaycastHit hit;
    float GunTimer;
    public float TaramaHizi;   
    public float Menzil;
    
    public float mermi;
    public float sarjor;
    public float tasinanmermi;
    float eklenenmermi;
    float reloadtimer;

    public TextMeshProUGUI MermiSayac; //

    public LayerMask zombiKatman;
    AudioSource SesKaynak;
    public AudioClip AtesSesi;
    public GameObject mermiefekti;
    public ParticleSystem MuzzleFlash;

    void Start()
    {
        SesKaynak = GetComponent<AudioSource>();
    }

    void Update()
    {
        eklenenmermi = sarjor - mermi;

        MermiSayac.text = "" + mermi + "/" + tasinanmermi; //

        if (eklenenmermi > tasinanmermi)
        {
            eklenenmermi = tasinanmermi;
        }
        if (Input.GetKeyDown(KeyCode.R) && eklenenmermi >0 && tasinanmermi>0)
        {
            if (Time.time >reloadtimer)
            {
                StartCoroutine(Reload());
                reloadtimer = Time.time;
            }

        }


        if (Input.GetKey(KeyCode.Mouse0) && AtesEdebilir == true && Time.time > GunTimer  && mermi>0) //mouse ile ateş etme koşulu
        {
            Fire();
            GunTimer = Time.time + TaramaHizi;
            recoil.Fire();
            Instantiate(mermiefekti , hit.point,  Quaternion.LookRotation(hit.normal)); //mermi izi efekti
            SesKaynak.Play();
            SesKaynak.clip = AtesSesi;
        }
    }


    void Fire()
    {

        if (mermi <=0) 
        {
            AtesEdebilir = false;   
        }
        if (mermi > 0)
        {
            AtesEdebilir = true;
            
            if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit, Menzil, zombiKatman))
            {
                MuzzleFlash.Play();               
                Debug.Log(hit.transform.name);
            }
            mermi--;
        }
        
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds((float)1);
        mermi = mermi + eklenenmermi;
        tasinanmermi = tasinanmermi - eklenenmermi;
    }
}