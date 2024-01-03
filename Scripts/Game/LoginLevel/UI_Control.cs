using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI_Control : MonoBehaviour
{
    [SerializeField]
    private GameObject Giris_panel , kayit_panel;

    [SerializeField]
    private TextMeshProUGUI  hata_TMP;

    AudioSource audioSource;
    [SerializeField]
    private AudioClip loginSes;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        audioSource.clip = loginSes;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void Kayitol_B()
    {
        kayit_panel.SetActive(true);
        Giris_panel.SetActive(false);
    }
    public void Geri_B()
    {
        kayit_panel.SetActive(false);
        Giris_panel.SetActive(true);       
    }

    public IEnumerator hataPanel(string hataText)
    {
        hata_TMP.SetText(hataText);
       
        yield return new WaitForSeconds(1.5f);
       // hataAnimator.SetBool("HataDurumu", false);
    }

    public void SesControl()
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

    public void exit()
    {
        Application.Quit();
    }
}
