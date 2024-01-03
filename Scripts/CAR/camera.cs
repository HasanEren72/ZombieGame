using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject arac;
    private Vector3 mesafe = new Vector3(0,6,-10);
    
    void Start()
    {
        
    }

    
    void LateUpdate() //LateUpdate geç güncelleme yapar  aracýn kamerasýný takýlarak gitmesini engeller
    {
        // Vector3.up (0,1,0) yukarý Y
        // Vector3.right (1,0,0) sað sol X
        // Vector3.forward  (0,0,1) ileri Z
        // Vector3.Down   (0,-1,0) aþaðý

        transform.position = arac.transform.position + mesafe;
        transform.rotation = arac.transform.rotation;
      
       
    }
}
