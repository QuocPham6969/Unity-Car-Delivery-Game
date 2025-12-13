using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverySys : MonoBehaviour
{
   [SerializeField] float destroyDelay = 0.1f;
   bool HasPackage;
    
    
    void OnCollisionEnter2D(Collision2D other) 
    {
         Debug.Log("Ouchhh!");
    }  

    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.tag == "Package" && !HasPackage)
       {
            Debug.Log("Package picked up");
            HasPackage = true; 
            Destroy(other.gameObject, destroyDelay);      
       }

       if (other.tag == "Customer" && HasPackage)
       {
          Debug.Log("Package delivered");
          HasPackage = false;
       }
    }
}
     


