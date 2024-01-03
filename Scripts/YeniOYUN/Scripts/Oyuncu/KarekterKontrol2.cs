using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KarekterKontrol2 : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private float Speed = 5f, FastSpeed = 10f;
    [SerializeField]
    private int KarekerCan = 100;

    public Transform groundCheck;  // zemin kontrolü için
    public float groundDistance = 0.4f; // yer mesafesi
    public LayerMask groundMask;   // zemin maskesi   zemini ayırt etmek için

    [SerializeField]
    private float ZiplamaUzunlugu = 3f;   //zıplama yüksekliği

    bool isGrounded; // toprak mı  kontorl için

    public bool hayattaMi = true;

    [SerializeField]
    private GameObject olduKamerasi , karekterKamerasi;

    [SerializeField]
    private GameObject YerdeKanEfekti;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private Gradient gradient;//canbar renklendirmek için

    [SerializeField]
    private Image fill;

    void Start()
    {
        anim = GetComponent<Animator>();
        //controller = GetComponent<CharacterController>();
        hayattaMi = true;

        SetStartCanBar(KarekerCan); //can barın ilk değeri atanır.
    }

    void Update()
    {
        if (KarekerCan <= 0)  // Karakter öldü ise ve kan efekti henüz oluşturulmadıysa
        {
            KarekerCan = 0;
            hayattaMi = false;
            anim.SetBool("die", true);
            Cursor.lockState = CursorLockMode.None;
            StartCoroutine(OlduKamerası());
        }

        if (hayattaMi)
        {
            Animsayon_Hareket();
            Hareket();
            Ziplama();
        }

        SetCanBar(KarekerCan);
    }
    public void SetStartCanBar(int can)
    {
        slider.maxValue = can;
        slider.value = can;
        fill.color = gradient.Evaluate(1f); // fill imagesine gradient 1f yani full değerinde olan yeşil rengi atadık
    }
    public void SetCanBar(int can)
    {
        slider.value = can;
        fill.color = gradient.Evaluate(slider.normalizedValue);//fill imagesine gradientın 
    }                          //sliderin normalize değeri atadık boylece slider degeri değişince renkde değişecek
    IEnumerator OlduKamerası()
    {
        yield return new WaitForSeconds(2);
        Instantiate(YerdeKanEfekti, new Vector3(this.gameObject.transform.position.x, (float)(this.gameObject.transform.position.y + 0.4), this.gameObject.transform.position.z), this.gameObject.transform.rotation);
        olduKamerasi.SetActive(true);
        karekterKamerasi.SetActive(false);
        yield return new WaitForSeconds(3); //3 saniye bekler
        Time.timeScale = 1.0f; //zamanı başlatır
        SceneManager.LoadScene("Game_Over"); //game over sahnesini açar
    }

    void Animsayon_Hareket()
    {
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", yatay);
        anim.SetFloat("Vertical", dikey);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("crouch", true);     //çömelme             
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
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //groundCheck pozisyonuna dayalı bir küre yaratacaktır 
                                                                                            //yer mesafesi ve zemin maskesi de eklenir
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            transform.Translate(Vector3.up * ZiplamaUzunlugu);
            // transform.Translate(0 , 1* ZiplamaUzunlugu , 0); aynısı
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }
    }
    public void HasarAL(string dusman)
    {
        if (dusman == "zombi") //düşman zombi ise hasar miktarı
        {
            KarekerCan -= Random.Range(5, 15);
        }
        else if (dusman == "mutant")//düşman mutant ise hasar miktarı
        {
            KarekerCan -= Random.Range(15, 20);
        }
    }
    public float GetKarekerCan()  // private değişkenleri başka sınıflardan  çağırıp kullanabilmek için 
    {                             //public fonk oluşturup private değişkeni return ettik.
        return KarekerCan;
    }
}
