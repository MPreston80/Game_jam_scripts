using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    GameObject bubble; 
    float horizontal; 
    float vertical;
    public static CharController instance;  //code monkey tutorial
    public static int mooseHealth = 30;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject introPanel;
    public static int kills = 6;


   void Start()
   { 
      rigidbody2d = GetComponent<Rigidbody2D>();       

   }

   void Awake()//added with this tutorial: https://www.youtube.com/watch?v=kELzSbrLfU8&t=66s (code Monkey spider man tut)
   {
      instance = this;
   }

   
   
   void Update()
   {
      horizontal = Input.GetAxis("Horizontal");
      vertical = Input.GetAxis("Vertical"); 
      if(mooseHealth < 0)
      {
         mooseHealth = 0;
         Time.timeScale = 0;
         losePanel.SetActive(true);
      }
      if(kills == 0)
      {
         winPanel.SetActive(true);
         Time.timeScale = 0;
      }     
   }

   public void startOver()
   {
      Time.timeScale = 1;
      mooseHealth = 30;
      kills = 6;
      collectGum.counter = 0;
      SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
      
   }

   public void destroyPanel()
   {
      Destroy(introPanel);
   }


   void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + 3.0f * horizontal * Time.deltaTime;
        position.y = position.y + 3.0f * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
        
    }
}