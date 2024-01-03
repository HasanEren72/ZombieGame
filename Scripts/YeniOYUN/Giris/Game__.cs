using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game__ : MonoBehaviour
{
    [SerializeField] private GameObject loginPanel;
    [SerializeField] private GameObject GamePanel;
    public bool girisYapti;

    private void Awake()
    {
        // girisYapti =true;
    }
    void Start()
    {
        Panel();
    }


    void Update()
    {

    }
    void Panel()
    {
        if (girisYapti == true)
        {
            loginPanel.SetActive(false);
            GamePanel.SetActive(true);
        }
    }
}
