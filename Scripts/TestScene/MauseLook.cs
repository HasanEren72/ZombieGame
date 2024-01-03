using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MauseLook : MonoBehaviour
{
    [Range(50, 500)]
    public float sensitivity;  //hassasiyet

    public Transform body;

    float xRotation=0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;// fare imlecini kilitler yani görünmez yapar
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sensitivity * Time.deltaTime;  //GetAxisRaw fare x eksenindeki hareketini 0 ile 1 deðerine çevirir
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;      //  y eksenindeki hareketi çýkarýyoruz

        xRotation = Mathf.Clamp(xRotation, -80f, 80f);  // rotasyonu -80 ile 80 arasýnda sýnýrladýk 360 derce dönmesin diye 

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        body.Rotate(Vector3.up * mouseX);  // nesneyi döndürmek için
    }
}
