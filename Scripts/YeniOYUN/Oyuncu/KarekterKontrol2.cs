using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KarekterKontrol2 : MonoBehaviour
{
    public Animator anim;

    [SerializeField]
    private float Speed = 5f, FastSpeed = 10f;
    private float KarekerCan = 100;

    public Transform groundCheck;  // zemin kontrol� i�in
    public float groundDistance = 0.4f; // yer mesafesi
    public LayerMask groundMask;   // zemin maskesi   zemini ay�rt etmek i�in

    public float ZiplamaUzunlugu = 3f;   //z�plama y�ksekli�i
    bool isGrounded; // toprak m�  kontorl i�in

    public bool hayattaMi=true;
    public GameObject olduKamerasi;
    public GameObject karekterKamerasi;


    public GameObject YerdeKanEfekti;

    void Start()
    {      
        anim = GetComponent<Animator>();
        //controller = GetComponent<CharacterController>();
        hayattaMi = true;       
    }
  
    void Update()
    {
        if (KarekerCan <= 0)  // Karakter �ld� ise ve kan efekti hen�z olu�turulmad�ysa
        {
            KarekerCan = 0;
            hayattaMi = false;
            anim.SetBool("die", true);
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(OlduKameras�());
     
        }

        if (hayattaMi)
        {
            Animsayon_Hareket();
            Hareket();
            Ziplama();
        }
    }

    IEnumerator OlduKameras�()
    {
        yield return new WaitForSeconds(2);
        Instantiate(YerdeKanEfekti, new Vector3(this.gameObject.transform.position.x, (float)(this.gameObject.transform.position.y + 0.4), this.gameObject.transform.position.z), this.gameObject.transform.rotation);
        olduKamerasi.SetActive(true);
        karekterKamerasi.SetActive(false);
        yield return new WaitForSeconds(3); //3 saniye bekler
        Time.timeScale = 1.0f; //zaman� ba�lat�r
        SceneManager.LoadScene("Game_Over"); //game over sahnesini a�ar
    }

    void Animsayon_Hareket()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", yatay);
        anim.SetFloat("Vertical", dikey);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("crouch", true);     //��melme             
        }
        else
        {
            anim.SetBool("crouch", false);
        }

    }
    void Hareket()
    {
        float yatay = Input.GetAxis("Horizontal") * Time.deltaTime;
        float dikey = Input.GetAxis("Vertical") * Time.deltaTime;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(yatay * FastSpeed, 0, dikey * FastSpeed);
            anim.SetBool("fastrun", true);           
        }
        else
        {
            transform.Translate(yatay * Speed, 0, dikey * Speed);
            anim.SetBool("fastrun", false);           
        }

        //Vector3 move = transform.right * yatay + transform.forward * dikey;
        //controller.Move(move);
    }
    void Ziplama()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //groundCheck pozisyonuna dayal� bir k�re yaratacakt�r 
                                                                                            //yer mesafesi ve zemin maskesi de eklenir
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            transform.Translate(Vector3.up * ZiplamaUzunlugu);
            // transform.Translate(0 , 1* ZiplamaUzunlugu , 0); ayn�s�
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }
    }
    public void HasarAL(string dusman)
    {

        if (dusman=="zombi") //d��man zombi ise hasar miktar�
        {
            KarekerCan -= Random.Range(5, 15);
        }
        else if (dusman == "mutant")//d��man mutant ise hasar miktar�
        {
            KarekerCan -= Random.Range(15, 20);
        }
        
    }
    public float GetKarekerCan()  // private de�i�kenleri ba�ka s�n�flardan  �a��r�p kullanabilmek i�in 
    {                             //public fonk olu�turup private de�i�keni return ettik.
        return KarekerCan;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer==LayerMask.NameToLayer("ground"))
    //    {
    //        Yurmusesi.SetActive(true);
    //    }
    //}
}
