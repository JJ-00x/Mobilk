using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WitchController : MonoBehaviour
{
    
     public SpriteRenderer characterSpriteRenderer;  // Renderer dla sprite’a postaci
    public float minFlipInterval = 1.0f;            // Minimalny czas do flipowania
    public float maxFlipInterval = 3.0f;            // Maksymalny czas do flipowania
    public Transform targetPosition;                // Miejsce, które należy kliknąć, by wygrać
    public float clickRadius = 1.0f;                // Promień kliknięcia wokół targetPosition
    public string loseSceneName = "LoseScene";      // Nazwa sceny przegranej

    public MobileTouchController pot;  // Referencja do skryptu PotController
    private bool isFlipped = false;
    private Color originalColor; // Oryginalny kolor sprite'a
    public Color flipColor = Color.red; // Kolor, na który zmieniamy sprite'a przed odwróceniem
    private bool touchDetectedDuringFlip = false; // Czy dotyk został wykryty podczas flipowania

    private void Start()
    {
        originalColor = characterSpriteRenderer.color; // Zapamiętaj oryginalny kolor
        StartCoroutine(RandomFlipRoutine());  // Uruchomienie losowego flipowania
    }

    private void Update()
    {
        DetectTouchOnTarget();
        CheckLoseCondition();
    }

    private System.Collections.IEnumerator RandomFlipRoutine()
    {
        while (true)
        {
            // Losowy czas do następnego odwrócenia postaci
            float waitTime = Random.Range(minFlipInterval, maxFlipInterval);
            yield return new WaitForSeconds(waitTime);

            // Zmień kolor sprite'a przed odwróceniem
            characterSpriteRenderer.color = flipColor;

            // Czekaj 1 sekundę
            yield return new WaitForSeconds(1.0f);

            // Odwrócenie postaci
            isFlipped = !isFlipped;
            characterSpriteRenderer.flipX = isFlipped;

            // Przywróć oryginalny kolor
            characterSpriteRenderer.color = originalColor;
        }
    }

    private void DetectTouchOnTarget()
    {
        // Sprawdzamy dotyk na ekranie
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                // Sprawdzamy odległość od celu
                if (Vector2.Distance(touchPosition, targetPosition.position) <= clickRadius)
                {
                    Debug.Log("Cel trafiony, ale nie przerywamy gry."); // Możesz usunąć tę linię, jeśli nie chcesz
                }

                // Ustawiamy zmienną, jeśli dotyk został wykryty
                touchDetectedDuringFlip = true;
            }
        }
        else
        {
            // Resetuj zmienną, jeśli nie ma dotyku
            touchDetectedDuringFlip = false;
        }
    }

    private void CheckLoseCondition()
    {
        // Sprawdzamy, czy `isClicked` jest true, postać jest odwrócona oraz czy dotyk został wykryty
        if (pot != null && pot.isClicked && characterSpriteRenderer.flipX && touchDetectedDuringFlip)
        {
           
            Debug.Log("Warunki przegranej spełnione - ładowanie sceny przegranej.");
            LoadLoseScene();
        }
    }

    private void LoadLoseScene()
    {
        SceneManager.LoadScene(loseSceneName);  // Ładowanie sceny przegranej
    }
}