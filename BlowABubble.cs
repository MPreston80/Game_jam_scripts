using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowABubble : MonoBehaviour
{
    float speed = 2.0f;
    public AudioSource inflating;
    public AudioSource popping;    
    

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
             if (collectGum.counter>0) 
             {                
                 gameObject.transform.localScale += gameObject.transform.localScale * (Time.deltaTime * speed); 

                 if(Input.GetKeyDown(KeyCode.Space))
                 {
                     inflating.Play(); 
                 }
                                                
             }
        }

        if(collectGum.counter>0 && Input.GetKeyUp(KeyCode.Space))
        {
            inflating.Stop();
            popping.Play();
            transform.localScale = new Vector3(0.07f, 0.07f, 1);
            collectGum.counter--;            
            squirrelPatrol.squirrelIsDead = true; 
            squirrel1.squirrel1IsDead = true;
            squirrel2.squirrel2IsDead = true;
            wolf.isDead = true;
            wolf2.wolf2IsDead = true;  
            bear.bearIsDamagable = true;        
        }
        
    }

   
}
