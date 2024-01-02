using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    //public GameObject[] nesneListesi;
    public List<GameObject> nesneListesi;

    public TextMeshProUGUI score;
    public GameObject GameOverPaneli;

    int skor = 0;
    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(Spawnnesneler());
    }

    void Update()
    {
        if (skor < 0)
        {
            GameOverPaneli.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void puanGuncelle(int puan)
    {
        skor += puan;
        score.text = "Score :" + skor;
    }
    IEnumerator Spawnnesneler()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            int randomİndis = Random.Range(0, nesneListesi.Count);
            Instantiate(nesneListesi[randomİndis]);
        }
    }

    public void restart()
    {
        SceneManager.LoadScene("Not5");
        Debug.Log("calisti1");
    }

    public void exit()
    {
        Application.Quit();
    }
}
