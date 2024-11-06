using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRabitController : MonoBehaviour
{
    public Animator animator; // Animator do zmiany animacji (opcjonalne)
    public SpriteRenderer rabbitSpriteRenderer; // Renderer sprite'a królika
    public Sprite originalSprite; // Oryginalny sprite królika
    public Sprite clickedSprite; // Sprite królika po kliknięciu
    public float inactivityThreshold = 0.5f; // Czas bezdotykowy
    public float clickRadius = 1.0f; // Promień kliknięcia
    public Transform targetPosition; // Miejsce, które należy kliknąć

    private bool isClicked = false; // Flaga, aby sprawdzić, czy królik został kliknięty

    private void Start()
    {
        rabbitSpriteRenderer.sprite = originalSprite; // Ustawiamy początkowy sprite
    }

    private void Update()
    {
        DetectTouch();
        HandleInactivity();
    }

    private void DetectTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                // Sprawdzamy, czy dotyk jest w obrębie promienia kliknięcia
                if (Vector2.Distance(touchPosition, targetPosition.position) <= clickRadius)
                {
                    rabbitSpriteRenderer.sprite = clickedSprite; // Ustawiamy sprite na clickedSprite
                    animator.SetBool("onClick", true); // Ustawiamy animację, jeśli jest potrzebna
                    isClicked = true; // Zapisujemy stan kliknięcia

                    // Odtwarzanie dźwięku przy każdym kliknięciu
                    FMODUnity.RuntimeManager.PlayOneShot("event:/KociolClick"); // Ścieżka do dźwięku
                }
            }
        }
    }

    private void HandleInactivity()
    {
        if (isClicked) return; // Jeśli kliknięto królika, nie resetujemy

        // Jeżeli nie ma aktywnego dotyku i królik nie był kliknięty, resetuj animację
        if (Input.touchCount == 0 && animator.GetBool("onClick"))
        {
            animator.SetBool("onClick", false); // Ustawiamy na false po czasie bezdotykowym
        }
    }
}
