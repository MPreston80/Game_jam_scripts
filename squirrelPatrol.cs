using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squirrelPatrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public bool facingRight; //Is squirrel facing right before runtime? If yes, check the box.
    public float horizontalValue;
    public AudioSource squirrelNoise;
    private Vector2 newTarget;
    public static bool squirrelIsDead = false;
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
            if(target.transform.position.x > transform.position.x)
            {
                newTarget = new Vector2(target.transform.position.x - 1.0f, target.transform.position.y + 0.5f);
                transform.position = Vector2.MoveTowards(transform.position, newTarget, speed * Time.deltaTime); 
            }
            if(target.transform.position.x < transform.position.x)
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
            squirrelIsDead = false;
        }

        if(squirrelIsDead)
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





//  Vector2 direction = target.transform.position - transform.position;
//             float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
//             Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
//             transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
//    Old code that wasn't quite the right solution for getting sprite to face correct direction. 

// horizontalValue = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
// if((horizontalValue < 0 && facingRight) || (horizontalValue > 0 && !facingRight))
// {
//     facingRight = !facingRight;
//     Vector3 theScale = transform.localScale;
//     theScale.x *= -1;
//     transform.localScale = theScale;
// }


//transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
//horizontalValue = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
// if((horizontalValue < 0 && facingRight) || (horizontalValue > 0 && !facingRight))
// {
 //     facingRight = !facingRight;
//     Vector3 theScale = transform.localScale;
//     theScale.x *= -1;
//     transform.localScale = theScale;
// }