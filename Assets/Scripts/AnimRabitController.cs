using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AnimRabitController : MonoBehaviour
{
    public Animator animator; // Animator do zmiany animacji
    public float inactivityThreshold = 0.5f; // Czas bezdotykowy, po którym animacja przełącza się na false
    public float clickRadius = 1.0f; // Promień kliknięcia
    public Transform targetPosition; // Miejsce, które należy kliknąć, by zmienić animację

    private float inactivityTimer; // Timer do śledzenia czasu bezdotykowego

    private void Update()
    {
        DetectTouch();
        HandleInactivity();
    }

    private void DetectTouch()
    {
        // Sprawdzamy dotyk na ekranie
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                // Sprawdzamy, czy dotyk jest w obrębie promienia
                if (Vector2.Distance(touchPosition, targetPosition.position) <= clickRadius)
                {
                    animator.SetBool("onClick", true); // Ustawiamy na true, gdy dotyk jest wykryty
                    inactivityTimer = 0f; // Resetuj timer bezdotykowy
                }
            }
        }
    }

    private void HandleInactivity()
    {
        // Jeżeli nie ma aktywnego dotyku
        if (Input.touchCount == 0)
        {
            inactivityTimer += Time.deltaTime; // Zwiększaj timer bezdotykowy

            // Jeżeli czas bezdotykowy przekroczył próg, zmień animację
            if (inactivityTimer >= inactivityThreshold)
            {
                animator.SetBool("onClick", false); // Ustawiamy na false po czasie bezdotykowym
            }
        }
    }
}
