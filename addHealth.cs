using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addHealth : MonoBehaviour
{
    public AudioSource chime;
     void OnTriggerEnter2D(Collider2D other)
     {
        if(other.tag =="moose")
        {
            
            if(CharController.mooseHealth < 25)
            {
                chime.Play();
                CharController.mooseHealth += 5;
                Destroy(gameObject, 0.3f);
            }
                       
        }
     }
}
