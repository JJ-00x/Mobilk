using System.Collections;
using System.Collections.Generic;
using System.Net;

using UnityEngine;
using UnityEngine.SceneManagement;


public class PotController : MonoBehaviour
{
    public IntValue Points;
    public NewsSubLetter PlayerDeathNewsletter;
    public NewsSubLetter PotHitNewsletter;
    
    public bool isClicked;
    
    //private Animator _anim;
   // [SerializeField] private GameObject _pot;
    
   
    public void Awake()
    {
       //_anim = GetComponent<Animator>();
      
     
    }

    private void OnMouseDown()
    {
        PointAdd();
        isClicked = true;
        PotHitNewsletter.SendNewsletter();
        
        if (Points.value == 100)
        {
            PlayerDeathNewsletter.SendNewsletter();
            Win();
        }
      
    }
    private void OnMouseUp()
    {
        Stop();

       

    }

    private void PointAdd()
    {
        Points.value += 1;
       // _anim.SetBool("onClick", true);
        Debug.Log("pain");
       
        // _blood.SetActive(true);
    }

    private void Stop()
    {
      // _anim.SetBool("onClick", false);
       // _blood.SetActive(false);
       
       
    }
    public void Click()
    {
        isClicked = false;
        Debug.Log("klikam");
      
    }

    

    private void Dead()
    {
        Debug.Log("dętka");
        
    }

    private void Win()
    {
        SceneManager.LoadScene("Win");
    }

}