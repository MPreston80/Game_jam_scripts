using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fivepackGum : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
   {
       if(other.tag =="moose")
       {
           collectGum.counter += 5;
           Destroy(gameObject);           
       }
   }
}
