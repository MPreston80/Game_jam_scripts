using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolf2 : MonoBehaviour
{
    public static bool wolf2IsDead = false;
    public GameObject deadWolfprefab;
    private CharController target;
    public float speed;
    private Vector2 newTarget;
    public bool facingRight; //Is wolf facing right before runtime? If yes, check the box.
    public AudioSource growl;
    private bool insideKillZone;
    
    
    void Start()
    {
        target = CharController.instance;        
    }   

    
    void Update()
    {
        
        if(Vector2.Distance(transform.position, target.transform.position)< 7.0f)
        {
            
            if((!facingRight)&&(target.transform.position.x > transform.position.x))
            {
                facingRight = !facingRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            if(facingRight&&(target.transform.position.x < transform.position.x))
            {
                facingRight = !facingRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            if(target.transform.position.x > transform.position.x)//Moose is to the right of the wolf
            {
                newTarget = new Vector2(target.transform.position.x - 2.0f, target.transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, newTarget, speed * Time.deltaTime); 
            }
            if(target.transform.position.x < transform.position.x)//If Moose is to the left of the wolf
            {
                newTarget = new Vector2(target.transform.position.x + 2.0f, target.transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, newTarget, speed * Time.deltaTime);
            }        
            
        }

        if(!insideKillZone)
        {
            wolf2IsDead = false;
        }
        if(wolf2IsDead)
        {
            CharController.kills--;
            growl.Stop();            
            Instantiate(deadWolfprefab, new Vector3(transform.localPosition.x, transform.localPosition.y, 0), Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="growl")
        {
            growl.Play();
            insideKillZone = true;
        }
        if(col.tag=="damage")
        {
            CharController.mooseHealth -= 2;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag=="growl")
        {
            insideKillZone = false;
        }
    }
}
