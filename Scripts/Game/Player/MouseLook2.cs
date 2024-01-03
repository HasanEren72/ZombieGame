using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook2 : MonoBehaviour
{
    [SerializeField]
    private Transform hedef;

    [SerializeField]
    private Vector3 hedefMesafe;

    [Range(0, 100)][SerializeField]
    private float MauseSensitivity;  // Fare Hassasiyeti hassasiyet

    float mouseX, mouseY;

    [SerializeField]
    private Transform karekterGovde;//kamera ile vucut dondurmek için

    Vector3 objRotasyon;

    KarekterKontrol2 nesne;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;// fare imlecini kilitler yani görünmez yapar
        nesne = GameObject.Find("character").GetComponent<KarekterKontrol2>();  //mauselook ve karekterkontrol varklý nesneler içinde old için
    }                                                                           //find ile aratýp compenentlerine ulaþtýk

    private void LateUpdate()  //kamera ile ilgili kodlar genelde LateUpdate e yazýlýr
    {
        if (nesne.hayattaMi ==true)  //oyuncu hayatta ise yapýlacaklar
        {
            transform.position = Vector3.Lerp(transform.position, hedef.position + hedefMesafe, Time.deltaTime * 10); //kamera pozisyonuna oyuncunun pozisyonu atadýk
                                                                                                                      //Bunun yerine kamerayý direk oyuncunun içinede atayabilirdk ayný iþlev
            mouseX += Input.GetAxis("Mouse X") * MauseSensitivity;
            mouseY += Input.GetAxis("Mouse Y") * MauseSensitivity;

            transform.eulerAngles = new Vector3(-mouseY, mouseX, 0);  //giriþlere göre kamerayý rotasyon deðiþtirir 
            hedef.transform.eulerAngles = new Vector3(0, mouseX, 0); //giriþlere göre hedefi rotasyon deðiþtirir

            if (mouseY > 30)
            {
                mouseY = 30;
            }
            if (mouseY < -30)
            {
                mouseY = -30;
            }

            // kamera hareketi ile karekterin vucudunu hareket ettirmek için

            Vector3 gecici = transform.eulerAngles; //kameranýn rotasyonunu deðiþkene atadýk
            gecici.z = 0;
            gecici.y = transform.localEulerAngles.y;      // y ekseninde kamera ile ayný rotasyon olacak
            gecici.x = transform.localEulerAngles.x + 10; // x ekseninde 10 birim dondurecek
            objRotasyon = gecici;

            karekterGovde.transform.eulerAngles = objRotasyon;  // karekterGovde rotasyon transformuna oluþturduðumuz rotasyonu atadýk
        }
    }
}
