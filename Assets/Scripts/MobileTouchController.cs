using UnityEngine;
using UnityEngine.SceneManagement;

public class MobileTouchController : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public IntValue Points;
    public NewsSubLetter PlayerDeathNewsletter;
    public NewsSubLetter PotHitNewsletter;
    public bool isClicked;
    
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Sprawdź, czy ekran został dotknięty
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Pobierz pierwszy dotyk

            // Sprawdź, czy dotyk jest w fazie rozpoczęcia (TouchPhase.Began)
            if (touch.phase == TouchPhase.Began)
            {
                // Konwertuj pozycję dotyku na współrzędne świata
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                
                // Sprawdź, czy dotyk znajduje się w obrębie BoxCollider2D
                if (boxCollider.OverlapPoint(touchPosition))
                {
                    // Wykonaj akcję, gdy dotykł obiektu
                    OnObjectTouched();
                    
                }
            }
        }
    }

    void OnObjectTouched()
    {
        Debug.Log("Obiekt został dotknięty!");
        // Tutaj możesz dodać inne akcje, np. dodanie punktów, zmiana koloru, itd.
        PointAdd();
        isClicked = true;
        PotHitNewsletter.SendNewsletter();
        
        if (Points.value == 100)
        {
            PlayerDeathNewsletter.SendNewsletter();
            Win();
        }
    }
    
    private void PointAdd()
    {
        Points.value += 1;
        // _anim.SetBool("onClick", true);
        Debug.Log("pain");
       
        // _blood.SetActive(true);
    }
    private void Win()
    {
        SceneManager.LoadScene("Win");
    }
}
