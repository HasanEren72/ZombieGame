using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_over : MonoBehaviour
{   
    public void Home()
    {
        SceneManager.LoadScene("Menu");
        PlayerPrefs.DeleteKey("puan_verisi");
        PlayerPrefs.DeleteKey("altin_sayisi_verisi");
        PlayerPrefs.DeleteKey("elmas_verisi");
        Cursor.lockState = CursorLockMode.None;// fare imlecini kilidini
    }

    public void Restart_button()
    {
        PlayerPrefs.DeleteKey("puan_verisi");
        PlayerPrefs.DeleteKey("altin_sayisi_verisi");
        PlayerPrefs.DeleteKey("elmas_verisi");

        if (MenuManager.bolum1==true)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("Bolum1");
            Cursor.lockState = CursorLockMode.Locked;// fare imlecini kilidini
        }
        else
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("Bolum2");
            Cursor.lockState = CursorLockMode.Locked;// fare imlecini kilidini
        }       
    }

    public void Exit_button()
    {
        PlayerPrefs.DeleteKey("puan_verisi");
        PlayerPrefs.DeleteKey("altin_sayisi_verisi");
        PlayerPrefs.DeleteKey("elmas_verisi");
        Application.Quit();
    }
}