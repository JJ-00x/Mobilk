using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // Dodano przestrzeń nazw dla UI

public class LiquidFilled : MonoBehaviour, INewsletter
{
    public TextMeshProUGUI textMeshProUGUI; // Komponent tekstowy
    public IntValue PotPoints; // Wartość punktów
    public NewsSubLetter PlayerDeathNewsletter; // Subskrybent na wiadomości

    public Image progressBar; // Komponent paska postępu
    public int maxPoints = 100; // Maksymalna wartość punktów dla paska postępu

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        PotPoints.value = 0; // Inicjalizacja punktów na 0
        UpdateProgressBar(); // Ustawienie początkowego stanu paska postępu
    }

    private void Update()
    {
        UpdateTextWithNumbers(PotPoints.value); // Aktualizacja tekstu z punktami
        UpdateProgressBar(); // Aktualizacja paska postępu
        PlayerDeathNewsletter.SubsrcribeForNewsletter(this); // Subskrypcja na powiadomienia
    }

    public void UpdateTextWithNumbers(int value)
    {
        if (textMeshProUGUI != null) textMeshProUGUI.text = value.ToString();
    }

    public void UpdateProgressBar()
    {
        if (progressBar != null)
        {
            // Obliczamy wypełnienie paska postępu
            float fillAmount = (float)PotPoints.value / maxPoints;
            progressBar.fillAmount = fillAmount; // Ustawiamy wypełnienie paska
        }
    }

    public void Notify()
    {
        PlayerDeathNewsletter.UnsubscribeForNewsletter(this); // Unsubscribe from the newsletter
        Debug.Log("Informed");
        // Destroy(textMeshProUGUI); // Można odkomentować, jeśli chcemy zniszczyć komponent
    }
}