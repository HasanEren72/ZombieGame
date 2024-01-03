using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public  CharacterController controller;

    public float speed = 5f;
    public float FastSpeed = 10;
    public float gravity = -10;  // yerçekimi

    Vector3 velocity; // vector3 te bir tane hýz  deðiþkeni

    public Transform groundCheck;  // zemin kontrolü için
    public float groundDistance = 0.4f; // yer mesafesi
    public LayerMask groundMask;   // zemin maskesi   zemini ayýrt etmek için
    public float jumpheight=3f;   //zýplama yüksekliði
   
    bool isGrounded; // toprak mý  kontorl için

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position , groundDistance , groundMask); //groundCheck pozisyonuna dayalý bir küre yaratacaktýr 
                                                                                           //yer mesafesi ve zemin maskesi de eklenir
        if (isGrounded && velocity.y<0) //toprak ta ve y deki hýzý o dan küçük ise hýzýný sýfýrlarýz
        {
            velocity.y = -2f;  //   0 yerine -2 yaparsak  daha optimize olur
        }

        float x = Input.GetAxis("Horizontal") * Time.deltaTime; //yatay hareket -1 ile 1 arasýnda deðer döndürür.
        float z = Input.GetAxis("Vertical") * Time.deltaTime;  //dikey hareket
                                                                       // oyuncu hangi yöne bakarsa o yöne gidecektir
        Vector3 move = transform.right * x  + transform.forward * z;    // Vector3 move = new Vector3(x , 0f ,z); böyle yapmýþ olsaydýk oyuncu 
      //Vector3 move = new vector3(1,0,0) * x  + new vector3(0,0,1) * z;   //hangi yone bakarsa baksýn hep ayný yone hareket ederdi

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * FastSpeed );  //  karekteri hýzlý hareket ettirir.
        }
        else
        {
            controller.Move(move * speed );  // karekter hareket ettirir.
        }

        velocity.y += gravity * Time.deltaTime * Time.deltaTime;  //Yerçekimi Ýçin  serbest düþmede hýz h=1/2gt^2 old için  vector3 te y ekseninde 
        controller.Move(velocity);                               //hareket old için gravity(yerçekimi) ile zamanýn karesi ile çarpýyoruz.

        if (Input.GetButtonDown("Jump") && isGrounded )
        {           
            velocity.y = Mathf.Sqrt(jumpheight * -2f *gravity);          
        }
    }
}
