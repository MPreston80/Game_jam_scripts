using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squirrel1 : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public bool facingRight; //Is squirrel facing right before runtime? If yes, check the box.
    public float horizontalValue;
    public AudioSource squirrelNoise;
    private Vector2 newTarget;
    public static bool squirrel1IsDead = false;
    public GameObject deadSquirrelPrefab;
    private bool insideKillZone;
    //public static squirrelPatrol squirrelInstance;

    public Transform[] moveSpots;
    private int randomSpot;
    private CharController target;
    // Start is called before the first frame update
    void Start()
    {
        target = CharController.instance;
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
    }



    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.transform.position)< 3.3f)
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
                newTarget = new Vector2(target.transform.position.x - 1.0f, target.transform.position.y + 0.5f);
                transform.position = Vector2.MoveTowards(transform.position, newTarget, speed * Time.deltaTime); 
            }
            if(target.transform.position.x < transform.position.x)//If Moose is to the left of the wolf
            {
                newTarget = new Vector2(target.transform.position.x + 1.0f, target.transform.position.y + 0.5f);
                transform.position = Vector2.MoveTowards(transform.position, newTarget, speed * Time.deltaTime);
            }        
        }
        if(Vector2.Distance(transform.position, target.transform.position)>= 3.3f)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
            
            if((!facingRight)&&(moveSpots[randomSpot].position.x > transform.position.x))
            {
                facingRight = !facingRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            if(facingRight&&(moveSpots[randomSpot].position.x < transform.position.x))
            {
                facingRight = !facingRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }       

        if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if(!insideKillZone)
        {
            squirrel1IsDead = false;
        }

        if(squirrel1IsDead)
        {
            CharController.kills--;
            squirrelNoise.Stop();            
            Instantiate(deadSquirrelPrefab, new Vector3(transform.localPosition.x, transform.localPosition.y, 0), Quaternion.identity);
            Destroy(gameObject);
        }

        
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag=="growl")
        {
            squirrelNoise.Play();
            insideKillZone = true;            
        }
        if(col.tag=="damage")
        {
            CharController.mooseHealth -= 1;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag=="growl")
        {
            squirrelNoise.Stop();
            insideKillZone = false;
        }
    }
}





