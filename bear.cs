using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bear : MonoBehaviour
{
    public static bool bearIsDamagable = false;
    public GameObject deadBearPrefab;
    private CharController target;
    public float speed;
    private int bearHealth = 6;
    private Vector2 newTarget;
    public bool facingRight; //Is bear facing right before runtime? If yes, check the box.
    public AudioSource bearGrowl;
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
                newTarget = new Vector2(target.transform.position.x - 1.0f, target.transform.position.y + 1.8f);
                transform.position = Vector2.MoveTowards(transform.position, newTarget, speed * Time.deltaTime); 
            }
            if(target.transform.position.x < transform.position.x)//If Moose is to the left of the wolf
            {
                newTarget = new Vector2(target.transform.position.x + 1.0f, target.transform.position.y + 1.8f);
                transform.position = Vector2.MoveTowards(transform.position, newTarget, speed * Time.deltaTime);
            }        
            
        }

        if(!insideKillZone)
        {
            bearIsDamagable = false;
        }
        if(bearIsDamagable)
        {
            damageBear();            
        }
        
    }

    void damageBear()
    {
        bearIsDamagable = false;
        if(bearHealth < 4)
        {
            CharController.kills--;
            bearGrowl.Stop();            
            Instantiate(deadBearPrefab, new Vector3(transform.localPosition.x, transform.localPosition.y, 0), Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            bearHealth -= 3;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="growl")
        {
            bearGrowl.Play();
            insideKillZone = true;
        }
        if(col.tag=="damage")
        {
            CharController.mooseHealth -= 4;
            //Debug.Log("Moose Health: " + CharController.mooseHealth);
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
