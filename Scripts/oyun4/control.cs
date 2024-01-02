using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class control : MonoBehaviour
{
    [Range (0,100)]
    public float Hiz = 5;
    Rigidbody rb;
    private GameObject odak;
    public GameObject gameoverPanali;
    public GameObject gucgostergesi;
    private bool itmegucu = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        odak = GameObject.Find("odak");
    }
 
    void Update()
    {
        float yatayInput = Input.GetAxis("Horizontal");
        float dikeyInput = Input.GetAxis("Vertical");
        //transform.Translate(dondurmeHizi * Time.deltaTime * yatayInput, 0, dondurmeHizi * Time.deltaTime * dikeyInput); 
                  //ikisi de ayn� i�levi yapar hareket sa�lar ama addForce ile g�� uygulad���m�z zaman da hareketin yan�nda rotasyonunada etki eder
        rb.AddForce(odak.transform.forward*Hiz * dikeyInput);//bu yuzden kure nesnesi g�� uygulad���nda hem hareket eder hemde d�ner.

        if (transform.position.y<-5)
        {
            Time.timeScale = 0;
            gameoverPanali.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
        }
  
         gucgostergesi.transform.position = new Vector3(transform.position.x + 1.54f, 0.62f, transform.position.z - 1.44f);        
    }

    private void OnTriggerEnter(Collider other) //triger tetikleme olay�
    {
        if (other.CompareTag("guc"))
        {        
            Destroy(other.gameObject);
            StartCoroutine(GucGoster());
        }
    }
    private void OnCollisionEnter(Collision collision) //�arp��ma olay�
    {
        if (collision.gameObject.CompareTag("dusman") && itmegucu==true)
        {
            Rigidbody dusmanRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 ittirmeYon = (collision.gameObject.transform.position - transform.position).normalized;
            dusmanRb.AddForce(ittirmeYon*20 ,ForceMode.Impulse);
        }
    }
    IEnumerator GucGoster()
    {
        itmegucu = true;
        Debug.Log("itme gucu kazan�ld�");
        gucgostergesi.SetActive(true);   
        yield return new WaitForSeconds(5);
        gucgostergesi.SetActive(false);
        itmegucu = false;
    }
    public void restart()
    {
        gameoverPanali.SetActive(false);     
        SceneManager.LoadScene("Not4");
    }
    public void sag()
    {
        rb.AddForce(odak.transform.right * Hiz);
    }
    public void sol()
    {
        rb.AddForce(odak.transform.right * -Hiz);
    }
    public void yukari()
    {
        rb.AddForce(odak.transform.forward * Hiz);
    }
    public void asagi()
    {
        rb.AddForce(odak.transform.forward * -Hiz);
    }
}
