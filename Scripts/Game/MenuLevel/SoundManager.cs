using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip clickSesi, SatinAlmaSesi;

    [SerializeField]
    private Button[] butonlar;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        butonlar = Resources.FindObjectsOfTypeAll<Button>();//sahenedeki aktif ve pasif t�m butonlar� atar
      //butonlar = Object.FindObjectsOfType<Button>() bu kod sadece aktif olan butonlar� atar
    }

    private void Start()
    {
        foreach (var buton in butonlar) //t�m butonlara  clickSesiCal() fonk. ekledik
        {
            buton.onClick.AddListener(() => clickSesiCal()); 
        }
    }

    public void clickSesiCal()
    {
        if (clickSesi !=null)
        {
            audioSource.PlayOneShot(clickSesi);
        }       
    }

    public void SatinAlmaSesiCal()
    {
        audioSource.PlayOneShot(SatinAlmaSesi);
    }

    public void SesAcKapa()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        else
        {
            audioSource.Play();
        }
    }
}
