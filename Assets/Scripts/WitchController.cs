using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WitchController : MonoBehaviour
{
    public SpriteRenderer characterSpriteRenderer;
    public float minFlipInterval = 1.0f;
    public float maxFlipInterval = 3.0f;
    public Transform targetPosition;
    public float clickRadius = 1.0f;
    public string loseSceneName = "LoseScene";

    public MobileTouchController pot;
    private bool isFlipped = false;
    private Color originalColor;
    public Color flipColor = Color.red;
    private bool touchDetectedDuringFlip = false;

    private void Start()
    {
        originalColor = characterSpriteRenderer.color;
        StartCoroutine(RandomFlipRoutine());
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
            float waitTime = Random.Range(minFlipInterval, maxFlipInterval);
            yield return new WaitForSeconds(waitTime);

            // Zmień kolor sprite'a na czerwony i odtwórz dźwięk
            characterSpriteRenderer.color = flipColor;
            FMODUnity.RuntimeManager.PlayOneShot("event:/EnemyAlert"); // Odtworzenie dźwięku w momencie zmiany koloru

            yield return new WaitForSeconds(1.0f);

            isFlipped = !isFlipped;
            characterSpriteRenderer.flipX = isFlipped;
            characterSpriteRenderer.color = originalColor;
        }
    }

    private void DetectTouchOnTarget()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                if (Vector2.Distance(touchPosition, targetPosition.position) <= clickRadius)
                {
                    Debug.Log("Cel trafiony, ale nie przerywamy gry.");
                }

                touchDetectedDuringFlip = true;
            }
        }
        else
        {
            touchDetectedDuringFlip = false;
        }
    }

    private void CheckLoseCondition()
    {
        if (pot != null && pot.isClicked && characterSpriteRenderer.flipX && touchDetectedDuringFlip)
        {
            Debug.Log("Warunki przegranej spełnione - ładowanie sceny przegranej.");
            LoadLoseScene();
        }
    }

    private void LoadLoseScene()
    {
        SceneManager.LoadScene(loseSceneName);
    }
}
