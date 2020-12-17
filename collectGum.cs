using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectGum : MonoBehaviour
{
    public static int counter = 0;

    void OnTriggerEnter2D(Collider2D other)
   {
       if(other.tag =="moose")
       {
           counter ++;
           Destroy(gameObject);           
       }
   }


}
